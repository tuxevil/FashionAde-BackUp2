using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.Accounts;
using FashionAde.Core.OutfitCombination;
using FashionAde.Core.DataInterfaces;
using FashionAde.Core.Clothing;
using FashionAde.Core;
using FashionAde.Core.OutfitEngine;
using System.Reflection;
using FashionAde.Core.Services;
using SharpArch.Web.NHibernate;

namespace FashionAde.ApplicationServices.Outfit
{
    public class OutfitCreationService : IOutfitCreationService
    {
        private IBasicUserRepository basicUserRepository;
        private IClosetOutfitRepository closetOutfitRepository;
        private IPreCombinationRepository preCombinationRepository;
        private IGarmentRepository garmentRepository;

        public OutfitCreationService(IGarmentRepository garmentRepository, IBasicUserRepository basicUserRepository, IClosetOutfitRepository closetOutfitRepository, IPreCombinationRepository preCombinationRepository)
        {
            this.basicUserRepository = basicUserRepository;
            this.closetOutfitRepository = closetOutfitRepository;
            this.preCombinationRepository = preCombinationRepository;
            this.garmentRepository = garmentRepository;
        }

        /// <summary>
        /// Creates a new closet outfit in the system related with the current user.
        /// </summary>
        /// <param name="userId">User</param>
        /// <param name="garments">List of Garments</param>
        /// <param name="season">Season</param>
        /// <param name="visibility">Visibility</param>
        /// <exception cref="NotValidCombinationException">When a combination is not valid.</exception>
        public void CreateUserOutfit(int userId, IList<Garment> garments, Season season, ClosetOutfitVisibility visibility)
        {
            if (!OutfitValidationService.IsValidCombination(garments))
                throw new NotValidCombinationException();

            ClosetOutfit uo = new ClosetOutfit();

            // HACK: We need the garments saved with 0 in the non filled fields to make sure we have no duplicates.
            Garment hack = garmentRepository.Get(0);
            foreach (Garment g in garments)
                uo.AddComponent(g, hack);

            uo.Rating.CalculateEditorRating(uo.Components);

            BasicUser bu = basicUserRepository.Get(userId);
            FashionFlavor ff = bu.GetPreferredFlavor(); 
            
            uo.FashionFlavor = ff;
            uo.Closet = (bu as RegisteredUser).Closet;

            uo.SetSeason(season);
            uo.SetEventTypes(bu as RegisteredUser);
            uo.SetVisibility(visibility);
            
            //Agregar PreCombination
            PreCombination pc = preCombinationRepository.GetByGarments(uo.RetrieveCombinableComponents().ToList<Garment>(), ff);

            if (pc == null)
            {
                pc = new PreCombination();
                pc.FashionFlavor = ff;

                for (int j = 0; j < garments.Count; j++)
                {
                    Garment g = garments[j];
                    if (!OutfitValidationService.IsAccessory(g))
                        pc.AddPreGarment(garments[j]);
                }
            }

            uo.PreCombination = pc;
            uo.User = bu as RegisteredUser;

            if (!uo.IsValid())
                throw new NotValidCombinationException();

            closetOutfitRepository.DbContext.BeginTransaction();

            // REVIEW: This may lead to orphaned records, given we cannot change the autonumeric for now because of the combination process.
            // TODO: Is better to check first if the Closet Outfit combination already exists and then proceed. 
            preCombinationRepository.SaveOrUpdate(pc);

            try
            {
                closetOutfitRepository.SaveOrUpdate(uo);
                closetOutfitRepository.DbContext.BeginTransaction();
            }
            catch
            {
                closetOutfitRepository.DbContext.RollbackTransaction();
                throw new CombinationAlreadyExistsException();
            }
        }
        
        public void CopyOutfitFromOtherUserCloset(int closetOutfitId, int userId)
        {
            ClosetOutfit co = closetOutfitRepository.Get(closetOutfitId);

            // Make sure this closet is public
            if (co.Closet.PrivacyLevel != PrivacyLevel.FullCloset &&
                !(co.Closet.FavoriteOutfit == co && co.Closet.PrivacyLevel == PrivacyLevel.FavoriteOutfit))
                throw new Exception("You can not access this outfit.");

            ClosetOutfit copiedOutfit = co.Clone() as ClosetOutfit;
            copiedOutfit.CreatedFrom = co;

            BasicUser bu = basicUserRepository.Get(userId);
            copiedOutfit.FashionFlavor = bu.GetPreferredFlavor();
            copiedOutfit.SetEventTypes(bu as RegisteredUser);
            copiedOutfit.Closet = (bu as RegisteredUser).Closet;
            copiedOutfit.User = bu as RegisteredUser;

            closetOutfitRepository.SaveOrUpdate(copiedOutfit);            
        }

        public bool CanCopyOutfit(int closetOutfitId, int closetId)
        {
            return closetOutfitRepository.CanCopyOutfit(closetOutfitId, closetId);
        }
    }

    public class NotValidCombinationException : Exception
    {
    }

    public class CombinationAlreadyExistsException : Exception
    {
    }

}
