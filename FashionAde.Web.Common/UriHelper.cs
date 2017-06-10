using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Utils;
using System.Configuration;
using System.Web.Mvc;

namespace FashionAde.Web.Extensions
{
    public static class HtmlHelpers
    {
        private static string StaticUrl = ConfigurationManager.AppSettings["StaticURL"];

        /// <summary>
        /// Creates an static url with a different hostname for each file provided.
        /// Assures the same hostname for the same file.
        /// </summary>
        /// <param name="fileName">fileName to be used</param>
        /// <returns>Full path to be used</returns>
        /// <remarks>Need setup on web.config with a StaticURL application setting.</remarks>
        public static string GetStaticUri(this HtmlHelper html, string fileName)
        {
            return GetStaticUri(Security.DefineHashForHostName(fileName));
        }

        /// <summary>
        /// Creates an static url with a different hostname for each index provided.
        /// </summary>
        /// <param name="index">Index in the static namespace</param>
        /// <returns></returns>
        public static string GetStaticUri(this HtmlHelper html, int index)
        {
            return GetStaticUri(index);
        }

        /// <summary>
        /// Creates an static url with a different hostname for each index provided.
        /// </summary>
        /// <param name="index">Index in the static namespace</param>
        /// <returns></returns>
        public static string GetStaticUri(this HtmlHelper html)
        {
            return GetStaticUri();
        }

        public static string GetStaticUri()
        {
            return GetStaticUri(1);
        }

        public static string GetStaticUri(int index)
        {
            string[] split = StaticUrl.Split(':');
            string hostName = StaticUrl;
            int portNumber = 0;
            if (split.Length == 2)
            {
                hostName = split[0];
                portNumber = Convert.ToInt32(split[1]);
            }

            UriBuilder uriBuilder = new UriBuilder();
            uriBuilder.Host = string.Format(hostName, index);
            if (portNumber != 0)
                uriBuilder.Port = portNumber;

            return uriBuilder.ToString();
        }
    }
}
