using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FashionAde.Core.ContentManagement
{
    public class ContentPublished : ContentBase
    {
        private ContentPublishedStatus _status;

        public ContentPublished()
        {
            _status = ContentPublishedStatus.Enabled; 
        }

        public virtual Content Content { get; set; }

        protected IList<ContentPublishedSection> sections = new List<ContentPublishedSection>();
        public virtual IList<ContentPublishedSection> Sections
        {
            get { return sections; }
            set { sections = value; }
        }

        public virtual void AddSection(ContentPublishedSection section)
        {
            if (!Sections.Contains(section))
            {
                section.ContentPublished = this;
                Sections.Add(section);
            }
        }

        public virtual void RemoveSection(ContentPublishedSection section)
        {
            if (Sections.Contains(section))
                Sections.Remove(section);
        }

        public virtual ContentPublishedStatus Status { 
            get { return _status; }
            protected set { _status = value; }
        }

        public virtual void Enable() 
        {
            this._status = ContentPublishedStatus.Enabled;
        }

        public virtual void Disable()
        {
            this._status = ContentPublishedStatus.Disabled;
        }
    }
}
