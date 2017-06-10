using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FashionAde.Web.Controllers.MVCInteraction
{
    public class OutfitRate
    {
        private int closetOutfitId;
        private int rate;
        private Guid key;
        private string message;

        public int ClosetOutfitId
        {
            get { return closetOutfitId; }
            set { closetOutfitId = value; }
        }

        public int Rate
        {
            get { return rate; }
            set { rate = value; }
        }

        public Guid Key
        {
            get { return key; }
            set { key = value; }
        }

        public string Message
        {
            get { return message; }
            set { message = value; }
        }
    }
}
