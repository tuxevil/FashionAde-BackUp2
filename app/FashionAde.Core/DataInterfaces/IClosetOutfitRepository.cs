using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.Accounts;
using FashionAde.Core.Clothing;
using FashionAde.Core.MVCInteraction;
using FashionAde.Core.OutfitEngine;
using FashionAde.Core.ThirdParties;
using SharpArch.Core.PersistenceSupport;

namespace FashionAde.Core.DataInterfaces
{
    public interface IClosetOutfitRepository : IRepository<ClosetOutfit>
    {
        //IList<ClosetOutfitGarmentView> GetComponentList(IList<ClosetOutfitView> outfits, Closet closet);
        IList<ClosetOutfitView> Search(out int totalCount, int pageSize, int pageNumber, int season, string search, string sortBy, RegisteredUser user);
        ClosetOutfitView GetByClosetOutfitId(int closetOutfitId);
        IList<ClosetOutfitView> GetTopRatedOutfits(int userId);
        bool HaveBeenRated(int closetId);
        bool CanCopyOutfit(int closetOutfitId, int closetId);
    }
}
