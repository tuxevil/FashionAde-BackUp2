using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Core.DomainModel;

namespace FashionAde.Core.Accounts
{
    public class InvitationCode : Entity
    {
        public virtual string EmailAddress { get; set; }
        public virtual string Code { get; set; }
        public virtual bool IsUsed { get; protected set; }
        public virtual BasicUser InvitedBy { get; set; }

        public virtual void MarkUsed()
        {
            this.IsUsed = true;
        }
    }
}
