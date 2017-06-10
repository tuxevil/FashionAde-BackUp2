using System;
using System.Collections.Generic;

namespace FashionAde.Core.Clothing
{
    public class GarmentTags
    {
        private Silouhette silouhette;
        private IList<Color> colors = new List<Color>();
        private Pattern pattern;
        private IList<Season> seasons = new List<Season>();
        private IList<EventType> eventTypes = new List<EventType>();
        private Fabric fabric;

        public virtual Category Category
        {
            get { return this.silouhette.Category; }
        }

        public virtual Color DefaultColor
        {
            get;
            set;
        }

        public virtual Silouhette Silouhette
        {
            get { return silouhette; }
            set { silouhette = value; }
        }

        public virtual IList<Color> Colors
        {
            get { return colors; }
            set { colors = value; }
        }

        public virtual Pattern Pattern
        {
            get { return pattern; }
            set { pattern = value; }
        }

        public virtual Fabric Fabric
        {
            get { return fabric; }
            set { fabric = value;  }
        }        

        public virtual IList<EventType> EventTypes
        {
            get { return eventTypes; }
            set { eventTypes = value; }
        }

        public virtual IList<Season> Seasons
        {
            get { return seasons; }
            set { seasons = value; }
        }
    }
}