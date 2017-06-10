using System.Web.Mvc;
using System.Web.Security;

namespace FashionAde.Web.Controllers
{
    [HandleError]
    public class LoginController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }

        public RedirectToRouteResult Validate(string userName, string userPassword, bool? chkMaintain)
        {
            if (Membership.ValidateUser(userName, userPassword) && !Roles.IsUserInRole(userName, "User"))
            {
                bool tmp = false;
                if (chkMaintain != null)
                    tmp = (bool) chkMaintain;

                FormsAuthentication.SetAuthCookie(userName, tmp);
                return RedirectToAction("Index", "Grid");
            }
            
            TempData["LoginMessage"] = "The username and password supplied are not valid.";
            return RedirectToAction("Index", "Login");
        }

        public RedirectToRouteResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}
