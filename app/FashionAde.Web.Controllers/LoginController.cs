using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;
using FashionAde.Core;
using FashionAde.Core.DataInterfaces;
using FashionAde.Web.Controllers.MVCInteraction;
using FashionAde.Core.Accounts;
using System.Web;
using FashionAde.Web.Common;
using FashionAde.ApplicationServices;
using SharpArch.Web.NHibernate;

namespace FashionAde.Web.Controllers
{
    [HandleError]
    public class LoginController : BaseController
    {
        IRegisteredUserRepository registeredUserRepository;
        IMessageSenderService messageSender;

        public LoginController(IRegisteredUserRepository registeredUserRepository, IMessageSenderService messageSender)
        {
            this.registeredUserRepository = registeredUserRepository;
            this.messageSender = messageSender;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ViewResult Index(string validatedUser)
        {
            ViewData["Errors"] = TempData["Errors"];
            TempData["Errors"] = null;
            ViewData["validatedUser"] = !string.IsNullOrEmpty(validatedUser) && Convert.ToBoolean(validatedUser);

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public RedirectToRouteResult Validate(string userName, string userPassword, bool? chkMaintain)
        {
            bool firstTime = false;
            MembershipUser mu = Membership.GetUser(userName);
            if (mu != null && mu.CreationDate == mu.LastLoginDate) {
                firstTime = true;
            }

            if (Membership.ValidateUser(userName, userPassword))
            {
                bool tmp = false;
                if (chkMaintain != null)
                    tmp = (bool) chkMaintain;

                FormsAuthentication.SetAuthCookie(userName, tmp);
                UserDataHelper.LoadFromDatabase(userName);

                // Clear closet state when the user logs in.
                new BuildYourClosetState().Clear();

                if (firstTime)
                    return RedirectToAction("Index", "FriendBetaInvitation");
                else
                    return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["Errors"] = "The username and password supplied are not valid.";
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpGet]
        public RedirectToRouteResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }



        [ObjectFilter(Param = "username", RootType = typeof(string))]
        public ActionResult GetQuestion(string username)
        {
            MembershipUser mu = Membership.GetUser(username);
            if(mu != null)
            {
                return Json(new
                                {
                                    Exist = true,
                                    PasswordQuestion = mu.PasswordQuestion
                                });
                
            }

            return Json(new
            {
                Exist = false
            });
        }

        #region Forgot Password

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            ViewData["Errors"] = TempData["Errors"];
            TempData["Errors"] = null;

            return View(new ForgotPasswordView());
        }

        [HttpPost]
        [Transaction]
        public ActionResult ForgotPassword(ForgotPasswordView view)
        {
            if (ModelState.IsValid)
            {
                MembershipUser mu = Membership.GetUser(view.UserName);

                if (mu != null)
                {
                    try
                    {
                        string newPassword = mu.ResetPassword(view.Answer);

                        PasswordEmailData ped = new PasswordEmailData();
                        ped.UserName = mu.UserName;
                        ped.NewPassword = newPassword;
                        messageSender.SendWithTemplate("forgotpassword", null, ped, mu.Email);

                        TempData["Errors"] = "Your new password has been sent by email, please check your inbox.";
                        return RedirectToAction("ForgotPassword");

                    }
                    catch (MembershipPasswordException)
                    {
                        ModelState.AddModelError("Answer", "Your answer does not match with our records.");
                    }
                }
                else
                    ModelState.AddModelError("UserName", "Your user name is not valid.");
            }
           
            return View(view);
        }

        #endregion
    }
}
