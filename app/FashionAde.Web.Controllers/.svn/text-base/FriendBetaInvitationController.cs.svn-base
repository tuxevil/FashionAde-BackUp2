using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using FashionAde.Web.Controllers.MVCInteraction;
using FashionAde.ApplicationServices;
using FashionAde.Core.Accounts;
using SharpArch.Web.NHibernate;

namespace FashionAde.Web.Controllers
{
    [Authorize]
    public class FriendBetaInvitationController : BaseController
    {
        private IInviteContactsService inviteContactService;

        public FriendBetaInvitationController(IInviteContactsService inviteContactService)
        {
            this.inviteContactService = inviteContactService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            BetaInvitation bi = new BetaInvitation();
            bi.Emails = new BetaInvitationEmail[5];
            return View(bi);
        }


        [HttpPost]
        [Transaction]
        public ActionResult Index(BetaInvitation bi)
        {
            if (ModelState.IsValid)
            {
                IList<IBasicUser> contacts = TransformContacts(bi.Emails);
                if (contacts.Count != 0)
                {
                    try
                    {
                        inviteContactService.InviteUsers(this.ProxyLoggedUser, bi.Comments, null, contacts, true);
                        TempData["Message"] = "Thanks for inviting your friends. Enjoy Fashion-Ade.com!";
                        return RedirectToAction("Index", "Home");
                    }
                    catch (LimitOfFriendsExceededException ex)
                    {
                        ModelState.AddModelError("FriendExceeded", ex.Message);
                    }
                }
                else
                    ModelState.AddModelError("Empty", "At least one contact must be supplied.");
            }

            return View(bi);
        }

        private IList<IBasicUser> TransformContacts(BetaInvitationEmail[] contacts)
        {
            List<IBasicUser> lst = new List<IBasicUser>();
            foreach (BetaInvitationEmail mail in contacts)
                if (!string.IsNullOrEmpty(mail.EmailAddress))
                    lst.Add(new UserContact() { EmailAddress = mail.EmailAddress });
            return lst;
        }


    }
}
