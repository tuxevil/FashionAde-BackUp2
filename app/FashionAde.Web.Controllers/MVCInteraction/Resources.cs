using System.Web.Mvc;
using FashionAde.Core;
using FashionAde.Core.Clothing;
using FashionAde.Core.FlavorSelection;
using System.IO;
using FashionAde.Core.MVCInteraction;
using System.Configuration;
using FashionAde.Web.Extensions;

namespace FashionAde.Web.Controllers.MVCInteraction
{
    public static class Resources
    {
        private static string rootPath = HtmlHelpers.GetStaticUri();

        public static string GetFashionFlavorThumbnailPath(FashionFlavor fashionFlavor)
        {
            return rootPath + "res/FashionFlavors/shape_" + fashionFlavor.Id + "_small.gif";
        }

        public static string GetFashionFlavorPath(FashionFlavor fashionFlavor)
        {
            return rootPath + "res/FashionFlavors/shape_" + fashionFlavor.Id + ".gif";
        }

        public static string GetEventTypePath(EventType eventType)
        {
            return rootPath + "res/EventTypes/" + eventType.Image;
        }

        public static string GetSilouhettePath(Silouhette silouhette)
        {
            return rootPath + "res/Silouhettes/" + silouhette.ImageUri;            
        }

        public static string GetGarmentPath()
        {
            return HtmlHelpers.GetStaticUri(3) + "res/Garments/65/";
        }

        public static string GetGarmentLargePath()
        {
            return HtmlHelpers.GetStaticUri(3) + "res/Garments/95/";
        }

        public static string GetGarmentPath(Garment garment)
        {
            return GetGarmentPath() + garment.ImageUri;
        }

        public static string GetGarmentLargePath(Garment garment)
        {
            return GetGarmentLargePath() + garment.ImageUri;
        }

        public static string GetPatternPath()
        {
            return rootPath + "res/Patterns/";
        }

        public static string GetPatternPath(Pattern pattern)
        {
            return GetPatternPath() + pattern.ImageUri;
        }

        public static string GetStylePhotographPath(StylePhotograph stylePhotograph)
        {
            return rootPath + "res/StylePhotographs/" + stylePhotograph.ImageUri;
        }

        public static string GetBrandSetPath(BrandSet brandSet)
        {
            return rootPath + "res/BrandSets/" + brandSet.ImageUri;
        }

        public static string GetWordingPath(Wording wording)
        {
            return rootPath + "res/Wordings/" + wording.ImageUri;
        }

        public static string GetFlavorsPath()
        {
            return rootPath + "res/FashionFlavors/";
        }

        public static string GetFashionResultPath(Wording wording)
        {
            return rootPath + "res/Wordings/" + wording.ImageUri;
        }

        public static string GetFriendProviderPath(string providerImg)
        {
            return rootPath + "res/FriendProviders/" + providerImg;
        }

        public static string GetWebClosetGarmentPath(WebClosetGarment garment)
        {
            return GetGarmentPath() + garment.ImageUri;
        }

        public static string GetClosetOutfitGarmentViewPath(ClosetOutfitGarmentView garment)
        {
            return GetGarmentPath() + garment.ImageUri;
        }

        public static string GetRatingLargePath()
        {
            return HtmlHelpers.GetStaticUri(3) + "res/Rating/";
        }

        public static string GetRatingText(int rating)
        {
            switch (rating)
            {
                case 1:
                    return "Never wear this!";
                case 2:
                    return "Wear it in pinch";
                case 3:
                    return "Wear it occasionally";
                case 4:
                    return "Love it – put it in regular rotation!";
                case 5:
                    return "This is a Signature Outfit*";
            }
            return string.Empty;
        }
    }
}


