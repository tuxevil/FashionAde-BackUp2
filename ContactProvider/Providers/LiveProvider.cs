using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Xml;
using ContactProvider.Helpers;
using ContactProvider.Classes;

namespace ContactProvider
{
    public class LiveProvider : IContactProvider
    {
        const string AuthCookie = "delauthtoken";
        static DateTime ExpireCookie = DateTime.Now.AddYears(-10);
        static DateTime PersistCookie = DateTime.Now.AddYears(10);
        private HttpCookie authCookie;
        private int TotalCount;
        private string contactsPageUrl = ConfigurationManager.AppSettings["SiteURL"] + ConfigurationManager.AppSettings["ContactsPageUrl"] + "?api=live";
        private WindowsLiveLogin WindowsLiveLogin = new WindowsLiveLogin(true);

        /// <summary>
        /// Step 1
        /// </summary>
        /// <returns>Authorization URL</returns>
        public string GetAuthorizationURL()
        {
            return WindowsLiveLogin.GetConsentUrl("Contacts.Invite", string.Empty, WindowsLiveLogin.ReturnUrl);
        }

        /// <summary>
        /// Step 2: Read the context and get the token.
        /// Then process consent and get the ConsentToken.
        /// Then set the cookie.
        /// </summary>
        /// <param name="context">The context of the application</param>
        public void SetToken()
        {
            // Extract the 'action' parameter, if any, from the request.
            string action = HttpContext.Current.Request["action"];

            if (action == "delauth")
            {
                //Attempt to extract the consent token from the response.
                WindowsLiveLogin.ConsentToken token = WindowsLiveLogin.ProcessConsent(HttpContext.Current.Request.Form);

                HttpCookie authCookietmp = new HttpCookie(AuthCookie);
                // If a consent token is found, store it in the cookie
                if (token != null)
                {
                    authCookietmp.Value = token.Token;
                    authCookietmp.Expires = PersistCookie;
                }
                else
                {
                    authCookietmp.Expires = ExpireCookie;
                }

                HttpContext.Current.Response.Cookies.Add(authCookietmp);
                HttpContext.Current.Response.Redirect(contactsPageUrl);
                HttpContext.Current.Response.End();
            }
            else
            {
                HttpContext.Current.Response.End();
            }
        }

        /// <summary>
        /// Step 3a: Get the contact list for display, and/or selection
        /// </summary>
        /// <param name="pageNumber">Number of the page</param>
        /// <param name="pageSize">Size of the page</param>
        /// <param name="totalCount">Return of the total count of contacts</param>
        /// <returns>List of contacts</returns>
        public IList<IContact> GetContacts(int pageNumber, int pageSize, out int totalCount)
        {
            IList<IContact> contacts = GetContacts(pageNumber, pageSize, null);
            totalCount = this.TotalCount;
            return contacts;
        }

        /// <summary>
        /// Step 3b: Get the selected contact list for importation
        /// </summary>
        /// <param name="selectedIndexs">List of indexs that were selected by user</param>
        /// <returns>List of contacts</returns>
        public IList<IContact> GetContacts(ISelection selection)
        {
            return GetContacts(0, 0, selection);
        }

        public string Name
        {
            get { return "Live"; }
        }

        public string FullName
        {
            get { return "ContactProvider.LiveProvider"; }
        }

        private IList<IContact> GetContacts(int pageNumber, int pageSize, ISelection selection)
        {
            HttpWebResponse response;
            authCookie = HttpContext.Current.Request.Cookies["delauthtoken"];
            IList<IContact> result = new List<IContact>();
            if (authCookie != null)
            {
                string t = authCookie.Value;
                WindowsLiveLogin.ConsentToken Token = WindowsLiveLogin.ProcessConsentToken(t);

                if ((Token != null) && Token.IsValid())
                {
                    ServicePointManager.ServerCertificateValidationCallback +=
                        delegate(object sender, X509Certificate certificate, X509Chain chain,
                                 SslPolicyErrors sslPolicyErrors)
                            {
                                bool validationResult = true;
                                return validationResult;
                            };

                    UriBuilder uriBuilder = new UriBuilder();
                    uriBuilder.Scheme = "HTTPS";
                    uriBuilder.Path = "/users/@L@" + Token.LocationID + "/rest/invitationsbyemail";
                    uriBuilder.Host = "livecontacts.services.live.com";
                    uriBuilder.Port = 443;
                    string uriPath = uriBuilder.Uri.AbsoluteUri;

                    response = SendHttpRequest(Token, ref uriPath, "GET");

                    if (response != null && response.ContentLength != 0)
                    {
                        XmlDocument doc = new XmlDocument();
                        XmlTextReader reader = new XmlTextReader(response.GetResponseStream());
                        reader.Read();
                        // load reader 
                        doc.Load(reader);
                        int i = 1;
                        if (pageNumber > 0)
                        {
                            foreach (XmlNode node in doc.SelectNodes(@"/LiveContacts/Contacts/Contact"))
                            {
                                if (i < (pageNumber*pageSize) - (pageSize - 1))
                                {
                                    i++;
                                    continue;
                                }
                                if (i > (pageNumber*pageSize))
                                    break;
                                GetContact(i++, node, result);
                            }
                            TotalCount = doc.SelectNodes(@"/LiveContacts/Contacts/Contact").Count;
                        }
                        else if (selection != null)
                        {
                            foreach (XmlNode node in doc.SelectNodes(@"/LiveContacts/Contacts/Contact"))
                            {
                                if (!selection.SelectedAll && !selection.SelectedIndexs.Exists(delegate(int record) { if (record == i) { return true; } return false; }))
                                {
                                    i++;
                                    continue;
                                }
                                if (selection.SelectedAll && selection.SelectedIndexs.Exists(delegate(int record) { if (record == i) { return true; } return false; }))
                                {
                                    i++;
                                    continue;
                                }

                                Contact contact = new Contact();
                                contact.Index = i;
                                if (node.ChildNodes.Count > 0 && node.ChildNodes[0].ChildNodes.Count > 0 &&
                                    node.ChildNodes[0].ChildNodes[0].ChildNodes.Count > 0)
                                    contact.FirstName = node.ChildNodes[0].ChildNodes[0].ChildNodes[0].InnerText;
                                if (node.ChildNodes.Count > 0 && node.ChildNodes[0].ChildNodes.Count > 0 &&
                                    node.ChildNodes[0].ChildNodes[0].ChildNodes.Count > 1)
                                    contact.LastName = node.ChildNodes[0].ChildNodes[0].ChildNodes[1].InnerText;
                                if (node.ChildNodes.Count > 1)
                                    contact.Email = node.ChildNodes[1].InnerText;
                                result.Add(contact);
                                i++;
                            }
                        }
                        else
                        {
                            foreach (XmlNode node in doc.SelectNodes(@"/LiveContacts/Contacts/Contact"))
                                GetContact(i++, node, result);
                            TotalCount = doc.SelectNodes(@"/LiveContacts/Contacts/Contact").Count;
                        }
                    }
                    response.Close();
                }
            }
            return result;
        }

        private int GetContact(int i, XmlNode node, IList<IContact> result)
        {
            if (node.ChildNodes.Count > 1 && node.ChildNodes[1].InnerText == null)
                return i;
            Contact contact = new Contact();
            contact.Index = i;
            if (node.ChildNodes.Count > 0 && node.ChildNodes[0].ChildNodes.Count > 0 &&
                node.ChildNodes[0].ChildNodes[0].ChildNodes.Count > 0)
                contact.FirstName = node.ChildNodes[0].ChildNodes[0].ChildNodes[0].InnerText;
            if (node.ChildNodes.Count > 0 && node.ChildNodes[0].ChildNodes.Count > 0 &&
                node.ChildNodes[0].ChildNodes[0].ChildNodes.Count > 1)
                contact.LastName = node.ChildNodes[0].ChildNodes[0].ChildNodes[1].InnerText;
            contact.Email = node.ChildNodes[1].InnerText;
            result.Add(contact);
            return i;
        }

        private HttpWebResponse SendHttpRequest(WindowsLiveLogin.ConsentToken Token, ref string uriPath, string strRequestMethod)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uriPath);

            request.Headers["Authorization"] = "DelegatedToken dt=\"" + Token.DelegationToken + "\"";
            request.Headers["Pragma"] = "No-Cache";
            request.CookieContainer = new CookieContainer();
            request.Method = strRequestMethod;
            request.ContentType = "application/xml; charset=utf-8";

            return (HttpWebResponse)request.GetResponse();
        }
    }
}
