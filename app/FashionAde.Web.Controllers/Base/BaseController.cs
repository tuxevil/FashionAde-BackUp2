using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using FashionAde.Core.Accounts;
using FashionAde.Core;
using System.Web;
using FashionAde.Data.Repository;
using System.Reflection;

namespace FashionAde.Web.Controllers
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class BypassAntiForgeryTokenAttribute : ActionFilterAttribute { }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class UseAntiForgeryTokenOnPostByDefault : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (ShouldValidateAntiForgeryTokenManually(filterContext))
            {
                var authorizationContext = new AuthorizationContext(filterContext.Controller.ControllerContext, filterContext.ActionDescriptor);

                //Use the authorization of the anti forgery token, 
                //which can't be inhereted from because it is sealed
                new ValidateAntiForgeryTokenAttribute().OnAuthorization(authorizationContext);
            }

            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// We should validate the anti forgery token manually if the following criteria are met:
        /// 1. The http method must be POST
        /// 2. There is not an existing [ValidateAntiForgeryToken] attribute on the action
        /// 3. There is no [BypassAntiForgeryToken] attribute on the action
        /// </summary>
        private static bool ShouldValidateAntiForgeryTokenManually(ActionExecutingContext filterContext)
        {
            var httpMethod = filterContext.HttpContext.Request.HttpMethod;

            //1. The http method must be POST
            if (httpMethod != "POST") return false;

            // 2. There is not an existing anti forgery token attribute on the action
            var antiForgeryAttributes = filterContext.ActionDescriptor.GetCustomAttributes(typeof(ValidateAntiForgeryTokenAttribute), false);
            if (antiForgeryAttributes.Length > 0) return false;

            // 3. There is no [BypassAntiForgeryToken] attribute on the action
            var ignoreAntiForgeryAttributes = filterContext.ActionDescriptor.GetCustomAttributes(typeof(BypassAntiForgeryTokenAttribute), false);
            if (ignoreAntiForgeryAttributes.Length > 0) return false;

            return true;
        }
    }

    public class UserData
    {
        public int UserId { get; set; }
        public int ClosetId { get; set; }
        public string Flavors { get; set; }
        public string FlavorNames { get; set; }
    }

    public class UserDataHelper
    {
        private const string USERDATA = "UserData";
        public static UserData Data
        {
            get {
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                    return null;

                if (HttpContext.Current.Session[USERDATA] == null)
                    LoadFromDatabase();

                return (UserData)HttpContext.Current.Session[USERDATA];
            }
        }

        public static void LoadFromDatabase(string userName)
        {
            int providerUserKey = 0;
            MembershipUser mu;

            // Given we are saving on Session if the Session expires we need to retrieve again from DB.
            if (userName != null)
                mu = Membership.GetUser(userName);
            else
                mu = Membership.GetUser();

            if (mu != null)
                providerUserKey = Convert.ToInt32(mu.ProviderUserKey);
            else
                throw new Exception("User not found");

            // Create the Registered User repository
            RegisteredUserRepository rup = new RegisteredUserRepository();
            RegisteredUser ru = rup.GetByMembershipId(providerUserKey);
            if (ru != null)
            {
                UserData ud = new UserData();
                ud.UserId = ru.Id;
                ud.ClosetId = ru.Closet.Id;

                string[] flavors = new string[ru.UserFlavors.Count];
                for (int i = 0; i < ru.UserFlavors.Count; i++)
                    flavors[i] = ru.UserFlavors[i].Flavor.Id.ToString();
                ud.Flavors = string.Join(",", flavors);

                string[] flavornames = new string[ru.UserFlavors.Count];
                for (int i = 0; i < ru.UserFlavors.Count; i++)
                    flavornames[i] = ru.UserFlavors[i].Flavor.Name;
                ud.FlavorNames = string.Join(",", flavornames);

                HttpContext.Current.Session[USERDATA] = ud;
            }
        }

        public static void LoadFromDatabase()
        {
            LoadFromDatabase(null);
        }
    }

    public class BuildYourClosetController : BaseController {
        private BuildYourClosetState _state;
        protected BuildYourClosetState ClosetState
        {
            get
            {
                if (_state == null)
                    _state = new BuildYourClosetState();

                return _state;
            }
        }
    }

    //  TODO: Should be enabled and we need to review every AJAX call to include it. Better if we can generalize.
    //  http://blogs.us.sogeti.com/swilliams/2009/05/14/mvc-ndash-using-antiforgerytoken-over-ajax/
    //  Also need to add <%= Html.AntiForgeryToken() %> to every form in the system.
    //  [UseAntiForgeryTokenOnPostByDefault]
    public class BaseController : Controller
    {
        protected int UserId
        {
            get {
                if (UserDataHelper.Data == null)
                    return 0;
                return UserDataHelper.Data.UserId; 
            }
        }

        protected int ClosetId
        {
            get {
                if (UserDataHelper.Data == null)
                    return 0;

                return UserDataHelper.Data.ClosetId; 
            }
        }

        private RegisteredUser _proxyLoggedUser;
        protected RegisteredUser ProxyLoggedUser  
        {
            get {
                if (User.Identity.IsAuthenticated && _proxyLoggedUser == null)
                {
                    _proxyLoggedUser = new RegisteredUser(this.UserId);
                    _proxyLoggedUser.Closet = this.ProxyCloset;
                }
                return _proxyLoggedUser;
            }
        }

        private Closet _proxyCloset;
        protected Closet ProxyCloset
        {
            get
            {
                if (User.Identity.IsAuthenticated && _proxyCloset == null)
                    _proxyCloset = new Closet(ClosetId);
                return _proxyCloset;
            }
        }

        private IList<FashionFlavor> _proxyFlavors = new List<FashionFlavor>();
        public IList<FashionFlavor> ProxyFlavors
        {
            get 
            {
                if (User.Identity.IsAuthenticated && _proxyFlavors.Count == 0 && !string.IsNullOrEmpty(UserDataHelper.Data.Flavors))
                {
                    string[] arrFlavors = UserDataHelper.Data.Flavors.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    string[] arrFlavorNames = UserDataHelper.Data.FlavorNames.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    for (int i = 0; i < arrFlavors.Length; i++)
                        _proxyFlavors.Add(new FashionFlavor(Convert.ToInt32(arrFlavors[i]), arrFlavorNames[i]));
                }

                return _proxyFlavors;
            }
        }

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (User.Identity.IsAuthenticated)
            {
                // Write to ViewData, used on every view in the system when authorized.
                ViewData["UserName"] = User.Identity.Name;

                if (ProxyFlavors.Count > 0)
                    ViewData["FashionFlavors"] = ProxyFlavors;
            }

            base.OnAuthorization(filterContext);
        }
    }
}
