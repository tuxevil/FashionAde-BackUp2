using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using FashionAde.Core.OutfitEngine;
using FashionAde.Core.ThirdParties;

namespace FashionAde.Services
{
    [ServiceContract]
    public interface IOutfitUpdaterService
    {
        /// <summary>
        /// Updates the data of the feeds from all registered partners.
        /// </summary>
        /// <remarks>This is a one way operation</remarks>
        [OperationContract(IsOneWay = true)]
        void UpdateFeeds();

        [OperationContract(IsOneWay = true)]
        void MatchOutfitUpdatersForCloset(int closetId);
    }
}
