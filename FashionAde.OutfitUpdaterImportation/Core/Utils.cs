using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FashionAde.OutfitUpdaterImportation.Core
{
    public class Utils
    {
        public static log4net.ILog GetLogger()
        {
            return log4net.LogManager.GetLogger("OutfitUpdaterImportation");
        }
    }
}
