using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Security;
using FashionAde.Core.Accounts;
using FashionAde.Core.DataInterfaces;
using FashionAde.Web.Common;

namespace FashionAde.Web.Controllers
{
    public class AccountValidationController : BaseController
    {
        private IRegisteredUserRepository registeredUserRepository;
        private IZipCodeRepository zipCodeRepository;

        public AccountValidationController(IRegisteredUserRepository registeredUserRepository, IZipCodeRepository zipCodeRepository)
        {
            this.registeredUserRepository = registeredUserRepository;
            this.zipCodeRepository = zipCodeRepository;
        }

        [ObjectFilter(Param = "email", RootType = typeof(string))]
        public ActionResult CheckEmail(string email)
        {
            Regex regex = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");
            if (regex.IsMatch(email))
            {
                if (registeredUserRepository.GetByMail(email) == null)
                    return Json(new
                    {
                        Exist = false,
                        RegExError = false,
                        Email = email
                    });
                return Json(new
                {
                    Exist = true,
                    RegExError = false,
                    Email = email
                });
            }
            return Json(new
            {
                Exist = true,
                RegExError = true,
                Email = email
            });
        }

        [ObjectFilter(Param = "username", RootType = typeof(string))]
        public ActionResult CheckUsername(string username)
        {
            if (IsReserved(username))
                return Json(new
                {
                    Exist = false,
                    Reserved = true,
                    Username = username
                });
            string user = CheckUser(username);
            if (user == username)
                return Json(new
                {
                    Exist = false,
                    Reserved = false,
                    Username = username
                });


            return Json(new
            {
                Exist = true,
                Username = username,
                Recommended = user
            });
        }

        [ObjectFilter(Param = "code", RootType = typeof(string))]
        public ActionResult CheckZipCode(string code)
        {
            ZipCode zipcode = zipCodeRepository.GetByCode(code);

            if (zipcode != null)
                return Json(new
                {
                    Exist = true
                });
            return Json(new
            {
                Exist = false,
                ZipCode = code
            });
        }

        private string CheckUser(string username)
        {
            if (Membership.GetUser(username) != null)
            {
                Random r = new Random();
                return CheckUser(username + r.Next(99));
            }
            return username;
        }

        private bool IsReserved(string username)
        {
            foreach (string reservedUsername in ReservedUsernames)
                if (reservedUsername.ToLower() == username.ToLower())
                    return true;
            return false;
        }

        private string[] ReservedUsernames
        {
            get { return "Administrator,Administrador,Administra,Administro,Admin,Adm".Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries); }
        }
    }
}
