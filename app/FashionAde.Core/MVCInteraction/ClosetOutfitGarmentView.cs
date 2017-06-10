using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.Clothing;
using SharpArch.Core.DomainModel;

namespace FashionAde.Core.MVCInteraction
{
    public class ClosetOutfitGarmentView
    {
        private int closetOutfitId;
        private string imageUri;
        private string title;
        private int garmentId;

        public virtual int ClosetOutfitId
        {
            get { return closetOutfitId; }
            set { closetOutfitId = value; }
        }

        public virtual string ImageUri
        {
            get { return imageUri; }
            set { imageUri = value; }
        }

        public virtual string Title
        {
            get { return title; }
            set { title = value; }
        }

        public virtual int GarmentId
        {
            get { return garmentId; }
            set { garmentId = value; }
        }


        public ClosetOutfitGarmentView() { }
        public ClosetOutfitGarmentView(int closetOutfitId, string title, string imageUri, int garmentId)
        {
            this.closetOutfitId = closetOutfitId;
            this.imageUri = imageUri;
            this.title = title;
            this.garmentId = garmentId;
        }
    }
}
