using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FashionAde.Core.MVCInteraction
{
    public class WebClosetGarment
    {
        private int id;
        private string title;
        private string imageUri;
        private int catId;

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

        public virtual int CatId
        {
            get { return catId; }
            set { catId = value; }
        }

        public WebClosetGarment() { }
        public WebClosetGarment(int id, string title, string imageUri, int categoryId)
        {
            this.id = id;
            this.title = title;
            this.imageUri = imageUri;
            this.catId = categoryId;
        }
    }
}
