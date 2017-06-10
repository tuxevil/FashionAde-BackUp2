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
using FashionAde.Core.Accounts;
using FashionAde.Utils;

namespace FashionAde.ApplicationServices
{
    public class ContentService : IContentService 
    {
        IContentPublishedRepository _contentPublishedRepository;

        public ContentService(IContentPublishedRepository contentPublishedRepository)
        {
            _contentPublishedRepository = contentPublishedRepository;
        }

        public ContentPublished Get(int id)
        {
            return _contentPublishedRepository.Get(id);
        }

        public ContentPublished Get(ContentCategory cc, ContentType ct)
        {
            return _contentPublishedRepository.Get(cc, ct);
        }
        
        public IList<ContentPublished> List(ContentCategory cc, ContentType ct)
        {
            return _contentPublishedRepository.List(cc, ct);
        }

        public IList<ContentPublished> ListByFlavors(ContentCategory cc, IList<FashionFlavor> flavors, bool random)
        {
            IList<ContentPublished> lst = _contentPublishedRepository.ListByFlavors(cc, flavors);            
            if (random)
            {
                Random rnd = new Random();
                lst.OrderBy(x => rnd.Next()).Take(5);
            }
            return lst;
        }

        public IList<ContentPublishedSection> GetRandomStyleAlerts(IList<FashionFlavor> flavors, int quantity)
        {
            IList<ContentPublishedSection> lst = _contentPublishedRepository.ListSectionsByFlavors(new ContentCategory(1), flavors);
            return GetRandomItemsFromList(lst, quantity);
        }

        public IList<ContentPublishedSection> GetRandomStyleAlerts(IList<FashionFlavor> flavors)
        {
            IList<ContentPublishedSection> lst = _contentPublishedRepository.ListSectionsByFlavors(new ContentCategory(1), flavors);
            return GetRandomItemsFromList(lst, 5);
        }

        public IList<ContentPublishedSection> GetRandomStyleAlerts()
        {
            IList<ContentPublishedSection> lst = _contentPublishedRepository.ListSections(new ContentCategory(1), ContentType.Blog);
            return GetRandomItemsFromList(lst, 5);
        }

        private IList<ContentPublishedSection> GetRandomItemsFromList(IList<ContentPublishedSection> lst, int quantity)
        {
            if (lst.Count == 0)
                return lst;

            int max = (lst.Count > quantity) ? quantity : lst.Count;

            return lst.Shuffle(new Random()).Take(5).ToList();
        }
    }

}
