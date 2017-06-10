using System;
using SharpArch.Core.DomainModel;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MvcContrib.Pagination;

namespace FashionAde.Core.ContentManagement
{
    #region Content

    public class Content : ContentBase
    {
        #region Constructors

        public Content()
        { 
        
        }

        public Content(int id)
        {
            this.Id = id;
        }

        #endregion

        #region Properties

        public virtual ContentPublished LastContentPublished { get; set; }

        protected ContentStatus status = ContentStatus.Draft;
        public virtual ContentStatus Status
        {
            get { return status; }
            protected set { status = value; }
        }

        protected IList<ContentSection> sections = new List<ContentSection>();
        public virtual IList<ContentSection> Sections
        {
            get { return sections; }
            set { sections = value; }
        }
        #endregion

        # region Methods

        public virtual void AddSection(ContentSection section)
        {
            if (!Sections.Contains(section))
            {
                section.Content = this;
                Sections.Add(section);
            }
        }

        public virtual void RemoveSection(ContentSection section)
        {
            if (Sections.Contains(section))
                Sections.Remove(section);
        }

        public virtual void AddFlavor(FashionFlavor ff)
        {
            this.Flavors.Add(ff);
        }

        public virtual void SendToReview(int userId)
        {
            if (this.AssignedTo == userId)
                throw new CannotAssignToException();

            this.AssignedTo = userId;
            this.Status = ContentStatus.AtReview;
        }

        public virtual void SendToVerify(int userId)
        {
            if (this.AssignedTo == userId)
                throw new CannotAssignToException();

            this.AssignedTo = userId;
            this.Status = ContentStatus.Draft;
        }

        public virtual void SendToPublish(int userId)
        {
            if (this.AssignedTo == userId)
                throw new CannotAssignToException();

            this.AssignedTo = userId;
            this.Status = ContentStatus.WaitingForPublish;
        }

        /// <summary>
        /// Approves the content
        /// </summary>
        /// <exception cref="CannotApproveException">Raised when the content is disabled.</exception>
        public virtual void Publish()
        {
            if (this.status == ContentStatus.Disabled)
                throw new CannotApproveException();

            this.status = ContentStatus.Published;
        }

        public virtual void Rollback()
        {
            if (this.status == ContentStatus.Published)
                throw new CannotRollbackException();

            this.status = ContentStatus.Published;
        }

        public virtual void AllowToEdit()
        {
            if (this.status != ContentStatus.Published)
                throw new CannotEditException();

            this.status = ContentStatus.Draft;
        }

        /// <summary>
        /// Disable the content
        /// </summary>
        /// <exception cref="CannotDisableException">Raised when the content is not Approved.</exception>
        public virtual void Disable()
        {
            this.status = ContentStatus.Disabled;
        }

        public virtual void Schedule(DateTime from, DateTime? to)
        {
            if (from > to)
                throw new CannotScheduleException();

            this.ScheduleFrom = from;
            this.ScheduleTo = to;
        }

        #endregion
    }

    #endregion
}