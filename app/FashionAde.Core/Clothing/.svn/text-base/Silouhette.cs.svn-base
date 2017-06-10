using System.Collections.Generic;

namespace FashionAde.Core.Clothing
{
    public class Silouhette : Tag
    {
        private IList<Color> availableColors = new List<Color>();
        private IList<Pattern> availablePatterns = new List<Pattern>();
        private IList<FashionFlavor> fashionFlavors = new List<FashionFlavor>();
        private IList<Fabric> availableFabrics = new List<Fabric>();
        private IList<LayerCode> layers = new List<LayerCode>();
        private IList<Garment> garments = new List<Garment>();

        private Category category;
        private int importanceOrder;
        private string imageUri;

        public virtual IList<Color> AvailableColors
        {
            get { return availableColors; }
            set { availableColors = value; }
        }

        public virtual IList<Pattern> AvailablePatterns
        {
            get { return availablePatterns; }
            set { availablePatterns = value; }
        }

        public virtual IList<Fabric> AvailableFabrics
        {
            get { return availableFabrics; }
            set { availableFabrics = value; }
        }

        public virtual int ImportanceOrder
        {
            get { return importanceOrder; }
            set { importanceOrder = value; }
        }

        public virtual IList<FashionFlavor> FashionFlavors
        {
            get { return fashionFlavors; }
            set { fashionFlavors = value; }
        }

        public virtual string ImageUri
        {
            get { return imageUri; }
            set { imageUri = value; }
        }

        public virtual Category Category
        {
            get { return category; }
            set { category = value; }
        }

        public virtual IList<LayerCode> Layers
        {
            get { return layers; }
            set { layers = value; }
        }

        private IList<Season> seasons = new List<Season>();
        private IList<EventType> eventTypes = new List<EventType>();

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

        public virtual IList<Garment> Garments
        {
            get { return garments; }
            set { garments = value; }
        }

        public virtual Shape Shape
        {
            get;
            set;
        }

        public virtual Structure Structure
        {
            get;
            set;
        }

        public Silouhette() {}

        public Silouhette(int id)
        {
            this.Id = id;
        }
    }
}