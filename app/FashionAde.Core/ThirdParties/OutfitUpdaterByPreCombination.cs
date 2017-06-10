using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.OutfitEngine;
using SharpArch.Core.DomainModel;

namespace FashionAde.Core.ThirdParties
{
    public class OutfitUpdaterByPreCombination
    {
        private OutfitUpdater outfitUpdater;
        private PreCombination preCombination;

        public virtual OutfitUpdater OutfitUpdater
        {
            get { return outfitUpdater; }
            set { outfitUpdater = value; }
        }

        public virtual PreCombination PreCombination
        {
            get { return preCombination; }
            set { preCombination = value; }
        }

        public OutfitUpdaterByPreCombination() { }

        public OutfitUpdaterByPreCombination(OutfitUpdater outfitUpdater, PreCombination preCombination)
        {
            this.outfitUpdater = outfitUpdater;
            this.preCombination = preCombination;
        }
    }
}
