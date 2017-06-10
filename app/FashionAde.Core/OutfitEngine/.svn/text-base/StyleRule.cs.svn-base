using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using FashionAde.Core.Clothing;
using SharpArch.Core.DomainModel;

namespace FashionAde.Core.OutfitEngine
{
    public class StyleRule : Entity
    {
        private FashionFlavor fashionFlavor;
        private int maximumGarments;
        private int minimumGarments;
        private bool showPartnerGarments;
        private int maximumLayers;
        private int minimumLayers;
        private ColorBlendingRules colorBlending = new ColorBlendingRules();
        private IList<int> accessoriesAmount = new List<int>();
        private IList<PatternRule> patterns = new List<PatternRule>();
        private IList<ShapeRule> shapes = new List<ShapeRule>();
        private IList<StructureRule> structures = new List<StructureRule>(); 

        public virtual FashionFlavor FashionFlavor
        {
            get { return fashionFlavor; }
            set { fashionFlavor = value; }
        }

        public virtual int MaximumGarments
        {
            get { return maximumGarments; }
            set { maximumGarments = value; }
        }

        public virtual int MinimumGarments
        {
            get { return minimumGarments; }
            set { minimumGarments = value; }
        }

        public virtual bool ShowPartnerGarments
        {
            get { return showPartnerGarments; }
            set { showPartnerGarments = value; }
        }

        public virtual int MaximumLayers
        {
            get { return maximumLayers; }
            set { maximumLayers = value; }
        }

        public virtual int MinimumLayers
        {
            get { return minimumLayers; }
            set { minimumLayers = value; }
        }

        public virtual ColorBlendingRules ColorBlending
        {
            get { return colorBlending; }
            set { colorBlending = value; }
        }

        public virtual IList<int> AccessoriesAmount
        {
            get { return accessoriesAmount; }
            set { accessoriesAmount = value; }
        }

        public virtual IList<PatternRule> Patterns
        {
            get { return patterns; }
            set { patterns = value; }
        }

        public virtual IList<ShapeRule> Shapes
        {
            get { return shapes; }
            set { shapes = value; }
        }

        public virtual IList<StructureRule> Structures
        {
            get { return structures; }
            set { structures = value; }
        }
    }
}
