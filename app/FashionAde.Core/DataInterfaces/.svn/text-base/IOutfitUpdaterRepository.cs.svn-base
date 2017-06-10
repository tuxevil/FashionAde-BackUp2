using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FashionAde.Core.OutfitEngine;
using FashionAde.Core.ThirdParties;
using SharpArch.Core.PersistenceSupport;

namespace FashionAde.Core.DataInterfaces
{
    public interface IOutfitUpdaterRepository : IRepository<OutfitUpdater>
    {
        OutfitUpdater Get(string externalId);
        IList<OutfitUpdater> GetValidsFor(Partner partner);
        IList<OutfitUpdater> GetFor(Partner partner);
        IList<OutfitUpdater> GetFor(PreCombination preCombination, int pageNumber, int pageSize, out int totalCount);
        IList<OutfitUpdater> GetOnly(params OutfitUpdaterStatus[] status);
        void ProcessOutfitUpdatersByPreCombinationsFile(string fileName);
        void ChangeOutfitUpdatersStatus();
        OutfitUpdater GetRandomOutfitUpdaterFor(PreCombination preCombination);
    }
}
