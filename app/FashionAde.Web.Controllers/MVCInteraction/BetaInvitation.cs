using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using FashionAde.Utils.Validators;

namespace FashionAde.Web.Controllers.MVCInteraction
{
    public class BetaInvitation
    {
        [StringLength(256)]
        public string Comments { get; set; }

        public BetaInvitationEmail[] Emails { get; set; }
    }

    public class BetaInvitationEmail
    {
        [StringLength(120)]
        [Email(ErrorMessage = "Email address is invalid")]
        public string EmailAddress { get; set; }
    }
}
