using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using System.Xml;
using System.Xml.XPath;
using ContactProvider;
using FashionAde.ApplicationServices;
using FashionAde.Core;
using FashionAde.Core.Accounts;
using FashionAde.Core.DataInterfaces;
using FashionAde.Web.Controllers.MVCInteraction;
using SharpArch.Web.NHibernate;
using FashionAde.Web.Common;

namespace FashionAde.Web.Controllers
{
    [HandleError]
    [Authorize]
    public class AccountController : BaseController
    {
        private ISecurityQuestionRepository securityQuestionRepository;
        private IRegisteredUserRepository registeredUserRepository;
        private IRegisterMemberService registerMemberService;
        private IUserSizeRepository userSizeRepository;

        
        public AccountController(ISecurityQuestionRepository securityQuestionRepository, IRegisteredUserRepository registeredUserRepository, IUserSizeRepository userSizeRepository, IRegisterMemberService registerMemberService)
        {
            this.securityQuestionRepository = securityQuestionRepository;
            this.registeredUserRepository = registeredUserRepository;
            this.userSizeRepository = userSizeRepository;
            this.registerMemberService = registerMemberService;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ViewResult Index()
        {
            UserFull userModification = new UserFull();
            userModification = GetAccountData(userModification);
            
            return View(userModification);
        }

        private UserFull GetAccountData(UserFull userModification)
        {
            MembershipUser mu = Membership.GetUser();
            List<SelectListItem> securityQuestionList = new List<SelectListItem>();
            List<SelectListItem> userSizeList = new List<SelectListItem>();
            List<SelectListItem> privacyList = new List<SelectListItem>();
            if (mu != null)
            {
                RegisteredUser ru = registeredUserRepository.GetByMembershipId(Convert.ToInt32(mu.ProviderUserKey));
                SecurityQuestion securityQuestion = securityQuestionRepository.GetByDescription(mu.PasswordQuestion);
                userModification = new UserFull(ru, securityQuestion.Id, userModification.Alert, userModification.Tab);
                
                IList<SecurityQuestion> questions = securityQuestionRepository.GetAll();
                foreach (SecurityQuestion question in questions)
                {
                    SelectListItem sl = new SelectListItem();
                    sl.Text = question.Description;
                    sl.Value = question.Id.ToString();
                    if (question.Description == securityQuestion.Description)
                        sl.Selected = true;
                    securityQuestionList.Add(sl);
                }

                IList<UserSize> userSizes = userSizeRepository.GetAll();
                foreach (UserSize userSize in userSizes)
                {
                    SelectListItem sli = new SelectListItem();
                    sli.Text = userSize.Description;
                    sli.Value = userSize.Id.ToString();
                    if (userSize.Id == ru.Size.Id)
                        sli.Selected = true;
                    userSizeList.Add(sli);
                }

                SelectListItem pvy = new SelectListItem();
                pvy.Text = "My Closet can be viewed by me only";
                pvy.Value = PrivacyLevel.Private.ToString();
                if (ru.Closet.PrivacyLevel == PrivacyLevel.Private)
                    pvy.Selected = true;
                privacyList.Add(pvy);
                pvy = new SelectListItem();
                pvy.Text = "My Closet can be viewed by me and my friends";
                pvy.Value = PrivacyLevel.Friends.ToString();
                if (ru.Closet.PrivacyLevel == PrivacyLevel.Friends)
                    pvy.Selected = true;
                privacyList.Add(pvy);
                pvy = new SelectListItem();
                pvy.Text = "Only my Signature Outfit can be viewed by anyone";
                pvy.Value = PrivacyLevel.FavoriteOutfit.ToString();
                if (ru.Closet.PrivacyLevel == PrivacyLevel.FavoriteOutfit)
                    pvy.Selected = true;
                privacyList.Add(pvy);
                pvy = new SelectListItem();
                pvy.Text = "My Entire Closet can be viewed by anyone";
                pvy.Value = PrivacyLevel.FullCloset.ToString();
                if (ru.Closet.PrivacyLevel == PrivacyLevel.FullCloset)
                    pvy.Selected = true;
                privacyList.Add(pvy);
            }

            ViewData["SecurityQuestions"] = securityQuestionList;
            ViewData["UserSizes"] = userSizeList;
            ViewData["PrivacyStatus"] = privacyList;
            if (mu != null)
                ViewData["UserName"] = mu.UserName;
            
            return userModification;
        }

        [Transaction]
        public ViewResult ChangeInfo(UserFull userModification)
        {
            if (ModelState.IsValid)
            {
                MembershipUser mu = Membership.GetUser();
                RegisteredUser ru = (RegisteredUser)registeredUserRepository.GetByMembershipId(Convert.ToInt32(mu.ProviderUserKey));
                userModification.Email = ru.EmailAddress;
                ru.FirstName = userModification.FirstName;
                ru.LastName = userModification.LastName;
                ru.PhoneNumber = string.Empty;
                ru.ChangeZipCode(userModification.ZipCode);
                ru.Size = userSizeRepository.Get(userModification.UserSize);

                if (userModification.PrivacyStatus == PrivacyLevel.Private.ToString())
                    ru.Closet.PrivacyLevel = PrivacyLevel.Private;
                if (userModification.PrivacyStatus == PrivacyLevel.Friends.ToString())
                    ru.Closet.PrivacyLevel = PrivacyLevel.Friends;
                if (userModification.PrivacyStatus == PrivacyLevel.FavoriteOutfit.ToString())
                    ru.Closet.PrivacyLevel = PrivacyLevel.FavoriteOutfit;
                if (userModification.PrivacyStatus == PrivacyLevel.FullCloset.ToString())
                    ru.Closet.PrivacyLevel = PrivacyLevel.FullCloset;
                
                registeredUserRepository.SaveOrUpdate(ru);
                userModification.Alert = "User information updated successfully";
            }

            GetAccountData(userModification);
            return View("Index", userModification);
        }

        [Transaction]
        public ViewResult ChangeEmail(UserFull userModification)
        {
            if (ModelState.IsValid)
            {
                if (registeredUserRepository.GetByMail(userModification.Email) == null)
                {
                    MembershipUser mu = Membership.GetUser();
                    RegisteredUser ru = (RegisteredUser)registeredUserRepository.GetByMembershipId(Convert.ToInt32(mu.ProviderUserKey));
                    ru.NewMail = userModification.Email;
                    ru.RegistrationCode = Guid.NewGuid().ToString();
                    registeredUserRepository.SaveOrUpdate(ru);

                    registerMemberService.SendValidationCode(ru, Url.Action("Validate", "EmailConfirmation"));
                    Response.Redirect(Url.RouteUrl(new { controller = "EmailConfirmation", action = "Index", userid = mu.ProviderUserKey }));
                }
                else
                {
                    userModification.Alert = userModification.Email + " is in use";
                }
             }

            userModification.Tab = 1;
            userModification = GetAccountData(userModification);
            return View("Index", userModification);
        }

        public ViewResult ChangePassword(UserFull userModification, string OldPassword, string NewPassword, string ConfirmPassword)
        {
            UserPassword userPassword = new UserPassword(OldPassword, NewPassword, ConfirmPassword);

            if (ModelState.IsValid)
            {
                if (OldPassword != NewPassword)
                {
                    MembershipUser mu = Membership.GetUser();
                    if (mu.ChangePassword(userPassword.OldPassword, userPassword.NewPassword))
                        userModification.Alert = "User password changed successfully";
                    else
                        ModelState.AddModelError("OldPassword", "Old password is wrong");
                }
            }
            
            userModification.Tab = 2;
            userModification = GetAccountData(userModification);
            return View("Index", userModification);
        }

        public ViewResult ChangeAnswer(UserFull userModification)
        {
            UserAnswer userAnswer = new UserAnswer(userModification);
            if (ModelState.IsValid)
            {
                MembershipUser mu = Membership.GetUser();
                if (mu.ChangePasswordQuestionAndAnswer(userAnswer.Password, securityQuestionRepository.Get(userModification.SecurityQuestion).Description, userAnswer.SecurityAnswer))
                    userModification.Alert = "User security question and answer changed successfully";
                else
                    ModelState.AddModelError("Password", "The password dont match");
            }

            userModification.Tab = 3;
            userModification = GetAccountData(userModification);
            return View("Index", userModification);
        }
    }


    public class PasswordEmailData
    {
        public string UserName { get; set; }
        public string NewPassword { get; set; }
    }
}