using FashionAde.Core.Clothing;
using FashionAde.Core.OutfitCombination;
using SharpArch.Core.PersistenceSupport;
using FashionAde.Core.OutfitEngine;
using System.Collections.Generic;

namespace FashionAde.Core.DataInterfaces
{
    public interface IPreCombinationRepository : IRepository<PreCombination>
    {
        PreCombination GetByGarments(IList<Garment> garments, FashionFlavor ff);
        IList<PreCombination> GetFirsts(int maxResults);
        IList<PreCombination> GetNews();
        IList<PreCombination> GetNewsFor(Closet closet);
        IList<PreCombination> GetOnly(PreCombinationStatus status);
        void ChangePreCombinationsStatus(Closet closet);
    }
}