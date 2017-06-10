using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.Clothing;

namespace FashionAde.Web.Controllers.MVCInteraction
{
    public class jsonGarment
    {
        private int id;
        private string title;
        private string imageUri;

        public virtual int Id
        {
            get { return id; }
            set { id = value; }
        }

        public virtual string Title
        {
            get { return title; }
            set { title = value; }
        }

        public virtual string ImageUri
        {
            get { return imageUri; }
            set { imageUri = value; }
        }

        public jsonGarment() {}
        public jsonGarment(Garment garment)
        {
            this.id = garment.Id;
            this.title = garment.Title;
            this.imageUri = garment.ImageUri;
        }
    }
}
