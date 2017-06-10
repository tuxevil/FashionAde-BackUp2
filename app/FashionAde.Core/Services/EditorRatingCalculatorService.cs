using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.Clothing;

namespace FashionAde.Core.Services
{
    public class EditorRatingCalculatorService
    {
        /// <summary>
        /// Calculates the Editor Rating for a set of garments
        /// </summary>
        /// <param name="garments">Garment Lists</param>
        /// <returns>A number from 1 to 5</returns>
        public static int CalculateRating(IEnumerable<Garment> garments)
        {
            HashSet<PatternType> patternTypes = new HashSet<PatternType>();
            HashSet<Fabric> fabrics = new HashSet<Fabric>();
            HashSet<Color> colors = new HashSet<Color>();

            foreach (Garment g in garments)
            {
                patternTypes.Add(g.Tags.Pattern.Type);
                fabrics.Add(g.Tags.Fabric);
                colors.Add(g.Tags.DefaultColor);

                foreach (Color c in g.Tags.Colors)
                    colors.Add(c);
            }

            return CalculateRating(colors, fabrics, patternTypes);
        }

        public static int CalculateRating(
            IEnumerable<Color> colors, 
            IEnumerable<Fabric> fabrics,
            IEnumerable<PatternType> patternTypes)
        {

            int points = 1;
            foreach (Color c in colors)
                if (!c.Family.IsNeutral)
                {
                    points += 1;
                    break;
                }

            bool shines = false;
            foreach (Color c in colors)
            {
                if (c.Shines)
                    shines = true;
            }

            foreach (Fabric f in fabrics)
                if (f.Shines)
                    shines = true;

            if (patternTypes.Count() > 1 || (patternTypes.ElementAt<PatternType>(0)) != PatternType.Solid)
                points += 1;
            if (fabrics.Count() > 1 || (fabrics.ElementAt<Fabric>(0)).Description != "Generic") //
                points += 1;
            if (shines)
                points += 1;

            return points;
        }
    }
}
