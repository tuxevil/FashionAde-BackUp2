using System.Collections.Generic;
using FashionAde.Core.Clothing;
using SharpArch.Core.DomainModel;
using System.Collections.ObjectModel;

namespace FashionAde.Core.OutfitEngine
{
    public class Combination
    {
        public virtual Garment GarmentA { get; set; }
        public virtual Garment GarmentB { get; set; }
        public virtual Garment GarmentC { get; set; }
        public virtual Garment GarmentD { get; set; }
        public virtual Garment GarmentE { get; set; }
        public virtual Garment GarmentF { get; set; }
        public virtual Garment GarmentG { get; set; }
        public virtual Garment GarmentH { get; set; }
        public virtual FashionFlavor FashionFlavor { get; set; }
        private float editorRating;

        public virtual float EditorRating
        {
            get { return editorRating; }
            set { editorRating = value; }
        }

        /// <summary>
        /// Implemented Equals for Performance
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            // Check for null
            if (ReferenceEquals(obj, null))
                return false;

            // Check for same reference
            if (ReferenceEquals(this, obj))
                return true;

            Combination to = (Combination)obj;
            if (this.GarmentA == to.GarmentA &&
                this.GarmentB == to.GarmentB &&
                this.GarmentC == to.GarmentC &&
                this.GarmentD == to.GarmentD &&
                this.GarmentE == to.GarmentE &&
                this.GarmentF == to.GarmentF &&
                this.GarmentG == to.GarmentG &&
                this.GarmentH == to.GarmentH)
                return true;

            return false;
        }

        /// <summary>
        /// Implemented GerHashCode for Performance
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            int hash = 13;

            if (GarmentA != null)
                hash = (hash * 7) + GarmentA.GetHashCode();

            if (GarmentB != null)
                hash = (hash * 7) + GarmentB.GetHashCode();

            if (GarmentC != null)
                hash = (hash * 7) + GarmentC.GetHashCode();

            if (GarmentD != null)
                hash = (hash * 7) + GarmentD.GetHashCode();

            if (GarmentE != null)
                hash = (hash * 7) + GarmentE.GetHashCode();

            if (GarmentF != null)
                hash = (hash * 7) + GarmentF.GetHashCode();

            if (GarmentG != null)
                hash = (hash * 7) + GarmentG.GetHashCode();
            
            if (GarmentH != null)
                hash = (hash * 7) + GarmentH.GetHashCode();

            return hash;
        }
    }
}