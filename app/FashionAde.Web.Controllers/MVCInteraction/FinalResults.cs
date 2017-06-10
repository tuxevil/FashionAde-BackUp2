using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core;

namespace FashionAde.Web.Controllers.MVCInteraction
{
    public class FinalResult
    {
        private IList<FashionFlavor> flavors;
        private IList<EventType> eventTypes;

        public IList<FashionFlavor> Flavors
        {
            get { return flavors; }
            set { flavors = value; }
        }

        public IList<EventType> EventTypes
        {
            get { return eventTypes; }
            set { eventTypes = value; }
        }
        
        public FinalResult() {}

        public FinalResult(IList<FashionFlavor> flavors, IList<EventType> eventTypes)
        {
            this.flavors = flavors;
            this.eventTypes = eventTypes;
        }
    }
}
