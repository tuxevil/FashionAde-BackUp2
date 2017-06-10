//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Security.Principal;
//using System.Web;
//using FashionAde.Core.ContentManagement;

//namespace FashionAde.Web.Common
//{
//    public class RolePermissionService
//    {
//        public static bool CanCreate()
//        {
//            return IsEditor() || IsAdmin();
//        }

//        public static bool CanEdit()
//        {
//            return IsEditor() || IsPublisher() || IsAdmin();
//        }

//        public static bool CanEdit(Content c)
//        {
//            if (c.Status != ContentStatus.Open)
//                return false;

//            return CanEdit();
//        }

//        public static bool CanDelete()
//        {
//            return IsPublisher() || IsAdmin();
//        }

//        public static bool CanDelete(Content content)
//        {
//            if (content.Status != ContentStatus.Open)
//                return false;

//            return CanDelete();
//        }

//        public static bool CanSchedule()
//        {
//            return IsPublisher() || IsAdmin();
//        }

//        public static bool CanSchedule(Content content)
//        {
//            if (content.Type == ContentType.Private)
//                return false;

//            if (content.Status == ContentStatus.Approved)
//                return false;

//            return CanSchedule();
//        }

//        public static bool CanDisable()
//        {
//            return IsPublisher() || IsAdmin();
//        }

//        public static bool CanDisable(Content content)
//        {
//            if (content.Status != ContentStatus.Approved || content.Type != ContentType.Blog)
//                return false;
            
//            return CanDisable();
//        }

//        public static bool CanAssignTo(Content content)
//        {
//            if (content.Status != ContentStatus.Open || content.Id == 0)
//                return false;

//            return IsEditor() || IsPublisher() || IsAdmin(); 
//        }

//        public static bool CanApprove(Content content)
//        {
//            if (content.Status == ContentStatus.Approved)
//                return false;

//            return IsPublisher() || IsAdmin();
//        }

//        public static bool IsEditor()
//        {
//            return HttpContext.Current.User.IsInRole("Editor");
//        }

//        public static bool IsPublisher()
//        {
//            return HttpContext.Current.User.IsInRole("Publisher");
//        }

//        public static bool IsAdmin()
//        {
//            return HttpContext.Current.User.IsInRole("Admin");
//        }        
//    }
//}