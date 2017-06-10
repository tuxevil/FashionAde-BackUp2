using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace FashionAde.Utils
{
    public class StringHelper
    {
        public static bool IsNumber(string text)
        {
            Array chars = text.ToCharArray();
            foreach (char c in chars)
                if (!char.IsNumber(c))
                    return false;

            return true;
        }

        public static bool IsDateTime(string date)
        {
            try
            {
                Convert.ToDateTime(date);
                return true;
            }
            catch (Exception)
            {
                try
                {
                    IFormatProvider formatProvider = new CultureInfo("en-US");
                    Convert.ToDateTime(date, formatProvider);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}
