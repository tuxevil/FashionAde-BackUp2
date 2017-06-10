using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core;
using FashionAde.Core.Clothing;
using FashionAde.Core.ContentManagement;
using FashionAde.Core.MVCInteraction;

namespace FashionAde.Web.Controllers.MVCInteraction
{
    public class HomeRegisteredUserInfo
    {
        private IList<ClosetOutfitView> topRatedOutfits;
        private IList<UserGarment> recentlyUploadedGarments;
        private IList<FashionFlavor> fashionFlavors;
        private IList<ContentPublishedSection> styleAlerts;
        private string userName;
        private bool haveBeenRated;

        public virtual IList<ClosetOutfitView> TopRatedOutfits
        {
            get { return topRatedOutfits; }
            set { topRatedOutfits = value; }
        }

        public virtual IList<UserGarment> RecentlyUploadedGarments
        {
            get { return recentlyUploadedGarments; }
            set { recentlyUploadedGarments = value; }
        }

        public virtual IList<FashionFlavor> FashionFlavors
        {
            get { return fashionFlavors; }
            set { fashionFlavors = value; }
        }

        public virtual IList<ContentPublishedSection> StyleAlerts
        {
            get { return styleAlerts; }
            set { styleAlerts = value; }
        }

        public virtual string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        public virtual bool HaveBeenRated
        {
            get { return haveBeenRated; }
            set { haveBeenRated = value; }
        }
    }
}
