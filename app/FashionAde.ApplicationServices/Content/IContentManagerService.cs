using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.ContentManagement;
using FashionAde.Core;

namespace FashionAde.ApplicationServices
{
    public interface IContentManagerService
    {
        void Delete(int id);
        Content Approve(int id);
        Content Disable(int id);
        Content Enable(int id);
        Content ScheduleContent(int id, DateTime from, DateTime? to);

        Content Create(string title, string body, string keyWords, string promotionalText, ContentCategory category, int createdBy, IList<FashionFlavor> falvors, IList<ContentSection> sections);
        Content Edit(int id, string title, string body, string keyWords, string promotionalText, ContentCategory category, ContentType contentType, IList<FashionFlavor> falvors, IList<ContentSection> sections);

        Content SendToReview(int contentId, int publisherId);
        Content SendToPublish(int contentId, int publisherId);
        Content SendToVerify(int contentId, int publisherId);
        Content Get(int id);
        
        IList<Content> Search(string text, int? categoryId, int? contentTypeId);
        IList<Content> Search(string text, int? categoryId, int? contentTypeId, int userId);
    }
}
