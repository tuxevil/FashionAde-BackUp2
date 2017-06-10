using SharpArch.Core.DomainModel;
using System.Collections.Generic;

namespace FashionAde.Core.ContentManagement
{
    public class ContentCategory : Entity
    {
        public ContentCategory()
        {
        }

        public ContentCategory(int id)
        {
            this.Id = id;
        }

        private string name;
        private string description;
        private ContentType contentType;

        private IList<ContentCategory> categories = new List<ContentCategory>();

        public virtual IList<ContentCategory> Childs
        {
            get { return categories; }
            set { categories = value; } 
        }

        public virtual string Description
        {
            get { return description; }
            set { description = value; }
        }
        
        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        public virtual ContentType Type
        {
            get { return contentType; }
            set { contentType = value; }
        }
    }
}