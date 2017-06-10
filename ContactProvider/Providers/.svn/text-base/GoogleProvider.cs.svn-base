using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using ContactProvider.Classes;
using Google.Contacts;
using Google.GData.Client;
using Contact=ContactProvider.Classes.Contact;
using System.Web;

namespace ContactProvider
{
    public class GoogleProvider : IContactProvider
    {
        private int totalCount;
        private object token = HttpContext.Current.Session["Google_Token"];
        private string applicationName = ConfigurationManager.AppSettings["Google_ApplicationName"];
        private string returnUrl = ConfigurationManager.AppSettings["SiteURL"] + ConfigurationManager.AppSettings["Google_ReturnUrl"];
        private string contactsPageUrl = ConfigurationManager.AppSettings["SiteURL"] + ConfigurationManager.AppSettings["ContactsPageUrl"] + "?api=google";

        /// <summary>
        /// Step 1
        /// </summary>
        /// <returns>Authorization URL</returns>
        public string GetAuthorizationURL()
        {
            return AuthSubUtil.getRequestUrl(returnUrl, "http://www.google.com/m8/feeds/", false, true);
        }

        /// <summary>
        /// Step 2: Read the context and get the token in the query string.
        /// Then exchange it for the Session Token.
        /// </summary>
        /// <param name="context">The context of the application</param>
        public void SetToken()
        {
            this.token = AuthSubUtil.exchangeForSessionToken(HttpContext.Current.Request.QueryString["token"], null);
            HttpContext.Current.Session["Google_Token"] = this.token;
            HttpContext.Current.Response.Redirect(contactsPageUrl);
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
            totalCount = this.totalCount;
            return contacts;
        }

        /// <summary>
        /// Step 3b: Get the selected contact list for importation
        /// </summary>
        /// <param name="selection">List of indexs that were selected by user</param>
        /// <returns>List of contacts</returns>
        public IList<IContact> GetContacts(ISelection selection)
        {
            return GetContacts(0, 0, selection);
        }

        public string Name
        {
            get { return "Google"; }
        }

        public string FullName
        {
            get { return "ContactProvider.GoogleProvider"; }
        }

        private IList<IContact> GetContacts(int pageNumber, int pageSize, ISelection selection)
        {
            IList<IContact> result = new List<IContact>();
            RequestSettings rs = new RequestSettings(applicationName, token.ToString());
            rs.AutoPaging = true;
            ContactsRequest cr = new ContactsRequest(rs);

            Feed<Google.Contacts.Contact> contacts = cr.GetContacts();
            int i = 1;
            if (pageNumber > 0)
            {
                foreach (Google.Contacts.Contact e in contacts.Entries)
                {
                    if (i < (pageNumber * pageSize) - (pageSize - 1))
                    {
                        i++;
                        continue;
                    }
                    if (i > (pageNumber * pageSize))
                        break;

                    GetContact(i++, result, e);
                }

                this.totalCount = contacts.TotalResults;
            }
            else if (selection != null)
            {
                foreach (Google.Contacts.Contact e in contacts.Entries)
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
                    if (e.PrimaryEmail == null)
                        continue;

                    Contact contact = new Contact();
                    contact.Index = i;
                    contact.FirstName = e.Title;
                    contact.Email = e.PrimaryEmail.Address;
                    result.Add(contact);
                    i++;
                }
            }
            else
            {
                foreach (Google.Contacts.Contact e in contacts.Entries)
                    GetContact(i++, result, e);

                this.totalCount = contacts.TotalResults;
            }

            return result;
        }

        private void GetContact(int i, IList<IContact> result, Google.Contacts.Contact e)
        {
            if (e.PrimaryEmail == null)
                return;
            Contact contact = new Contact();
            contact.Index = i;
            contact.FirstName = e.Title;
            contact.Email = e.PrimaryEmail.Address;
            result.Add(contact);
        }
    }
}
