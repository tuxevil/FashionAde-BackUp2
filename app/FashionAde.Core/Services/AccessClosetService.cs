using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.Accounts;

namespace FashionAde.Core.Services
{
    public class AccessClosetService
    {
        /// <summary>
        /// Validates if the user can access the closet
        /// </summary>
        /// <param name="bu">Basic User</param>
        /// <param name="c">Closet</param>
        /// <returns></returns>
        public static bool CanViewCloset(BasicUser bu, Closet c)
        {
            if (bu != null && c.User.Id == bu.Id)
                return true;

            if (c.PrivacyLevel == PrivacyLevel.FullCloset)
                return true;

            if (bu != null && c.PrivacyLevel == PrivacyLevel.Friends && (from friend in bu.FriendsAccepted where friend.User.Id == c.User.Id select friend).Count() > 0)
                return true;

            return false;
        }

        /// <summary>
        /// Validates if the user can access the Outfit
        /// </summary>
        /// <param name="bu"></param>
        /// <param name="co"></param>
        /// <returns></returns>
        public static bool CanViewClosetOutfit(BasicUser bu, ClosetOutfit co)
        {
            Closet c = co.Closet;

            if (bu != null && c.User.Id == bu.Id)
                return true;

            bool isFriendOrFull = CanViewCloset(bu, c);
            if (isFriendOrFull)
                return true;

            if (c.PrivacyLevel == PrivacyLevel.FavoriteOutfit && co.Id == co.Closet.FavoriteOutfit.Id)
                return true;

            return false;
        }

    }
}
