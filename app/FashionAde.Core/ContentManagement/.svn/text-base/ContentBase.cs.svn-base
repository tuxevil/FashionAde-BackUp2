using System;
using SharpArch.Core.DomainModel;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MvcContrib.Pagination;
using NHibernate.Validator.Constraints;

namespace FashionAde.Core.ContentManagement
{
    public class ContentBase : Entity
    {
        #region Private Properties
        protected string title;
        protected ContentType type;
        protected string body;
        protected string keywords;
        protected string promotionalText;
        protected int? assignedTo;
        protected int? approvedBy;
        protected int editedBy;
        protected ContentCategory category;
        protected IList<FashionFlavor> flavors = new List<FashionFlavor>();
        protected DateTime? scheduleFrom;
        protected DateTime? scheduleTo;
        protected DateTime createdOn;
        #endregion

        #region Properties

        public virtual DateTime? ScheduleFrom
        {
            get { return scheduleFrom; }
            set { scheduleFrom = value; }
        }

        public virtual DateTime? ScheduleTo
        {
            get { return scheduleTo; }
            set { scheduleTo = value; }
        }

        public virtual DateTime CreatedOn
        {
            get { return createdOn; }
            set { createdOn = value; }
        }

        [NotNullNotEmpty]
        [Length(Max = 10)]
        public virtual string Title
        {
            get { return title; }
            set { title = value; }
        }

        [NotNull]
        public virtual ContentType Type
        {
            get { return type; }
            set { type = value; }
        }

        [NotNullNotEmpty]
        public virtual string Body
        {
            get { return body; }
            set { body = value; }
        }

        [NotNullNotEmpty]
        public virtual string Keywords
        {
            get { return keywords; }
            set { keywords = value; }
        }

        [NotNull]
        public virtual ContentCategory Category
        {
            get { return category; }
            set { category = value; }
        }

        public virtual IList<FashionFlavor> Flavors
        {
            get { return flavors; }
            set { flavors = value; }
        }

        public virtual string UserFriendlyTitle
        {
            get { return CreateUserFriendlyTitle(); }
        }

        private string CreateUserFriendlyTitle()
        {
            string formatedTitle = this.Title;

            formatedTitle = (formatedTitle ?? "").Trim().ToLower();
            formatedTitle = Regex.Replace(formatedTitle, @"[^a-z0-9]", "-"); // invalid chars
            formatedTitle = Regex.Replace(formatedTitle, @"-+", "-").Trim(); // convert multiple dashes into one

            return string.Format(@"/Content/{0}/{1}/{2}/default.aspx", this.Type, formatedTitle, this.Id);
        }

        public virtual int? AssignedTo
        {
            get { return assignedTo; }
            protected set { assignedTo = value; }
        }

        public virtual int? ApprovedBy
        {
            get { return approvedBy; }
            protected set { approvedBy = value; }
        }

        public virtual int EditedBy
        {
            get { return editedBy; }
            set { editedBy = value; }
        }

        public virtual string PromotionalText
        {
            get { return promotionalText; }
            set { promotionalText = value; }
        }

        #endregion

    }

}