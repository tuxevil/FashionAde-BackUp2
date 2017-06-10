using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Security;
using FashionAde.Core.Accounts;
using FashionAde.Utils.Validators;

namespace FashionAde.Web.Controllers.MVCInteraction
{
    public class UserFull
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }
        public string ZipCode { get; set; }
        public int UserSize { get; set; }
        public bool RememberMe { get; set; }
        public bool TermOfUse { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string PrivacyStatus { get; set; }
        public string Alert { get; set; }
        public int Tab { get; set; }
        
        public UserFull() {}
        public UserFull(RegisteredUser registeredUser, int securityQuestion, string alert, int tab)
        {
            this.Email = registeredUser.EmailAddress;
            this.FirstName = registeredUser.FirstName;
            this.LastName = registeredUser.LastName;
            this.SecurityQuestion = securityQuestion;
            this.PhoneNumber = registeredUser.PhoneNumber;
            this.UserSize = registeredUser.Size.Id;
            this.ZipCode = registeredUser.ZipCode;
            this.Alert = alert;
            this.Tab = tab;
        }
    }

    [PropertiesMatchAttribute("Password", "ConfirmPassword", ErrorMessage = "Password does not match.")]
    [EmailMatchAttribute("Email", "EmailConfirmation", ErrorMessage = "Email does not match.")]
    public class UserRegistration
    {
        [Required(ErrorMessage = "Email required")]
        [Email(ErrorMessage = "Email is invalid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Email confirmation required")]
        [Email(ErrorMessage = "Email is invalid")]
        public string EmailConfirmation { get; set; }

        [Required(ErrorMessage = "User Name required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password Confirmation required")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Security Question required")]
        public int? SecurityQuestion { get; set; }

        [Required(ErrorMessage = "Security Answer required")]
        public string SecurityAnswer { get; set; }

        [Required(ErrorMessage = "Zip Code required")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Invitation Code required")]
        public string InvitationCode { get; set; }

        [Required(ErrorMessage = "Terms of Use are required")]
        public bool TermOfUse { get; set; }

        public int? UserSize { get; set; }

        public UserRegistration() {}
        public UserRegistration(UserFull userFull)
        {
            this.ConfirmPassword = userFull.ConfirmPassword;
            this.Email = userFull.Email;
            this.Password = userFull.Password;
            this.SecurityAnswer = userFull.SecurityAnswer;
            this.SecurityQuestion = userFull.SecurityQuestion;
            this.UserName = userFull.UserName;
            this.UserSize = userFull.UserSize;
            this.ZipCode = this.ZipCode;
        }
        public UserRegistration(string userName)
        {
            this.UserName = userName;
        }
    }

    public class UserUpdate
    {
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name Required")]
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string PrivacyStatus { get; set; }
        [Required(ErrorMessage = "Zip Code Required")]
        public string ZipCode { get; set; }
        public string Email { get; set; }
        
        public UserUpdate(){}
        public UserUpdate(UserFull userFull)
        {
            this.FirstName = userFull.FirstName;
            this.LastName = userFull.LastName;
            this.PhoneNumber = userFull.PhoneNumber;
            this.PrivacyStatus = userFull.PrivacyStatus;
            this.ZipCode = userFull.ZipCode;
            this.Email = userFull.Email;
        }
    }

    public class UserAnswer
    {
        public int? SecurityQuestion { get; set; }
        [Required(ErrorMessage = "Security Answer Required")]
        public string SecurityAnswer { get; set; }
        [Required(ErrorMessage = "Password Required")]
        public string Password { get; set; }
        
        public UserAnswer() { }
        public UserAnswer(UserFull userFull)
        {
            this.SecurityQuestion = userFull.SecurityQuestion;
            this.Password = userFull.Password;
            this.SecurityAnswer = userFull.SecurityAnswer;
        }
    }

    public class UserEmail
    {
        [Required(ErrorMessage = "Email Required")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Email is invalid")]
        public string Email { get; set; }

        public UserEmail() {}
        public UserEmail(UserFull userFull)
        {
            this.Email = userFull.Email;
        }
        public UserEmail(UserRegistration userRegistration)
        {
            this.Email = userRegistration.Email;
        }
    }

    public class UserPassword
    {
        [Required(ErrorMessage = "Old Password Required")]
        public string OldPassword { get; set; }

        //[ValidateNotSameAs("OldPassword", "Same Password")]
        [Required(ErrorMessage = "New Password Required")]
        public string NewPassword { get; set; }

        //[ValidateSameAs("NewPassword", "Dont match")]
        [Required(ErrorMessage = "Password Confirmation Required")]
        public string ConfirmPassword { get; set; }
        
        public UserPassword() {}
        public UserPassword(UserFull userFull)
        {
            this.NewPassword = "";
            this.ConfirmPassword = "";
        }
        public UserPassword(UserRegistration userRegistration)
        {
            this.NewPassword = userRegistration.Password;
            this.ConfirmPassword = userRegistration.ConfirmPassword;
        }
        public UserPassword(string OldPassword, string NewPassword, string ConfirmPassword)
        {
            this.ConfirmPassword = ConfirmPassword;
            this.NewPassword = NewPassword;
            this.OldPassword = OldPassword;
        }
    }
}
