using SharpArch.Core.DomainModel;

namespace FashionAde.Core.ThirdParties
{
    public class Partner : Entity, IThirdParty
    {
        private string code;
        private string colorScheme;
        private string footerLayout;
        private string headerLayout;
        private string name;
        private string websiteUri;

        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        public virtual string Code
        {
            get { return code; }
            set { code = value; }
        }

        public virtual string ColorScheme
        {
            get { return colorScheme; }
            set { colorScheme = value; }
        }

        public virtual string HeaderLayout
        {
            get { return headerLayout; }
            set { headerLayout = value; }
        }

        public virtual string FooterLayout
        {
            get { return footerLayout; }
            set { footerLayout = value; }
        }

        public virtual string WebsiteUri
        {
            get { return websiteUri; }
            set { websiteUri = value; }
        }

        public Partner() { }
        public Partner(int id)
        {
            this.Id = id;
        }
    }
}