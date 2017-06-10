using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using OAuth.Net.Common;
using OAuth.Net.Components;
using ContactProvider.Helpers;

namespace ContactProvider
{
    public class YahooProvider : IContactProvider
    {
        private int totalCount = 0;
        private string apiKey = ConfigurationManager.AppSettings["Yahoo_ApiKey"];
        private string returnUrl = ConfigurationManager.AppSettings["SiteURL"] + ConfigurationManager.AppSettings["Yahoo_ReturnUrl"];
        private string secret = ConfigurationManager.AppSettings["Yahoo_Secret"];
        private string appUrl = ConfigurationManager.AppSettings["SiteURL"].Split(':')[0] + ConfigurationManager.AppSettings["SiteURL"].Split(':')[1];
        private string contactsPageUrl = ConfigurationManager.AppSettings["SiteURL"] + ConfigurationManager.AppSettings["ContactsPageUrl"] +"?api=yahoo";
        private string token;
        private string verifier;
        private string[] yahooRequestToken;
        private string[] yahooAccessToken;

        public string Tokensecret
        {
            get { return yahooRequestToken[1].Split('=')[1]; }
        }
        public string AccessToken
        {
            get { return yahooAccessToken[0].Split('=')[1]; }
        }
        public string AccessTokenSecret
        {
            get { return yahooAccessToken[1].Split('=')[1]; }
        }
        public string YGuid
        {
            get { return yahooAccessToken[5].Split('=')[1]; }
        }

        #region Authorization URL and Request Token
        /// <summary>
        /// Step 1
        /// </summary>
        /// <returns>Authorization URL</returns>
        public string GetAuthorizationURL()
        {
            string[] YahooRequestToken = GetYahooRequestToken();
            HttpContext.Current.Session["Yahoo_RequestToken"] = YahooRequestToken;
            return HttpUtility.UrlDecode(YahooRequestToken[3].Split('=')[1]);
        }

        /// <summary>
        /// Get the yahoo request token to begin the process
        /// </summary>
        /// <returns></returns>
        private string[] GetYahooRequestToken()
        {
            if (yahooRequestToken != null)
                return yahooRequestToken;

            string yahooOauth = "https://api.login.yahoo.com/oauth/v2/";
            yahooOauth += "get_request_token?oauth_nonce=" + Guid.NewGuid();
            yahooOauth += "&oauth_timestamp=" + Common.GetTimestamp();
            yahooOauth += "&oauth_consumer_key=" + apiKey;
            yahooOauth += "&oauth_signature_method=plaintext";
            yahooOauth += "&oauth_version=1.0";
            yahooOauth += "&oauth_callback=" + HttpUtility.UrlEncode(returnUrl);
            yahooOauth += "&oauth_signature=" + secret + "%26";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(yahooOauth);
            request.CookieContainer = new CookieContainer();
            request.Headers["WWW-Authenticate"] = " OAuth realm='" + appUrl + "'";
            request.Method = "GET";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader str = new StreamReader(response.GetResponseStream());
            return str.ReadToEnd().Split('&');
        }
        #endregion

        #region Access Token
        public void SetToken()
        {
            if (yahooAccessToken != null)
                return;
            if (HttpContext.Current.Request.QueryString["oauth_token"] != null)
                token = HttpContext.Current.Request.QueryString["oauth_token"];
            if (HttpContext.Current.Request.QueryString["oauth_verifier"] != null)
                verifier = HttpContext.Current.Request.QueryString["oauth_verifier"];
            yahooAccessToken = GetYahooAccessToken();
            HttpContext.Current.Session["Yahoo_AccessToken"] = yahooAccessToken;
            HttpContext.Current.Response.Redirect(contactsPageUrl);
        }

        private string[] GetYahooAccessToken()
        {
            yahooRequestToken = (string[])HttpContext.Current.Session["Yahoo_RequestToken"];
            string yahooOauth = "https://api.login.yahoo.com/oauth/v2/get_token?oauth_consumer_key=" + apiKey;
            yahooOauth += "&oauth_signature_method=PLAINTEXT";
            yahooOauth += "&oauth_version=1.0";
            yahooOauth += "&oauth_verifier=" + verifier;
            yahooOauth += "&oauth_token=" + token;
            yahooOauth += "&oauth_timestamp=" + Common.GetTimestamp();
            yahooOauth += "&oauth_nonce=" + Guid.NewGuid();
            yahooOauth += "&oauth_signature=" + secret + "%26" + Tokensecret;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(yahooOauth);

            request.CookieContainer = new CookieContainer();
            request.Headers["WWW-Authenticate"] = " OAuth realm='" + appUrl + "'";
            request.Method = "GET";
            request.ContentType = "application/xml; charset=utf-8";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader str = new StreamReader(response.GetResponseStream());
            return str.ReadToEnd().Split('&');
        }
        #endregion

        #region Get Contacts
        public IList<IContact> GetContacts(int pageNumber, int pageSize, out int totalCount)
        {
            IList<IContact> contacts = GetContacts(pageNumber, pageSize, null);
            totalCount = this.totalCount;
            return contacts;
        }

        public IList<IContact> GetContacts(ISelection selection)
        {
            return GetContacts(0, 0, selection);
        }

        public string Name
        {
            get { return "Yahoo"; }
        }

        public string FullName
        {
            get { return "ContactProvider.YahooProvider"; }
        }

        private IList<IContact> GetContacts(int pageNumber, int pageSize, ISelection selection)
        {
            HttpWebResponse contacts = GetContacts();
            StreamReader strcontacts = new StreamReader(contacts.GetResponseStream());
            string tmp = strcontacts.ReadToEnd();
            
            return new List<IContact>();
        }

        //TODO: No anda todavia!!
        private HttpWebResponse GetContacts()
        {
            yahooAccessToken = (string[])HttpContext.Current.Session["Yahoo_AccessToken"];
            Uri RequestContactBaseUri = new Uri("http://social.yahooapis.com/v1/user/" + YGuid + "/contacts");
            int timestamp = Common.GetTimestamp();

            OAuthParameters parameters = new OAuthParameters();
            parameters.ConsumerKey = apiKey;
            parameters.Nonce = new GuidNonceProvider().GenerateNonce(timestamp);
            parameters.SignatureMethod = "HMAC-SHA1";
            parameters.Timestamp = timestamp.ToString(CultureInfo.InvariantCulture);
            parameters.Token = Rfc3986.Decode(AccessToken);
            parameters.Version = "1.0";
            parameters.AdditionalParameters.Add("format", "xml");

            string sigBase = SignatureBase.Create("GET", RequestContactBaseUri, parameters);
            HmacSha1SigningProvider singProvier = new HmacSha1SigningProvider();
            parameters.Signature = singProvier.ComputeSignature(
            sigBase, (secret), Rfc3986.Encode(AccessTokenSecret));

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://social.yahooapis.com/v1/user/" + YGuid + "/contacts?view=tinyusercard");

            request.CookieContainer = new CookieContainer();
            request.Headers["WWW-Authenticate"] = " OAuth realm='yahooapis.com',";
            request.Headers["WWW-Authenticate"] += " oauth_consumer_key='" + parameters.ConsumerKey + "',";
            request.Headers["WWW-Authenticate"] += " oauth_nonce='" + parameters.Nonce + "',";
            request.Headers["WWW-Authenticate"] += " oauth_signature_method='" + parameters.SignatureMethod + "',";
            request.Headers["WWW-Authenticate"] += " oauth_timestamp='" + parameters.Timestamp + "',";
            request.Headers["WWW-Authenticate"] += " oauth_token='" + token + "',";
            request.Headers["WWW-Authenticate"] += " oauth_version='" + parameters.Version + "',";
            request.Headers["WWW-Authenticate"] += " oauth_signature='" + parameters.Signature + "'";
            request.Method = "GET";
            request.ContentType = "application/xml; charset=utf-8";

            return (HttpWebResponse)request.GetResponse();
        }
        #endregion

    }
}
