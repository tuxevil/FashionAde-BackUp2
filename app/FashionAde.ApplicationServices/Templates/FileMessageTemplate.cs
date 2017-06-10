using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.Accounts;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection;

namespace FashionAde.ApplicationServices
{
    public class FileMessageTemplate : IMessageTemplate
    {
        private const string TEMPLATE = "template.htm";

        protected string ResourcePath { get { return ConfigurationManager.AppSettings["Template_Location"]; } }
        protected string SiteUrl { get { return ConfigurationManager.AppSettings["Template_SiteUrl"]; } }
        protected string SiteName { get { return ConfigurationManager.AppSettings["Template_SiteName"]; } }

        private string GetSubject(string filePrefix)
        {
            string subject = File.ReadAllText(Path.Combine(this.ResourcePath, string.Format("{0}_subject.htm", filePrefix)));
            return subject;
        }

        private string GetBody(string filePrefix)
        {
            string body = File.ReadAllText(Path.Combine(this.ResourcePath, string.Format("{0}_body.htm", filePrefix)));
            return body;
        }

        private string GetTitle(string filePrefix)
        {
            string title = File.ReadAllText(Path.Combine(this.ResourcePath, string.Format("{0}_title.htm", filePrefix)));
            return title;
        }

        public IMessage Apply(string discriminator, IBasicUser owner, object systemData)
        {
            string subject = GetSubject(discriminator);
            string body = GetBody(discriminator);
            string title = GetTitle(discriminator);

            subject = RegExpTemplatorHelper.SetObjectProperties(subject, owner);
            subject = RegExpTemplatorHelper.SetObjectProperties(subject, systemData);

            body = RegExpTemplatorHelper.SetObjectProperties(body, owner);
            body = RegExpTemplatorHelper.SetObjectProperties(body, systemData);

            title = RegExpTemplatorHelper.SetObjectProperties(title, owner);
            title = RegExpTemplatorHelper.SetObjectProperties(title, systemData);

            string file = Path.Combine(this.ResourcePath, TEMPLATE);
            string defaultBody = string.Empty;
            if (File.Exists(file))
            {
                defaultBody = File.ReadAllText(file);
                defaultBody = defaultBody.Replace("[BODY]", body);
                defaultBody = defaultBody.Replace("[TITLE]", title);
                body = defaultBody;
            }

            subject = subject.Replace("[SITEURL]", this.SiteUrl);
            subject = subject.Replace("[SITENAME]", this.SiteName);

            body = body.Replace("[SITEURL]", this.SiteUrl);
            body = body.Replace("[SITENAME]", this.SiteName);

            IMessage cm = new Message();
            cm.Subject = subject;
            cm.Body = body;
            return cm;
        }
    }
}
