using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using FashionAde.Core.Accounts;
using System.Configuration;

namespace FashionAde.ApplicationServices
{
    public class SmtpMessageSenderService : IMessageSenderService
    {
        private IMessageTemplate template;
        public SmtpMessageSenderService(IMessageTemplate template)
        {
            this.template = template;
        }

        #region IMailSender Members

        public void Send(IMessage message, string to)
        {
            Send(message, to, null);
        }

        public void SendWithTemplate(string filePrefix, IBasicUser user, object data, string to)
        {
            SendWithTemplate(filePrefix, user, data, to, null);
        }

        public void SendWithTemplate(string filePrefix, IBasicUser user, object data, string to, string replyTo)
        {
            Send(template.Apply(filePrefix, user, data), to, replyTo);
        }

        public void Send(IMessage message, string to, string replyTo)
        {
            if (!string.IsNullOrEmpty(to) && !string.IsNullOrEmpty(message.Subject) && !string.IsNullOrEmpty(message.Body))
            {
                SmtpClient smtp = new SmtpClient();

                if (ConfigurationManager.AppSettings["Mailing_UseSsl"] != null)
                    smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["Mailing_UseSsl"]);

                MailMessage mm = new MailMessage();
                if (!string.IsNullOrEmpty(replyTo))
                    mm.ReplyTo = new MailAddress(replyTo);

                mm.Subject = message.Subject;
                mm.Body = message.Body;
                mm.IsBodyHtml = true;
                mm.To.Add(new MailAddress(to));
                smtp.Send(mm);
            }
        }

        #endregion
    }
}
