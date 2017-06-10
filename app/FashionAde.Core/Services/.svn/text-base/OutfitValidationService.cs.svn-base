using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.Clothing;
using FashionAde.Core.OutfitEngine;  

namespace FashionAde.Core.Services
{
    public class OutfitValidationService
    {
        private static Dictionary<LayerCode, int> dict = new Dictionary<LayerCode, int>();

        static OutfitValidationService() 
        {
            dict.Add(LayerCode.A, 0);
            dict.Add(LayerCode.Ai, 0);
            dict.Add(LayerCode.Aii, 1);
            dict.Add(LayerCode.B, 2);
            dict.Add(LayerCode.C, 3);
            dict.Add(LayerCode.D, 4);
            dict.Add(LayerCode.ACC1, 5);
            dict.Add(LayerCode.ACC2, 6);
            dict.Add(LayerCode.ACC3, 7);
            dict.Add(LayerCode.ACC4, 8);
            dict.Add(LayerCode.ACC5, 9);
            dict.Add(LayerCode.ACC6, 10);
            dict.Add(LayerCode.ACC7, 11);
            dict.Add(LayerCode.ACC8, 12);
        }

        /// <summary>
        /// Obtain the correct garment position given the passed garment, starting from zero.
        /// </summary>
        /// <returns>A number between 0 and 12</returns>
        public static int GetClosetOutfitPosition(Garment garment)
        {
            if (garment.Tags.Silouhette.Layers.Count == 0)
                throw new Exception(string.Format("No layer defined for garment {0}.",garment.Id));

            return dict[garment.Tags.Silouhette.Layers[0]];
        }

        /// <summary>
        /// Obtain the correct garment position given the passed garment, starting from zero.
        /// </summary>
        /// <returns>A number between 0 and 12</returns>
        public static int GetPrecombinationPosition(Garment garment)
        {
            if (garment.Tags.Silouhette.Layers.Count == 0)
                throw new Exception(string.Format("No layer defined for garment {0}.", garment.Id));

            if (OutfitValidationService.IsAccessory(garment))
                throw new Exception("A precombination can not have accessories.");

            return dict[garment.Tags.Silouhette.Layers[0]];
        }

        /// <summary>
        /// Determines if a garment is an accessory
        /// </summary>
        /// <param name="garment">Garment</param>
        /// <returns>True if it is an accessory</returns>
        public static bool IsAccessory(Garment garment)
        {
            return (garment.Tags.Silouhette.Layers.Contains(LayerCode.ACC1) ||
                garment.Tags.Silouhette.Layers.Contains(LayerCode.ACC2) ||
                garment.Tags.Silouhette.Layers.Contains(LayerCode.ACC3) ||
                garment.Tags.Silouhette.Layers.Contains(LayerCode.ACC4) ||
                garment.Tags.Silouhette.Layers.Contains(LayerCode.ACC5) ||
                garment.Tags.Silouhette.Layers.Contains(LayerCode.ACC6) ||
                garment.Tags.Silouhette.Layers.Contains(LayerCode.ACC7) ||
                garment.Tags.Silouhette.Layers.Contains(LayerCode.ACC8));
        }

        /// <summary>
        /// Determines if a garment is an accessory
        /// </summary>
        /// <param name="garment">Garment</param>
        /// <returns>True if it is an accessory</returns>
        public static bool IsAccessory(PreGarment pregarment)
        {
            return (pregarment.Silouhette.Layers.Contains(LayerCode.ACC1) ||
                pregarment.Silouhette.Layers.Contains(LayerCode.ACC2) ||
                pregarment.Silouhette.Layers.Contains(LayerCode.ACC3) ||
                pregarment.Silouhette.Layers.Contains(LayerCode.ACC4) ||
                pregarment.Silouhette.Layers.Contains(LayerCode.ACC5) ||
                pregarment.Silouhette.Layers.Contains(LayerCode.ACC6) ||
                pregarment.Silouhette.Layers.Contains(LayerCode.ACC7) ||
                pregarment.Silouhette.Layers.Contains(LayerCode.ACC8));
        }

        /// <summary>
        /// Determines if a garment is an accessory
        /// </summary>
        /// <param name="garment">Garment</param>
        /// <returns>True if it is an accessory</returns>
        public static bool IsAccessory(Silouhette silouhette)
        {
            return (silouhette.Layers.Contains(LayerCode.ACC1) ||
                silouhette.Layers.Contains(LayerCode.ACC2) ||
                silouhette.Layers.Contains(LayerCode.ACC3) ||
                silouhette.Layers.Contains(LayerCode.ACC4) ||
                silouhette.Layers.Contains(LayerCode.ACC5) ||
                silouhette.Layers.Contains(LayerCode.ACC6) ||
                silouhette.Layers.Contains(LayerCode.ACC7) ||
                silouhette.Layers.Contains(LayerCode.ACC8));
        }

        /// <summary>
        /// Validates if the combination makes sense on a Layering level only.
        /// We don't want to accept combinations bad layered from users.
        /// </summary>
        /// <param name="garments">Garment to try to combine</param>
        /// <returns></returns>
        public static bool IsValidCombination(IEnumerable<Garment> garments)
        {
            // Create the list of layers
            IList<IList<LayerCode>> layers = new List<IList<LayerCode>>();
            foreach (Garment g in garments)
            {
                if (g.Tags.Silouhette.Layers.Contains(LayerCode.A) ||
                    g.Tags.Silouhette.Layers.Contains(LayerCode.B) ||
                    g.Tags.Silouhette.Layers.Contains(LayerCode.C) ||
                    g.Tags.Silouhette.Layers.Contains(LayerCode.D) ||
                    g.Tags.Silouhette.Layers.Contains(LayerCode.Ai) ||
                    g.Tags.Silouhette.Layers.Contains(LayerCode.Aii))
                    layers.Add(g.Tags.Silouhette.Layers);
            }

            // Make sure we have only 1 garment per layer including shoes
            IList<IList<LayerCode>> layersWithShoes = new List<IList<LayerCode>>(layers);
            foreach (Garment g in garments)
                if (g.Tags.Silouhette.Layers.Contains(LayerCode.ACC1))
                    layersWithShoes.Add(g.Tags.Silouhette.Layers);

            if (CountLayer(layers, LayerCode.A) > 1 ||
                CountLayer(layers, LayerCode.B) > 1 ||
                CountLayer(layers, LayerCode.C) > 1 ||
                CountLayer(layers, LayerCode.D) > 1 ||
                CountLayer(layers, LayerCode.Ai) > 1 ||
                CountLayer(layers, LayerCode.Aii) > 1 ||
                CountLayer(layersWithShoes, LayerCode.ACC1) > 1)
                return false;

            bool isValid = false;
            if (layers.Count == 2 && HasLayer(layers,LayerCode.A) && HasLayer(layers,LayerCode.B))
               isValid = true;

            if (layers.Count == 2 && HasLayer(layers,LayerCode.A) && HasLayer(layers,LayerCode.C))
               isValid = true;

            if (layers.Count == 2 && HasLayer(layers,LayerCode.A) && HasLayer(layers,LayerCode.D))
               isValid = true;

            if (layers.Count == 3 && HasLayer(layers,LayerCode.A) && HasLayer(layers,LayerCode.B) 
                && HasLayer(layers,LayerCode.C))
                isValid = true;

            if (layers.Count == 3 && HasLayer(layers,LayerCode.A) && HasLayer(layers,LayerCode.B)
                && HasLayer(layers,LayerCode.D))
                isValid = true;

            if (layers.Count == 3 && HasLayer(layers,LayerCode.A) && HasLayer(layers,LayerCode.C)
                && HasLayer(layers,LayerCode.D))
                isValid = true;

            if (layers.Count == 3 && HasLayer(layers,LayerCode.Ai) && HasLayer(layers,LayerCode.Aii)
                && HasLayer(layers,LayerCode.B))
                isValid = true;

            if (layers.Count == 3 && HasLayer(layers,LayerCode.Ai) && HasLayer(layers,LayerCode.Aii)
                && HasLayer(layers,LayerCode.C))
                isValid = true;

            if (layers.Count == 3 && HasLayer(layers,LayerCode.Ai) && HasLayer(layers,LayerCode.Aii)
                && HasLayer(layers,LayerCode.D))
                isValid = true;

            if (layers.Count == 4 && HasLayer(layers,LayerCode.A) && HasLayer(layers,LayerCode.B)
                && HasLayer(layers,LayerCode.C) && HasLayer(layers,LayerCode.D))
                isValid = true;

            if (layers.Count == 4 && HasLayer(layers,LayerCode.Ai) && HasLayer(layers,LayerCode.Aii)
                && HasLayer(layers,LayerCode.B) && HasLayer(layers,LayerCode.C))
                isValid = true;

            if (layers.Count == 4 && HasLayer(layers,LayerCode.Ai) && HasLayer(layers,LayerCode.Aii)
                && HasLayer(layers,LayerCode.C) && HasLayer(layers,LayerCode.D))
                isValid = true;

            if (layers.Count == 5 && HasLayer(layers,LayerCode.Ai) && HasLayer(layers,LayerCode.Aii)
                && HasLayer(layers,LayerCode.B) && HasLayer(layers,LayerCode.C) && HasLayer(layers,LayerCode.D))
                isValid = true;

            return isValid;
        }

        public static bool VerifyAndSetSeasonsAndEventTypes(HashSet<int> seasons, HashSet<int> eventTypes, Combination comb)
        {
            PreOutfit po = new PreOutfit();
            po.Combination = comb;
            return VerifyAndSetSeasonsAndEventTypes(seasons, eventTypes, po);
        }

        public static bool VerifyAndSetSeasonsAndEventTypes(HashSet<int> seasons, HashSet<int> eventTypes, PreOutfit of)
        {
            // Load first data available
            seasons.Clear();
            seasons.Add(of.Combination.GarmentA.SeasonCode & (int)Season.Fall);
            seasons.Add(of.Combination.GarmentA.SeasonCode & (int)Season.Spring);
            seasons.Add(of.Combination.GarmentA.SeasonCode & (int)Season.Summer);
            seasons.Add(of.Combination.GarmentA.SeasonCode & (int)Season.Winter);
            seasons.Remove(0);

            eventTypes.Clear();
            eventTypes.Add(of.Combination.GarmentA.EventCode & 1);
            eventTypes.Add(of.Combination.GarmentA.EventCode & 2);
            eventTypes.Add(of.Combination.GarmentA.EventCode & 4);
            eventTypes.Add(of.Combination.GarmentA.EventCode & 8);
            eventTypes.Add(of.Combination.GarmentA.EventCode & 16);
            eventTypes.Remove(0);
             
            Intersect(seasons,GetSeasons(of.Combination.GarmentB));
            Intersect(seasons,GetSeasons(of.Combination.GarmentC));
            Intersect(seasons,GetSeasons(of.Combination.GarmentD));
            Intersect(seasons,GetSeasons(of.Combination.GarmentE));
            Intersect(seasons,GetSeasons(of.Accesory1));
            Intersect(seasons,GetSeasons(of.Accesory2));
            Intersect(seasons,GetSeasons(of.Accesory3));
            Intersect(seasons,GetSeasons(of.Accesory4));
            Intersect(seasons,GetSeasons(of.Accesory5));
            Intersect(seasons,GetSeasons(of.Accesory6));
            Intersect(seasons,GetSeasons(of.Accesory7));
            Intersect(seasons,GetSeasons(of.Accesory8));

            Intersect(eventTypes,GetEventTypes(of.Combination.GarmentB));
            Intersect(eventTypes,GetEventTypes(of.Combination.GarmentC));
            Intersect(eventTypes,GetEventTypes(of.Combination.GarmentD));
            Intersect(eventTypes,GetEventTypes(of.Combination.GarmentE));
            Intersect(eventTypes,GetEventTypes(of.Accesory1));
            Intersect(eventTypes,GetEventTypes(of.Accesory2));
            Intersect(eventTypes,GetEventTypes(of.Accesory3));
            Intersect(eventTypes,GetEventTypes(of.Accesory4));
            Intersect(eventTypes,GetEventTypes(of.Accesory5));
            Intersect(eventTypes,GetEventTypes(of.Accesory6));
            Intersect(eventTypes,GetEventTypes(of.Accesory7));
            Intersect(eventTypes,GetEventTypes(of.Accesory8));

            return (eventTypes.Count > 0 && seasons.Count > 0);
        }

        private static void Intersect(HashSet<int> lst, HashSet<int> newCol) 
        {
            if (newCol != null && newCol.Count > 0)
                lst.IntersectWith(newCol);
        }

        private static HashSet<int> GetSeasons(Garment g)
        {
            HashSet<int> lst = new HashSet<int>();
            if (g != null)
            {
                lst.Add(g.SeasonCode & (int)Season.Fall);
                lst.Add(g.SeasonCode & (int)Season.Spring);
                lst.Add(g.SeasonCode & (int)Season.Summer);
                lst.Add(g.SeasonCode & (int)Season.Winter);
                lst.Remove(0);
            }
            return lst;
        }

        private static HashSet<int> GetEventTypes(Garment g)
        {
            HashSet<int> lst = new HashSet<int>();
            if (g != null)
            {
                lst.Add(g.EventCode & 1);
                lst.Add(g.EventCode & 2);
                lst.Add(g.EventCode & 4);
                lst.Add(g.EventCode & 8);
                lst.Add(g.EventCode & 16);
                lst.Remove(0);
            }
            return lst;
        }

        private static bool HasLayer(IList<IList<LayerCode>> layerCodes, LayerCode layer) 
        {
            foreach (IList<LayerCode> layerCollection in layerCodes)
                if (layerCollection.Contains(layer))
                    return true;

            return false;
        }

        private static int CountLayer(IList<IList<LayerCode>> layerCodes, LayerCode layer)
        {
            int i = 0;
            foreach (IList<LayerCode> layerCollection in layerCodes)
                if (layerCollection.Contains(layer))
                    i++;
            return i;
        }

        public static bool IsJewleryColor(Garment g)
        {
            return !(g.Tags.Colors[0].Id < 43); 
        }

        public static bool IsValidOutfit(PreOutfit outfit, StyleRule sr, HashSet<int> seasons, HashSet<int> eventTypes)
        {
            Combination comb = outfit.Combination;

            if (!OutfitValidationService.VerifyAndSetSeasonsAndEventTypes(seasons, eventTypes, outfit))
                return false;

            HashSet<ColorFamily> colorFamiliesHash = new HashSet<ColorFamily>();
            if (comb.GarmentA != null) colorFamiliesHash.Add(comb.GarmentA.Tags.Colors[0].Family);
            if (comb.GarmentB != null) colorFamiliesHash.Add(comb.GarmentB.Tags.Colors[0].Family);
            if (comb.GarmentC != null) colorFamiliesHash.Add(comb.GarmentC.Tags.Colors[0].Family);
            if (comb.GarmentD != null) colorFamiliesHash.Add(comb.GarmentD.Tags.Colors[0].Family);
            if (comb.GarmentE != null) colorFamiliesHash.Add(comb.GarmentE.Tags.Colors[0].Family);
            if (outfit.Accesory1 != null && !IsJewleryColor(outfit.Accesory1)) colorFamiliesHash.Add(outfit.Accesory1.Tags.Colors[0].Family);
            if (outfit.Accesory2 != null && !IsJewleryColor(outfit.Accesory2)) colorFamiliesHash.Add(outfit.Accesory2.Tags.Colors[0].Family);
            if (outfit.Accesory3 != null && !IsJewleryColor(outfit.Accesory3)) colorFamiliesHash.Add(outfit.Accesory3.Tags.Colors[0].Family);
            if (outfit.Accesory4 != null && !IsJewleryColor(outfit.Accesory4)) colorFamiliesHash.Add(outfit.Accesory4.Tags.Colors[0].Family);
            if (outfit.Accesory5 != null && !IsJewleryColor(outfit.Accesory5)) colorFamiliesHash.Add(outfit.Accesory5.Tags.Colors[0].Family);
            if (outfit.Accesory6 != null && !IsJewleryColor(outfit.Accesory6)) colorFamiliesHash.Add(outfit.Accesory6.Tags.Colors[0].Family);
            if (outfit.Accesory7 != null && !IsJewleryColor(outfit.Accesory7)) colorFamiliesHash.Add(outfit.Accesory7.Tags.Colors[0].Family);
            if (outfit.Accesory8 != null && !IsJewleryColor(outfit.Accesory8)) colorFamiliesHash.Add(outfit.Accesory8.Tags.Colors[0].Family);

            if (!OutfitValidationService.IsValidCombination(colorFamiliesHash, null, null, null, sr, null, false, true))
                return false;

            if (seasons.Contains((int)Season.Summer))
            {
                bool hasD = false;
                if (
                    (comb.GarmentA != null && comb.GarmentA.Tags.Silouhette.Layers.Contains(LayerCode.D)) ||
                    (comb.GarmentB != null && comb.GarmentB.Tags.Silouhette.Layers.Contains(LayerCode.D)) ||
                    (comb.GarmentC != null && comb.GarmentC.Tags.Silouhette.Layers.Contains(LayerCode.D)) ||
                    (comb.GarmentD != null && comb.GarmentD.Tags.Silouhette.Layers.Contains(LayerCode.D)) ||
                    (comb.GarmentE != null && comb.GarmentE.Tags.Silouhette.Layers.Contains(LayerCode.D))
                    )
                    hasD = true;

                bool hasB = false;
                if (
                    (comb.GarmentA != null && comb.GarmentA.Tags.Silouhette.Layers.Contains(LayerCode.B)) ||
                    (comb.GarmentB != null && comb.GarmentB.Tags.Silouhette.Layers.Contains(LayerCode.B)) ||
                    (comb.GarmentC != null && comb.GarmentC.Tags.Silouhette.Layers.Contains(LayerCode.B)) ||
                    (comb.GarmentD != null && comb.GarmentD.Tags.Silouhette.Layers.Contains(LayerCode.B)) ||
                    (comb.GarmentE != null && comb.GarmentE.Tags.Silouhette.Layers.Contains(LayerCode.B))
                    )
                    hasB = true;

                bool hasC = false;
                if (
                    (comb.GarmentA != null && comb.GarmentA.Tags.Silouhette.Layers.Contains(LayerCode.C)) ||
                    (comb.GarmentB != null && comb.GarmentB.Tags.Silouhette.Layers.Contains(LayerCode.C)) ||
                    (comb.GarmentC != null && comb.GarmentC.Tags.Silouhette.Layers.Contains(LayerCode.C)) ||
                    (comb.GarmentD != null && comb.GarmentD.Tags.Silouhette.Layers.Contains(LayerCode.C)) ||
                    (comb.GarmentE != null && comb.GarmentE.Tags.Silouhette.Layers.Contains(LayerCode.C))
                    )
                    hasC = true;

                if (hasD || (hasB && hasC))
                    seasons.Remove((int)Season.Summer);
            }

            return true;
        }
        public static bool IsValidCombination(Combination cb, StyleRule sr)
        {
            HashSet<ColorFamily> colorFamiliesHash = new HashSet<ColorFamily>();
            if (cb.GarmentA != null) colorFamiliesHash.Add(cb.GarmentA.Tags.Colors[0].Family);
            if (cb.GarmentB != null) colorFamiliesHash.Add(cb.GarmentB.Tags.Colors[0].Family);
            if (cb.GarmentC != null) colorFamiliesHash.Add(cb.GarmentC.Tags.Colors[0].Family);
            if (cb.GarmentD != null) colorFamiliesHash.Add(cb.GarmentD.Tags.Colors[0].Family);
            if (cb.GarmentE != null) colorFamiliesHash.Add(cb.GarmentE.Tags.Colors[0].Family);

            HashSet<PatternType> pt = new HashSet<PatternType>();
            if (cb.GarmentA != null) pt.Add(cb.GarmentA.Tags.Pattern.Type);
            if (cb.GarmentB != null) pt.Add(cb.GarmentB.Tags.Pattern.Type);
            if (cb.GarmentC != null) pt.Add(cb.GarmentC.Tags.Pattern.Type);
            if (cb.GarmentD != null) pt.Add(cb.GarmentD.Tags.Pattern.Type);
            if (cb.GarmentE != null) pt.Add(cb.GarmentE.Tags.Pattern.Type);

            HashSet<StructureType> st = new HashSet<StructureType>();
            if (cb.GarmentA != null) st.Add(cb.GarmentA.Tags.Silouhette.Structure.Type);
            if (cb.GarmentB != null) st.Add(cb.GarmentB.Tags.Silouhette.Structure.Type);
            if (cb.GarmentC != null) st.Add(cb.GarmentC.Tags.Silouhette.Structure.Type);
            if (cb.GarmentD != null) st.Add(cb.GarmentD.Tags.Silouhette.Structure.Type);
            if (cb.GarmentE != null) st.Add(cb.GarmentE.Tags.Silouhette.Structure.Type);

            HashSet<ShapeType> sh = new HashSet<ShapeType>();
            if (cb.GarmentA != null) sh.Add(cb.GarmentA.Tags.Silouhette.Shape.Type);
            if (cb.GarmentB != null) sh.Add(cb.GarmentB.Tags.Silouhette.Shape.Type);
            if (cb.GarmentC != null) sh.Add(cb.GarmentC.Tags.Silouhette.Shape.Type);
            if (cb.GarmentD != null) sh.Add(cb.GarmentD.Tags.Silouhette.Shape.Type);
            if (cb.GarmentE != null) sh.Add(cb.GarmentE.Tags.Silouhette.Shape.Type);

            HashSet<Color> colors = new HashSet<Color>();
            if (cb.GarmentA != null) colors.Add(cb.GarmentA.Tags.Colors[0]);
            if (cb.GarmentB != null) colors.Add(cb.GarmentB.Tags.Colors[0]);
            if (cb.GarmentC != null) colors.Add(cb.GarmentC.Tags.Colors[0]);
            if (cb.GarmentD != null) colors.Add(cb.GarmentD.Tags.Colors[0]);
            if (cb.GarmentE != null) colors.Add(cb.GarmentE.Tags.Colors[0]);

            return IsValidCombination(
                        colorFamiliesHash,
                        colors,
                        pt,
                        st,
                        sr,
                        sh,
                        cb.GarmentA.Tags.Silouhette.Layers.Contains(LayerCode.Ai),
                        false);
        }

        public static bool IsValidCombination(HashSet<ColorFamily> colorFamiliesHash, HashSet<Color> colors, HashSet<PatternType> pt, HashSet<StructureType> st, StyleRule sr, HashSet<ShapeType> sh, bool isAiLayer, bool IsUpdaterAccessory)
        {
            bool colorMatched = false;

            IList<ColorFamily> colorFamilies = colorFamiliesHash.ToList<ColorFamily>();
            if (sr.ColorBlending.NeutralColor)
            {
                colorMatched = true;
                foreach (ColorFamily cf in colorFamilies)
                    if (!cf.IsNeutral)
                        colorMatched = false;
            }

            if (!colorMatched && sr.ColorBlending.MonotoneColor)
                colorMatched = (colorFamilies.Count == 1);

            if (!colorMatched && sr.ColorBlending.AnalogousColor)
            {
                if (colorFamilies.Count == 2)
                    colorMatched = (colorFamilies[0].AnalogousFamily == colorFamilies[1] || colorFamilies[0].AnalogousFamily2 == colorFamilies[1]);
            }

            if (!colorMatched && sr.ColorBlending.ComplimentaryColor)
                if (colorFamilies.Count == 2)
                    colorMatched = (colorFamilies[0].ComplimentaryFamily == colorFamilies[1]);

            if (!colorMatched && sr.ColorBlending.NeutralPrimaryColor)
            {
                colorMatched = true;
                foreach (ColorFamily cf in colorFamilies)
                    if (!cf.IsNeutral && !cf.IsPrimary)
                        colorMatched = false;
            }

            if (!colorMatched && sr.ColorBlending.NeutralSecondaryColor)
            {
                colorMatched = true;
                foreach (ColorFamily cf in colorFamilies)
                    if (!cf.IsNeutral && !cf.IsSecondary)
                        colorMatched = false;
            }

            if (!colorMatched)
                return false;

            // HACK: If we have brown and gray, exclude.
            if (colorMatched && colors != null)
            {
                bool found = false;
                foreach (Color c in colors)
                    if (c.Id == 6)
                        found = true;

                if (found)
                {
                    foreach (Color c in colors)
                        if (c.Id == 2)
                            colorMatched = false;
                }
            }

            if (!IsUpdaterAccessory)
            {
                bool matched = false;
                foreach (PatternRule pr in sr.Patterns)
                {
                    if (pt.Count == 1 && pr.FromItem == pr.ToItem && pt.Contains(pr.FromItem))
                        matched = true;
                    else if (pt.Count == 2 && pr.FromItem != pr.ToItem && pt.Contains(pr.FromItem) && pt.Contains(pr.ToItem))
                        matched = true;
                }


                if (!matched)
                    return false;

                matched = false;
                foreach (StructureRule pr in sr.Structures)
                {
                    if (st.Count == 1 && pr.FromItem == pr.ToItem && st.Contains(pr.FromItem))
                        matched = true;
                    else if (st.Count == 2 && pr.FromItem != pr.ToItem && st.Contains(pr.FromItem) && st.Contains(pr.ToItem))
                        matched = true;
                }

                if (!matched)
                    return false;

                if (isAiLayer)
                {
                    matched = false;
                    foreach (ShapeRule shapeRule in sr.Shapes)
                    {
                        if (sh.Count == 1 && shapeRule.FromItem == shapeRule.ToItem && sh.Contains(shapeRule.FromItem))
                            matched = true;
                        else if (sh.Count == 2 && shapeRule.FromItem != shapeRule.ToItem && sh.Contains(shapeRule.FromItem) && sh.Contains(shapeRule.ToItem))
                            matched = true;
                    }

                    if (!matched)
                        return false;
                }
            }

            return true;
        }


    }
}
