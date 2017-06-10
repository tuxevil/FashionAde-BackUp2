using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ContactProvider
{
    public class ProviderFactory
    {
        public static IContactProvider GetCurrentProvider()
        {
            if (HttpContext.Current.Request.Url.PathAndQuery.Contains("google"))
                return new GoogleProvider();
            else if (HttpContext.Current.Request.Url.PathAndQuery.Contains("live"))
                return new LiveProvider();
            else if (HttpContext.Current.Request.Url.PathAndQuery.Contains("yahoo"))
                return new YahooProvider();

            throw new Exception("Not provider found");
        }

        public static IContactProvider GetProvider(string providerKey)
        {
            if (providerKey.Contains("google"))
                return new GoogleProvider();
            else if (providerKey.Contains("live"))
                return new LiveProvider();
            else if (providerKey.Contains("yahoo"))
                return new YahooProvider();

            throw new Exception("Not provider found");
        }

    }
}
