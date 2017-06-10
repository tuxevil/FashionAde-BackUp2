using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace FashionAde.Utils
{
    public class ImageHelper
    {
        public static void MakeTransparent(string fileName)
        {
            Bitmap source = GetTransparentBitmap(new Bitmap(fileName));
            source.Save(fileName, ImageFormat.Png);
        }

        public static Bitmap GetTransparentBitmap(Bitmap source)
        {
            Color color = source.GetPixel(0, 0);
            source.MakeTransparent(source.GetPixel(0, 0));
            return source;
        }

    }
}
