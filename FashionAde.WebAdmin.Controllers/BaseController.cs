using System.Web.Mvc;
using System.Web.Security;

namespace FashionAde.WebAdmin.Controllers
{
    [Authorize(Roles="Editor,Publisher,Administrator,Author")]
    public class BaseController : Controller
    {
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewData["UserName"] = Membership.GetUser().UserName;
            }
            base.OnAuthorization(filterContext);
        }
    }
}
