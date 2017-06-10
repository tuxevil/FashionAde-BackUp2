using System;
using FashionAde.Core.Clothing;
using System.Collections.Generic;

namespace FashionAde.Web.Controllers.MVCInteraction
{
    public class SeasonHelper
    {
        public static Season CurrentSeason
        {
            get 
            {
                int doy = DateTime.Now.DayOfYear - Convert.ToInt32((DateTime.IsLeapYear(DateTime.Now.Year)) && DateTime.Now.DayOfYear > 59);
                Season currentSeason = ((doy < 80 || doy >= 355)
                                            ? Season.Winter
                                            : ((doy >= 80 && doy < 172)
                                                   ? Season.Spring
                                                   : ((doy >= 172 && doy < 266) ? Season.Summer : Season.Fall)));

                return currentSeason;            
            }
        }

        public static string CurrentSeasonId
        {
            get  { return ((int)CurrentSeason).ToString(); }
        }
        
        public static IList<Season> ListSeasons()
        {
            List<Season> lst = new List<Season>();

            switch (CurrentSeason)
            {
                case Season.Spring:
                    lst.AddRange(new List<Season> { Season.Spring, Season.Summer, Season.Fall, Season.Winter, Season.All });
                    break;
                case Season.Summer:
                    lst.AddRange(new List<Season> { Season.Summer, Season.Fall, Season.Winter, Season.Spring, Season.All });
                    break;
                case Season.Fall:
                    lst.AddRange(new List<Season> { Season.Fall, Season.Winter, Season.Spring, Season.Summer, Season.All });
                    break;
                case Season.Winter:
                    lst.AddRange(new List<Season> { Season.Winter, Season.Spring, Season.Summer, Season.Fall, Season.All });
                    break;
                case Season.All:
                    break;                
            }

            return lst;
        }
    }
}
