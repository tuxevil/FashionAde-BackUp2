using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.DataInterfaces;
using FashionAde.Core.ContentManagement;
using FashionAde.Core;
using MvcContrib.Pagination;
using SharpArch.Web.NHibernate;
using System.Collections.Specialized;

namespace FashionAde.ApplicationServices
{
    public class ContentManagerService : IContentManagerService
    {
        IContentRepository _contentRepository;
        IContentPublishedRepository _contentPublishedRepository;

        #region Constructors

        public ContentManagerService(IContentRepository contentRepository, IContentPublishedRepository contentPublishedRepository)
        {
            _contentRepository = contentRepository;
            _contentPublishedRepository = contentPublishedRepository;
        }

        #endregion

        public Content Get(int id)
        {
            return _contentRepository.Get(id);
        }

        public Content Create(string title, string body, string keyWords, string promotionalText, ContentCategory category, int createdBy, IList<FashionFlavor> flavors, IList<ContentSection> sections)
        {
            return Save(null, title, body, keyWords, promotionalText, category, ContentType.Blog, createdBy, flavors, sections);
        }

        public Content Edit(int id, string title, string body, string keyWords, string promotionalText, ContentCategory category, ContentType contentType, IList<FashionFlavor> flavors, IList<ContentSection> sections)
        {
            return Save(id, title, body, keyWords, promotionalText, category, contentType, null, flavors, sections);
        }

        public void Delete(int id)
        {
            Content c = _contentRepository.Get(id);
            c.Disable();

            if (c.LastContentPublished != null)
            {
                c.LastContentPublished.Disable();
                _contentPublishedRepository.SaveOrUpdate(c.LastContentPublished);
            }

            _contentRepository.SaveOrUpdate(c);
        }

        private Content Save(int? id, string title, string body, string keyWords, string promotionalText, ContentCategory category, ContentType contentType, int? createdBy, IList<FashionFlavor> flavors, IList<ContentSection> sections)
        {
            Content c;
            if (id != null) c = _contentRepository.Get(Convert.ToInt32(id));
            else c = new Content();

            c.Title = title;
            c.Body = body;
            c.Type = contentType;
            c.Keywords = keyWords;
            c.Category = category;
            c.PromotionalText = promotionalText;

            c.Flavors.Clear();
            foreach (FashionFlavor ff in flavors)
                c.AddFlavor(ff);

            c.Sections.Clear();
            foreach (ContentSection cs in sections)
                c.AddSection(cs);

            if (createdBy.HasValue)
                c.EditedBy = createdBy.Value;

            _contentRepository.SaveOrUpdate(c);
            return c;
        }

        public Content SendToReview(int contentId, int editorId)
        {
            Content c = _contentRepository.Get(contentId);
            c.SendToReview(editorId);
            _contentRepository.SaveOrUpdate(c);
            return c;
        }

        public Content SendToVerify(int contentId, int editorId)
        {
            Content c = _contentRepository.Get(contentId);
            c.SendToVerify(editorId);
            _contentRepository.SaveOrUpdate(c);
            return c;
        }

        public Content SendToPublish(int contentId, int editorId)
        {
            Content c = _contentRepository.Get(contentId);
            c.SendToPublish(editorId);
            _contentRepository.SaveOrUpdate(c);
            return c;
        }

        public Content Approve(int id)
        {
            Content c = _contentRepository.Get(id);
            c.Publish();
            _contentRepository.SaveOrUpdate(c);

            Publish(c.Id);

            return c;
        }

        public Content Rollback(int id)
        {
            Content c = _contentRepository.Get(id);
            c.Rollback();
            ContentFactory.UpdateFromContentPublished(c);
            _contentRepository.SaveOrUpdate(c);

            return c;
        }

        public Content Enable(int id)
        {
            Content c = _contentRepository.Get(id);
            c.LastContentPublished.Enable();
            _contentPublishedRepository.SaveOrUpdate(c.LastContentPublished);
            return c;
        }


        public Content Disable(int id)
        {
            Content c = _contentRepository.Get(id);
            c.LastContentPublished.Disable();
            _contentPublishedRepository.SaveOrUpdate(c.LastContentPublished);
            return c;
        }

        public Content ScheduleContent(int id, DateTime from, DateTime? to)
        {
            Content c = _contentRepository.Get(id);
            c.Schedule(from, to);
            _contentRepository.SaveOrUpdate(c);
            return c;
        }

        private ContentPublished Publish(int contentId)
        {
            Content c = _contentRepository.Get(contentId);
            if (c == null)
                throw new Exception("Not valid content.");

            IDictionary<string, object> propertyValues = new Dictionary<string, object>();
            propertyValues.Add("Content", c);
            ContentPublished pc = _contentPublishedRepository.FindOne(propertyValues);

            if (pc != null)
                _contentPublishedRepository.Delete(pc);

            pc = ContentPublishedFactory.CreateFromContent(c);
            _contentPublishedRepository.SaveOrUpdate(pc);

            c.LastContentPublished = pc;

            return pc;
        }

        public IList<Content> Search(string text, int? catId, int? typeId)
        {
            return Search(text, catId, typeId,  0);
        }

        public IList<Content> Search(string text, int? catId, int? typeId, int userId)
        {
            ContentCategory cc = null;
            if (catId.HasValue)
                cc = new ContentCategory(catId.Value);

            if (typeId.HasValue)
                return _contentRepository.Search(text, cc, (ContentType)typeId.Value, userId);
            else
                return _contentRepository.Search(text, cc, userId);
        }
    }
}
