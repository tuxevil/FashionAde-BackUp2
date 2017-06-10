using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using System.Web;
using FashionAde.Core.ContentManagement;

namespace FashionAde.WebAdmin.Controllers
{
    public class RolePermissionService
    {
        public static bool CanCreate()
        {
            return IsAuthor() || IsEditor();
        }

        public static bool CanAccess(Content c)
        {
            if ((c.Status == ContentStatus.Draft || c.Status == ContentStatus.Published) && CanCreate())
                return true;

            if (c.Status == ContentStatus.AtReview && IsEditor())
                return true;

            if (c.Status == ContentStatus.WaitingForPublish && IsPublisher())
                return true;

            return false;
        }

        public static bool CanDelete(Content content)
        {
            return IsAdmin();
        }

        public static bool CanSchedule(Content content)
        {
            if (content.LastContentPublished != null && content.Status != ContentStatus.Published && content.Type == ContentType.Blog)
                return IsPublisher() || IsAdmin();

            return false;
        }

        public static bool CanEnable(Content content)
        {
            if (content.LastContentPublished != null && content.LastContentPublished.Status == ContentPublishedStatus.Disabled && content.Type == ContentType.Blog)
                return IsPublisher() || IsAdmin();

            return false;
        }

        public static bool CanDisable(Content content)
        {
            if (content.LastContentPublished != null && content.LastContentPublished.Status == ContentPublishedStatus.Enabled && content.Type == ContentType.Blog)
                return IsPublisher() || IsAdmin();

            return false;
        }

        public static bool CanAssignTo(Content content)
        {
            if (content.Id == 0)
                return false;

            return true; 
        }

        public static bool CanApprove(Content content)
        {
            if (content.Status == ContentStatus.Draft)
                return false;

            return IsPublisher() || IsAdmin();
        }

        public static bool IsAuthor()
        {
            return HttpContext.Current.User.IsInRole("Author");
        }

        public static bool IsEditor()
        {
            return HttpContext.Current.User.IsInRole("Editor");
        }

        public static bool IsPublisher()
        {
            return HttpContext.Current.User.IsInRole("Publisher");
        }

        public static bool IsAdmin()
        {
            return HttpContext.Current.User.IsInRole("Admin");
        }        
    }
}