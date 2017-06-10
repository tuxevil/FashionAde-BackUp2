using System.Collections.Generic;
using FashionAde.Core.OutfitCombination;
using SharpArch.Core.PersistenceSupport;

namespace FashionAde.Core.DataInterfaces
{
    public interface IFashionFlavorRepository : IRepository<FashionFlavor>
    {
        IList<FashionFlavor> GetByIds(IList<int> ids);
    }
}