using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FashionAde.Core.ContentManagement
{
    public class ContentPublishedFactory
    {
        /// <summary>
        /// Creates a new PublishedContent cloning from an actual content
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static ContentPublished CreateFromContent(Content c)
        {
            if (c.Status != ContentStatus.Published)
                throw new CannotPublishException();

            ContentPublished pc = new ContentPublished();
            pc.Title = c.Title;
            pc.Content = c;
            pc.Body = c.Body;
            pc.Category = c.Category;

            foreach(FashionFlavor fv in c.Flavors)
                pc.Flavors.Add(fv);
            
            pc.Keywords = c.Keywords;
            pc.PromotionalText = c.PromotionalText;
            pc.ScheduleFrom = c.ScheduleFrom;
            pc.ScheduleTo = c.ScheduleTo;
            pc.Type = c.Type;

            foreach (ContentSection cs in c.Sections)
                pc.AddSection(new ContentPublishedSection { ContentPublished = pc, FashionFlavor = cs.FashionFlavor, Body = cs.Body, Summary = cs.Summary, Title = cs.Title });

            return pc;
        }
    }

    public class ContentFactory
    {
        /// <summary>
        /// Creates a new PublishedContent cloning from an actual content
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static void UpdateFromContentPublished(Content content)
        {
            if (content.LastContentPublished == null)
                throw new CannotRollbackException();

            ContentPublished contentPublished = content.LastContentPublished;

            content.Title = contentPublished.Title;
            content.Body = contentPublished.Body;
            content.Category = contentPublished.Category;

            content.Flavors.Clear();
            foreach (FashionFlavor fv in contentPublished.Flavors)
                content.Flavors.Add(fv);

            content.Keywords = contentPublished.Keywords;
            content.PromotionalText = contentPublished.PromotionalText;
            content.ScheduleFrom = contentPublished.ScheduleFrom;
            content.ScheduleTo = contentPublished.ScheduleTo;
            content.Type = contentPublished.Type;

            content.Sections.Clear();
            foreach (ContentPublishedSection cs in contentPublished.Sections)
                content.AddSection(new ContentSection { Content = content, FashionFlavor = cs.FashionFlavor, Body = cs.Body, Summary = cs.Summary, Title = cs.Title });
        }
    }

}
