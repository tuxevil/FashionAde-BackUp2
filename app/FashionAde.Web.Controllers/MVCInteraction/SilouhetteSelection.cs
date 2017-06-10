using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.Clothing;

namespace FashionAde.Web.Controllers.MVCInteraction
{
    public class SilouhetteSelection
    {
        private HashSet<Pattern> patterns = new HashSet<Pattern>();
        private HashSet<Fabric> fabrics = new HashSet<Fabric>();
        private bool success;

        public HashSet<Pattern> Patterns
        {
            get { return patterns; }
            set { patterns = value; }
        }

        public HashSet<Fabric> Fabrics
        {
            get { return fabrics; }
            set { fabrics = value; }
        }

        public bool Success
        {
            get { return success; }
            set { success = value; }
        }

        public SilouhetteSelection() {}

        public SilouhetteSelection(HashSet<Pattern> patterns, HashSet<Fabric> fabrics)
        {
            this.patterns = patterns;
            this.fabrics = fabrics;
            this.success = true;
        }
    }
}
