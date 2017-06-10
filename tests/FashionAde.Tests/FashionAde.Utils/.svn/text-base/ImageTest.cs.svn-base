using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FashionAde.Utils;

namespace Tests.FashionAde.Utils
{
    [TestFixture]
    public class ImageTest
    {
        [Test]
        public void CanRemoveBackground()
        {
            // Load Image
            ImageHelper.MakeTransparent(@"c:\testimage.jpg");
            ImageHelper.MakeTransparent(@"c:\testimage2.jpg");
        }

        [Test]
        public void CanValidateHashForFilename()
        {
            // Load Image
            int maxValue = 4;
            int minValue = 1;

            for (int i = 0; i < 100; i++)
            {
                int val1 = Security.DefineHashForHostName("testimage.jpg" + i.ToString());
                int val2 = Security.DefineHashForHostName("testimage.jpg" + i.ToString());
                Assert.LessOrEqual(val1, maxValue);
                Assert.LessOrEqual(val2, maxValue);
                Assert.GreaterOrEqual(val1, minValue);
                Assert.GreaterOrEqual(val2, minValue);
                Assert.IsTrue(val1 == val2, string.Format("val1: {0} , val2:{1}", val1, val2));
            }
        }

    }
}
