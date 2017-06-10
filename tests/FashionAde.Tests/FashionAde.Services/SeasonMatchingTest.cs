using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FashionAde.Core.Services;
using FashionAde.Core.Clothing;
using FashionAde.Core;
using FashionAde.Core.OutfitEngine;

namespace Tests.FashionAde.Services
{
    [TestFixture]
    public class SeasonMatchingTest
    {
        [Test]
        public void CanMatchSeasons()
        {
            HashSet<int> seasons = new HashSet<int>();
            HashSet<int> eventTypes = new HashSet<int>();
            PreOutfit po = new PreOutfit();

            Combination c = new Combination();
            c.GarmentA = new MasterGarment();
            c.GarmentA.EventCode = 3;
            c.GarmentA.SeasonCode = 15;

            po.Accesory1 = new MasterGarment();
            po.Accesory1.EventCode = 3;
            po.Accesory1.SeasonCode = 3;

            c.GarmentB = new MasterGarment();
            c.GarmentB.EventCode = 1;
            c.GarmentB.SeasonCode = 3;

            po.Combination = c;
            OutfitValidationService.VerifyAndSetSeasonsAndEventTypes(seasons, eventTypes, po);

            Assert.IsTrue(seasons.Sum() == 3, seasons.Sum().ToString());
            Assert.IsTrue(eventTypes.Sum() == 1, eventTypes.Sum().ToString());
        }
    }
}
