using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FashionAde.ApplicationServices
{
    public class ContactMessage : Message, IContactMessage
    {
        public ContactMessage(IMessage message, string to) 
        {
            this.To = to;
            this.Body = message.Body;
            this.Subject = message.Subject;
        }

        #region IMailMessage Members

        public string From
        {
            get;
            set;
        }

        public string To
        {
            get; set;
        }

        #endregion
    }


}
