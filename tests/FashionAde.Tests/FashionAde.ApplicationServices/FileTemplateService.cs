using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FashionAde.ApplicationServices;

namespace Tests.FashionAde.ApplicationServices
{
    [TestFixture]
    public class FileTemplateServiceTest
    {
        [Test]
        public void CanUpdateTemplateWithCollection()
        {
            string templateText = "ponemos mas fruta |$EmailDataTest.Rows[N].Column$| ponemos mas fruta |$EmailDataTest.Rows[N].Column$| END";
            string finalText = "ponemos mas fruta 12345 ponemos mas fruta 12345 END";

            EmailDataTest t = new EmailDataTest();
            t.Rows = new List<EmailRowTest>();
            t.Rows.Add(new EmailRowTest { Column = "1" }); 
            t.Rows.Add(new EmailRowTest { Column = "2" }); 
            t.Rows.Add(new EmailRowTest { Column = "3" }); 
            t.Rows.Add(new EmailRowTest { Column = "4" }); 
            t.Rows.Add(new EmailRowTest { Column = "5" }); 

            string newText = RegExpTemplatorHelper.SetObjectProperties(templateText, t);

            Assert.IsTrue(newText == finalText, newText);
        }

        [Test]
        public void CanUpdateTemplateWithStandardObject()
        {
            string templateText = "1 == $EmailRowTest.Column$ y ademas 2 == $EmailRowTest.ColumnChild.Column$";
            string finalText = "1 == 1 y ademas 2 == 2";

            EmailRowTest child = new EmailRowTest() { Column = "2" };
            EmailRowTest ert = new EmailRowTest
            {
                Column = "1",
                ColumnChild = child
            };

            string newText = RegExpTemplatorHelper.SetObjectProperties(templateText, ert);

            Assert.IsTrue(newText == finalText, newText);
        }

        [Test]
        public void CanUpdateTemplateWithValueObject()
        {
            string templateText = "Texto == $data$";
            string finalText = "Texto == Texto";
            string textValue = "Texto";

            string newText = RegExpTemplatorHelper.SetObjectProperties(templateText, textValue);

            Assert.IsTrue(newText == finalText, newText);
        }

        public class EmailDataTest
        {
            public IList<EmailRowTest> Rows { get; set; }
        }

        public class EmailRowTest
        {
            public string Column { get; set; }
            public EmailRowTest ColumnChild { get; set; }
        }
    }
}
