using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using ContactProvider;
using ContactProvider.Classes;
using FashionAde.Core.Accounts;
using FashionAde.Core.DataInterfaces;
using SharpArch.Web.NHibernate;
using FashionAde.Web.Common;
using FashionAde.ApplicationServices;

namespace FashionAde.Web.Controllers
{
    [HandleError]
    [Authorize]
    public class ContactController : BaseController
    {
        private IInviteContactsService inviteContactsService;
        private IContactProvider contactProvider;
        private IFriendProviderRepository friendProviderRepository;
        private IFriendRepository friendRepository;

        public ContactController(IFriendProviderRepository friendProviderRepository, 
            IInviteContactsService inviteContactsService,
            IFriendRepository friendRespository)
        {
            this.friendProviderRepository = friendProviderRepository;
            this.inviteContactsService = inviteContactsService;
            this.friendRepository = friendRespository;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ViewResult Index(int? page)
        {
            if (string.IsNullOrEmpty(Request.QueryString["api"]))
                RedirectToAction("Index", "Home");

            contactProvider = ProviderFactory.GetProvider(Request.QueryString["api"]);
            int totalCount;

            // Find all friends
            IDictionary<string, object> propertyValues = new Dictionary<string, object>();
            propertyValues.Add("BasicUser", this.ProxyLoggedUser);
            IList<Friend> currentFriends = friendRepository.FindAll(propertyValues);

            IList<IContact> contacts = contactProvider.GetContacts(0, 0, out totalCount);
            Session["Contacts_" + Request.QueryString["api"]] = contacts;

            // Make sure the user is not a friend already
            IList<IContact> finalContacts = (from c in contacts
                                             where !(from f in currentFriends select f.User.EmailAddress).Contains(c.Email.ToLower())
                                 select c).ToList<IContact>();

            propertyValues = new Dictionary<string, object>();
            propertyValues.Add("BasicUser", this.ProxyLoggedUser);
            
            ViewData["TotalCount"] = totalCount;
            FriendProvider friendProvider = friendProviderRepository.GetByName(contactProvider.Name);
            ViewData["providerImg"] = friendProvider.ImageUri;
            ViewData["providerName"] = friendProvider.Name;
            ViewData["provider"] = Request.QueryString["api"];

            return View(finalContacts);
        }

        [Transaction]
        public ActionResult AddContacts(string selectedIndexs, string selectedAll, string emailmessage, string provider)
        {
            contactProvider = ProviderFactory.GetProvider(provider);
            string[] selected = selectedIndexs.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            bool all = selectedAll == "true";
            List<int> indexs = new List<int>();
            foreach(string index in selected)
                indexs.Add(Convert.ToInt32(index));
            
            IList<IContact> selectedContacts = new List<IContact>();
            if (Session["Contacts_" + provider] != null)
                selectedContacts = GetContacts((IList<IContact>)Session["Contacts_" + provider], new Selection(all, indexs));
            else
                selectedContacts = contactProvider.GetContacts(new Selection(all, indexs));

            FriendProvider fp = friendProviderRepository.GetByName(contactProvider.Name);

            inviteContactsService.InviteUsers(this.ProxyLoggedUser, emailmessage, fp, TransformContacts(selectedContacts), Convert.ToBoolean(ConfigurationManager.AppSettings["IsInBeta"]));

            Session.Remove("Contacts_" + provider);
            return RedirectToAction("Index", "Friend");
        }

        private IList<IContact> GetContacts(IList<IContact> allContacts, Selection selection)
        {
            IList<IContact> result = new List<IContact>();
            int i = 1;

            foreach (IContact e in allContacts)
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
                
                result.Add(e);
                i++;
            }
        
            return result;
        }

        private IList<IBasicUser> TransformContacts(IList<IContact> contacts)
        {
            List<IBasicUser> lst = new List<IBasicUser>(contacts.Count);
            foreach (IContact c in contacts)
                lst.Add(new UserContact() { FirstName = c.FirstName, LastName = c.LastName, EmailAddress = c.Email });
            return lst;
        }
    }
}
