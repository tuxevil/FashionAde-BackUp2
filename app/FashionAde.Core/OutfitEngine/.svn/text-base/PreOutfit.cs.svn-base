using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.Clothing;

namespace FashionAde.Core.OutfitEngine
{
    public class PreOutfit
    {
        public Combination Combination { get; set; }
        public Garment Accesory1 { get; set; }
        public Garment Accesory2 { get; set; }
        public Garment Accesory3 { get; set; }
        public Garment Accesory4 { get; set; }
        public Garment Accesory5 { get; set; }
        public Garment Accesory6 { get; set; }
        public Garment Accesory7 { get; set; }
        public Garment Accesory8 { get; set; }
        public int Seasons { get; set; }
        public int EventTypes { get; set; }

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

            PreOutfit to = (PreOutfit)obj;
            if (this.Combination == to.Combination &&
                this.Accesory1 == to.Accesory1 &&
                this.Accesory2 == to.Accesory2 &&
                this.Accesory3 == to.Accesory3 &&
                this.Accesory4 == to.Accesory4 &&
                this.Accesory5 == to.Accesory5 &&
                this.Accesory6 == to.Accesory6 &&
                this.Accesory7 == to.Accesory7 &&
                this.Accesory8 == to.Accesory8)
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

            if (Combination != null)
                hash = (hash * 7) + Combination.GetHashCode();

            if (Accesory1 != null)
                hash = (hash * 7) + Accesory1.GetHashCode();

            if (Accesory2 != null)
                hash = (hash * 7) + Accesory2.GetHashCode();

            if (Accesory3 != null)
                hash = (hash * 7) + Accesory3.GetHashCode();

            if (Accesory4 != null)
                hash = (hash * 7) + Accesory4.GetHashCode();

            if (Accesory5 != null)
                hash = (hash * 7) + Accesory5.GetHashCode();

            if (Accesory6 != null)
                hash = (hash * 7) + Accesory6.GetHashCode();

            if (Accesory7 != null)
                hash = (hash * 7) + Accesory7.GetHashCode();

            if (Accesory8 != null)
                hash = (hash * 7) + Accesory8.GetHashCode();

            return hash;
        }

        public IEnumerable<Garment> ToList()
        {
            IList<Garment> garments = new List<Garment>();

            if (Combination.GarmentA != null)
                garments.Add(Combination.GarmentA);
            if (Combination.GarmentB != null)
                garments.Add(Combination.GarmentB);
            if (Combination.GarmentC != null)
                garments.Add(Combination.GarmentC);
            if (Combination.GarmentD != null)
                garments.Add(Combination.GarmentD);
            if (Combination.GarmentE != null)
                garments.Add(Combination.GarmentE);

            if (Accesory1 != null)
                garments.Add(Accesory1);
            if (Accesory2 != null)
                garments.Add(Accesory2);
            if (Accesory3 != null)
                garments.Add(Accesory3);
            if (Accesory4 != null)
                garments.Add(Accesory4);
            if (Accesory5 != null)
                garments.Add(Accesory5);
            if (Accesory6 != null)
                garments.Add(Accesory6);
            if (Accesory7 != null)
                garments.Add(Accesory7);
            if (Accesory8 != null)
                garments.Add(Accesory8);


            return garments;
        }

    }
}
