using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Security;
using FashionAde.Core;
using FashionAde.Core.Accounts;
using FashionAde.Core.Clothing;
using FashionAde.Core.DataInterfaces;
using FashionAde.Web.Controllers.MVCInteraction;
using SharpArch.Web.NHibernate;
using FashionAde.Web.Common;
using FashionAde.ApplicationServices;

namespace FashionAde.Web.Controllers.BuildYourCloser
{
    [HandleError]
    public class RegistrationController : BuildYourClosetController
    {
        private IUserSizeRepository userSizeRepository;
        private ISecurityQuestionRepository securityQuestionRepository;
        private IRegisterMemberService registerMemberService;
        private IGarmentRepository garmentRepository;

        public RegistrationController(IGarmentRepository garmentRepository, IRegisterMemberService registerMemberService, IUserSizeRepository userSizeRepository, ISecurityQuestionRepository securityQuestionRepository)
        {
            this.userSizeRepository = userSizeRepository;
            this.securityQuestionRepository = securityQuestionRepository;
            this.registerMemberService = registerMemberService;
            this.garmentRepository = garmentRepository;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ViewResult Index()
        {
            if (ClosetState.AddGarments == null)
                Response.Redirect(Url.RouteUrl(new { controller = "FlavorSelect", action = "Index" }));

            UserRegistration userReg = new UserRegistration();
            GetRegistrationInfo(userReg);
            return View(userReg);
        }

        private void GetRegistrationInfo(UserRegistration userRegistration)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            IList<SecurityQuestion> questions = securityQuestionRepository.GetAll();
            foreach (SecurityQuestion question in questions)
            {
                SelectListItem sl = new SelectListItem();
                sl.Text = question.Description;
                sl.Value = question.Id.ToString();
                if (userRegistration != null && userRegistration.SecurityQuestion.HasValue
                    && userRegistration.SecurityQuestion == question.Id)
                    sl.Selected = true;
                lst.Add(sl);
            }
            ViewData["securityQuestions"] = lst;

            List<SelectListItem> results = new List<SelectListItem>();
            IList<UserSize> userSizes = userSizeRepository.GetAll();
            foreach (UserSize userSize in userSizes)
            {
                SelectListItem sli = new SelectListItem();
                sli.Text = userSize.Description;
                sli.Value = userSize.Id.ToString();
                if (userRegistration != null && userRegistration.UserSize.HasValue
                    && userRegistration.UserSize == userSize.Id)
                    sli.Selected = true;
                results.Add(sli);
            }
            ViewData["UserSizes"] = results;
        }

        [Transaction]
        public ActionResult Register(UserRegistration userRegistration)
        {
            if (ModelState.IsValid)
            {
                IList<UserFlavor> userFlavors = ClosetState.UserFlavors as List<UserFlavor>;
                IList<EventType> eventTypes = ClosetState.EventTypes as List<EventType>;
                IList<Garment> mygarments = garmentRepository.GetByIds(ClosetState.AddGarments) as List<Garment>;
                IList<Garment> mywishlist = garmentRepository.GetByIds(ClosetState.WishGarments) as List<Garment>;

                SecurityQuestion sq = securityQuestionRepository.Get(Convert.ToInt32(userRegistration.SecurityQuestion));

                // Create Membership User
                MembershipCreateStatus status;
                MembershipUser mu = Membership.CreateUser(userRegistration.UserName, userRegistration.Password, userRegistration.Email, sq.Description, userRegistration.SecurityAnswer, false, out status);
                if (status != MembershipCreateStatus.Success)
                    ViewData["Errors"] = new string[] { status.ToString() };

#if !DEBUG
                string invitationCode = userRegistration.InvitationCode;
#else
                string invitationCode = string.Empty;
#endif
                try
                {
                    registerMemberService.RegisterMember(userRegistration.Email,
                        userRegistration.UserName,
                        userRegistration.Password,
                        new UserSize(Convert.ToInt32(userRegistration.UserSize)),
                        Convert.ToInt32(mu.ProviderUserKey),
                        userRegistration.ZipCode,
                        userFlavors,
                        eventTypes,
                        mygarments,
                        mywishlist,
                        Url.Action("Validate", "EmailConfirmation"),
                        invitationCode);

                    // Assign User Role
                    Roles.AddUserToRole(mu.UserName, "User");

                    ClosetState.Clear();

                    return RedirectToAction("Index", "EmailConfirmation", new { userid = mu.ProviderUserKey }); 
                }
                catch(Exception ex) 
                {
                    // Try to delete the incomplete created user because something went wrong.
                    try { Membership.DeleteUser(userRegistration.UserName); }
                    catch { }

                    if (ex is InvalidInvitationCodeException)
                        ModelState.AddModelError("InvitationCode", "The code is not valid or already used.");
                    else
                        throw ex;
                }
            }
            else
            {
                if(new List<ModelState>(ModelState.Values).Find(e => e.Value == null).Errors[0].ErrorMessage.StartsWith("Email"))
                    ModelState.AddModelError("Email", "Email does not match.");
                else
                    ModelState.AddModelError("Password", "Password does not match.");
            }
            
            GetRegistrationInfo(userRegistration);
            return View("Index", userRegistration);
        }
    }
}
