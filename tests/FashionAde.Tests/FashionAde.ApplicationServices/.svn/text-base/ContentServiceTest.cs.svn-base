using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FashionAde.ApplicationServices;
using FashionAde.Core.DataInterfaces;
using FashionAde.Core.ContentManagement;
using Rhino.Mocks;
using FashionAde.Core;

namespace Tests.FashionAde.ApplicationServices
{
    [TestFixture]
    public class ContentManagerServiceTest
    {
        #region Tests

        [Test]
        public void CanCreateContent()
        {
            IContentManagerService cs = CreateContentManagerService();

            string title = "Title Separated Let's try";

            Content c = cs.Create(title, "Test content body", "key words test", "Promotional Text", new ContentCategory(1), 1, CreateContentFashionFlavors(), CreateSections());

            Assert.IsTrue(c.Title == title);
            Assert.IsTrue(c.UserFriendlyTitle == "/Blog/title-separated-let-s-try/0/default.aspx", c.UserFriendlyTitle);
            Assert.IsTrue(c.Status == ContentStatus.Draft);
            Assert.IsTrue(c.Flavors.Count == 4);
            Assert.IsTrue(c.Sections.Count == 4);
        }

        [Test]
        public void CanEditContent()
        {
            IContentManagerService cs = CreateContentManagerService();

            string title = "Title edited";
            string body = "body edited";

            Content c = cs.Edit(1, title, body, "key words test", "Promotional Text", new ContentCategory(1), ContentType.Blog, CreateContentFashionFlavors(), CreateSections());

            Assert.IsTrue(c.Title == title);
            Assert.IsTrue(c.UserFriendlyTitle == "/Blog/title-edited/1/default.aspx");
            Assert.IsTrue(c.Status == ContentStatus.Draft);
            Assert.IsTrue(c.Flavors.Count == 4);
            Assert.IsTrue(c.Sections.Count == 4);
        }

        private IList<ContentSection> CreateSections()
        {
            IList<ContentSection> sections = new List<ContentSection>();
            sections.Add(new ContentSection { FashionFlavor = CreateContentFashionFlavors()[0], Body = "Body", Summary = "Summary", Title = "Title" });
            sections.Add(new ContentSection { FashionFlavor = CreateContentFashionFlavors()[1], Body = "Body1", Summary = "Summary1", Title = "Title1" });
            sections.Add(new ContentSection { FashionFlavor = CreateContentFashionFlavors()[2], Body = "Body2", Summary = "Summary2", Title = "Title2" });
            sections.Add(new ContentSection { FashionFlavor = CreateContentFashionFlavors()[3], Body = "Body3", Summary = "Summary3", Title = "Title3" });
            return sections;
        }

        [Test]
        public void CanApproveContent()
        {
            IContentManagerService cs = CreateContentManagerService();

            Content c = cs.Approve(1);

            Assert.IsTrue(c.Status == ContentStatus.Published);
        }

        [Test]
        [ExpectedException(typeof(CannotApproveException))]
        public void CanNotApproveContent()
        {
            IContentManagerService cs = CreateContentManagerService();
            Content c = cs.Approve(3);
        }

        [Test]
        [ExpectedException(typeof(CannotDisableException))]
        public void CanNotDisableContent()
        {
            IContentManagerService cs = CreateContentManagerService();

            Content c = cs.Disable(1);
        }

        [Test]
        public void CanDisableContent()
        {
            IContentManagerService cs = CreateContentManagerService();

            Content c = cs.Disable(2);

            Assert.IsTrue(c.Status == ContentStatus.Disabled);
        }

        [Test]
        public void CanScheduleContent()
        {
            IContentManagerService cs = CreateContentManagerService();

            DateTime from = DateTime.Today;
            DateTime to = DateTime.Today.AddDays(30);

            Content c = cs.ScheduleContent(1, from, to);

            Assert.IsTrue(DateTime.Today >= c.ScheduleFrom & DateTime.Today <= c.ScheduleTo);
        }

        [Test]
        [ExpectedException(typeof(CannotScheduleException))]
        public void CanNotScheduleContent()
        {
            IContentManagerService cs = CreateContentManagerService();

            DateTime from = DateTime.Today;
            DateTime to = DateTime.Today.AddDays(-30);

            Content c = cs.ScheduleContent(1, from, to);
        }

        [Test]
        public void CanSendToEditor()
        {
            IContentManagerService cs = CreateContentManagerService();

            Content c = cs.SendToReview(1, 2);
            Assert.IsTrue(c.AssignedTo == 2);
        }

        #endregion

        #region Service and Repository Methods

        private IContentManagerService CreateContentManagerService()
        {
            IContentRepository contentRepository = CreateContentRepository();
            IContentPublishedRepository contentPublishedRepository = CreateContentPublishedRepository();
            return new ContentManagerService(contentRepository, contentPublishedRepository);
        }

        private IContentRepository CreateContentRepository()
        {
            IContentRepository repository = MockRepository.GenerateMock<IContentRepository>();
            repository.Expect(r => r.Get(1)).Return(CreateContent(1));
            Content c = CreateContent(2);
            c.Publish();
            repository.Expect(r => r.Get(2)).Return(c);
            c = CreateContent(3);
            c.Publish();
            c.Disable();
            repository.Expect(r => r.Get(3)).Return(c);
            return repository;
        }

        private IContentPublishedRepository CreateContentPublishedRepository()
        {
            IContentPublishedRepository repository = MockRepository.GenerateMock<IContentPublishedRepository>();
            return repository;
        }

        #endregion

        #region Mock Data Region

        private IList<Content> GetListByFlavors()
        {
            IList<Content> lst = new List<Content>();

            Content c1 = new Content(1);
            Content c2 = new Content(2);
            Content c3 = new Content(3);


            FashionFlavor ff1 = new FashionFlavor();
            ff1.Name = "Flavor1";
            FashionFlavor ff2 = new FashionFlavor();
            ff1.Name = "Flavor2";

            c1.Flavors.Add(ff1);
            c2.Flavors.Add(ff1);
            c3.Flavors.Add(ff2);

            lst.Add(c1);
            lst.Add(c2);
            lst.Add(c3);

            return lst;
        }

        private IList<FashionFlavor> CreateContentFashionFlavors()
        {
            IList<FashionFlavor> lst = new List<FashionFlavor>();
            lst.Add(new FashionFlavor(1));
            lst.Add(new FashionFlavor(2));
            lst.Add(new FashionFlavor(3));
            lst.Add(new FashionFlavor(4));
            return lst;
        }

        private ContentCategory CreateContentCategory()
        {
            ContentCategory cc = new ContentCategory();
            cc.Name = "Organizing";
            return cc;
        }

        private Content CreateContent(int id)
        {
            Content c = new Content(id);
            c.Category = new ContentCategory(1);
            c.Type = ContentType.Blog;
            return c;
        }

        #endregion
    }
}
