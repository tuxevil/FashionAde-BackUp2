using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using FashionAde.Core.ContentManagement;

namespace FashionAde.Web.Controllers.MVCInteraction
{
    public class FeedBack
    {
        [Required(ErrorMessage = "Email required")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Email is invalid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Name required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Message required")]
        public string Message { get; set; }
        public IList<ContentPublishedSection> Content { get; set; }
    }
}
