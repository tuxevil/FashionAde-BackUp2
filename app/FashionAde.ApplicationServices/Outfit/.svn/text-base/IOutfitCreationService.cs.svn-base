using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.OutfitCombination;
using FashionAde.Core.Clothing;

namespace FashionAde.ApplicationServices.Outfit
{
    public interface IOutfitCreationService
    {
        void CreateUserOutfit(int userId, IList<Garment> garments, Season season, ClosetOutfitVisibility visibility);
        void CopyOutfitFromOtherUserCloset(int closetOutfitId, int userId);
        bool CanCopyOutfit(int closetOutfitId, int closetId);
    }
}
