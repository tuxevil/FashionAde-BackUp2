//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using NUnit.Framework;
//using FashionAde.ApplicationServices;
//using FashionAde.Core.DataInterfaces;
//using Rhino.Mocks;
//using FashionAde.Core;
//using FashionAde.Core.Clothing;
//using FashionAde.Core.OutfitCombination;
//using FashionAde.Core.OutfitEngine;
//using FashionAde.Core.Accounts;

//namespace Tests.FashionAde.ApplicationServices
//{

//    [Category("LongRunning")]
//    public class OutfitEngineTest
//    {
//        Random rnd = new Random();

//        public void CanGenerateValidCombinations()
//        {
//            //IOutfitEngineProcessor processor = new OutfitEngineProcessor(CreateMockStyleRulesRepository());

//            //processor.Garments = CreatePreGarments();
//            //processor.FashionFlavors = CreateFashionFlavors(); //.Add(CreateFashionFlavors()[0]);
//            //processor.EventTypes.Add(CreateEventTypes()[0]);

//            //Assert.IsTrue(processor.HasValidCombinations());
//        }

//        private Closet CreateCloset()
//        {
//            IList<PreGarment> garments = CreatePreGarments();
//            Closet c = new Closet();
//            c.User = new RegisteredUser();
//            IList<UserFlavor> flavors = new List<UserFlavor>();
//            flavors.Add(new UserFlavor(CreateFashionFlavors()[0], 1));
//            //flavors.Add(new UserFlavor(CreateFashionFlavors()[1], 1));
//            //flavors.Add(new UserFlavor(CreateFashionFlavors()[2], 1));
//            //flavors.Add(new UserFlavor(CreateFashionFlavors()[3], 1));
//            //flavors.Add(new UserFlavor(CreateFashionFlavors()[4], 1));
//            //flavors.Add(new UserFlavor(CreateFashionFlavors()[5], 1));
//            c.User.SetFlavors(flavors);
//            c.User.AddEventType(CreateEventTypes()[0]);
//            //c.User.AddEventType(CreateEventTypes()[1]);
//            //c.User.AddEventType(CreateEventTypes()[2]);
//            //c.User.AddEventType(CreateEventTypes()[3]);
//            //c.User.AddEventType(CreateEventTypes()[4]);
//            //c.User.AddEventType(CreateEventTypes()[5]);

//            foreach (PreGarment pg in garments)
//                c.AddGarment(CreateGarment(pg));

//            return c;
//        }

//        public Garment CreateGarment(PreGarment pregarment)
//        {
//            Garment g = new Garment();
//            SharpArch.Testing.EntityIdSetter.SetIdOf<int>(g, pregarment.Id);

//            //g.Tags.Silouhette = new Silouhette { PreSilouhette = pregarment.Silouhette };
//            g.Tags.Pattern = new Pattern { Type = pregarment.PatternType };
//            g.Tags.Colors.Add(new Color { Family = pregarment.ColorFamily });

//            g.Tags.Seasons.Add(Season.Summer);
//            g.Tags.EventTypes.Add(CreateEventTypes()[0]);
//            g.PreGarment = pregarment;

//            //if (rnd.Next(0, 5) != 0) // Some could be for all seasons.
//            //{
//            //    // Most logically they will be used on two seasons.
//            //    if (rnd.Next(0, 2) == 0)
//            //    {
//            //        g.Tags.Seasons.Add(Season.Summer);
//            //        g.Tags.Seasons.Add(Season.Spring);
//            //    }
//            //    else
//            //    {
//            //        g.Tags.Seasons.Add(Season.Winter);
//            //        g.Tags.Seasons.Add(Season.Fall);
//            //    }
//            //}
//            //else
//            //{
//                //foreach (Season season in Enum.GetValues(typeof(Season)))
//                //    g.Tags.Seasons.Add(season);
//            //}

//            //if (rnd.Next(0, 5) != 0) // Some could be for all seasons.
//            //{
//            //    g.Tags.EventTypes.Add(GetRandomEventType());
//            //    g.Tags.EventTypes.Add(GetRandomEventType());
//            //}
//            //else
//            //{
//                //foreach (EventType eventType in CreateEventTypes())
//                //    g.Tags.EventTypes.Add(eventType);
//            //}

//            return g;
//        }

//        #region Style Rules

//        private IList<StyleRule> CreateStyleRules()
//        {
//            IList<StyleRule> lst = new List<StyleRule>();
//            foreach(FashionFlavor fv in CreateFashionFlavors())
//                lst.Add(CreateStyleRule(fv));
//            return lst;
//        }

//        private StyleRule CreateStyleRule(FashionFlavor fv)
//        {
//            StyleRule sr = new StyleRule();
//            sr.MaximumGarments = 10;
//            sr.MinimumGarments = 2;
//            sr.FashionFlavor = fv;

//            int maximumLayers = 5;
//            int minimumLayers = 2;

//            switch (fv.Name)
//            {
//                case "Classic":
//                    maximumLayers = 5;
//                    minimumLayers = 3;
//                    sr.ColorBlending.MonotoneColor = true;
//                    sr.ColorBlending.NeutralColor = true;
//                    sr.Patterns.Add(new PatternRule { FromItem = PatternType.Solid, ToItem = PatternType.Solid });
//                    sr.Patterns.Add(new PatternRule { FromItem = PatternType.Solid, ToItem = PatternType.Minimal });

//                    sr.Structures.Add(new StructureRule { FromItem = StructureType.Structured, ToItem = StructureType.Structured });
//                    sr.Structures.Add(new StructureRule { FromItem = StructureType.Structured, ToItem = StructureType.FlowingFluid });
//                    sr.Structures.Add(new StructureRule { FromItem = StructureType.Structured, ToItem = StructureType.Relaxed });
//                    sr.Structures.Add(new StructureRule { FromItem = StructureType.Structured, ToItem = StructureType.BodyConscious });
///*
//                    sr.Shapes.Add(new ShapeRule { FromItem = ShapeType.Slim, ToItem = ShapeType.Slim });
//                    sr.Shapes.Add(new ShapeRule { FromItem = ShapeType.Slim, ToItem = ShapeType.Fitted });
//                    sr.Shapes.Add(new ShapeRule { FromItem = ShapeType.Fitted, ToItem = ShapeType.Slim });
//                    sr.Shapes.Add(new ShapeRule { FromItem = ShapeType.Fitted, ToItem = ShapeType.Fitted });
//*/
//                    sr.AccessoriesAmount.Add(3);
//                    break;
//                case "Romantic":
//                    maximumLayers = 5;
//                    minimumLayers = 1;
//                    sr.ColorBlending.MonotoneColor = true;
//                    sr.ColorBlending.AnalogousColor = true;
//                    sr.ColorBlending.NeutralColor = true;
//                    sr.AccessoriesAmount.Add(4);

//                    sr.Structures.Add(new StructureRule { FromItem = StructureType.Structured, ToItem = StructureType.FlowingFluid });
//                    sr.Structures.Add(new StructureRule { FromItem = StructureType.FlowingFluid, ToItem = StructureType.FlowingFluid });
//                    sr.Structures.Add(new StructureRule { FromItem = StructureType.FlowingFluid, ToItem = StructureType.Relaxed });

//                    break;
//                case "Comfortable":
//                    maximumLayers = 3;
//                    minimumLayers = 1;
//                    sr.ColorBlending.ComplimentaryColor = true;
//                    sr.ColorBlending.NeutralPrimaryColor = true;
//                    sr.ColorBlending.NeutralSecondaryColor = true;
//                    sr.ColorBlending.NeutralColor = true;
//                    sr.Patterns.Add(new PatternRule { FromItem = PatternType.Solid, ToItem = PatternType.Solid });
//                    sr.Patterns.Add(new PatternRule { FromItem = PatternType.Solid, ToItem = PatternType.Minimal });

//                    sr.AccessoriesAmount.Add(1);
//                    sr.AccessoriesAmount.Add(3);

//                    sr.Structures.Add(new StructureRule { FromItem = StructureType.Structured, ToItem = StructureType.Relaxed });
//                    sr.Structures.Add(new StructureRule { FromItem = StructureType.FlowingFluid, ToItem = StructureType.Relaxed });
//                    sr.Structures.Add(new StructureRule { FromItem = StructureType.Relaxed, ToItem = StructureType.BodyConscious });


//                    break;
//                case "Sporty":
//                    maximumLayers = 3;
//                    minimumLayers = 1;
//                    sr.ColorBlending.ComplimentaryColor = true;
//                    sr.ColorBlending.NeutralPrimaryColor = true;
//                    sr.ColorBlending.NeutralSecondaryColor = true;
//                    sr.ColorBlending.NeutralColor = true;
//                    sr.AccessoriesAmount.Add(1);
//                    sr.AccessoriesAmount.Add(3);
//                    sr.Patterns.Add(new PatternRule { FromItem = PatternType.Solid, ToItem = PatternType.Solid });
//                    sr.Patterns.Add(new PatternRule { FromItem = PatternType.Solid, ToItem = PatternType.Minimal });

//                    sr.Structures.Add(new StructureRule { FromItem = StructureType.Structured, ToItem = StructureType.BodyConscious });
//                    sr.Structures.Add(new StructureRule { FromItem = StructureType.Relaxed, ToItem = StructureType.Relaxed });
//                    sr.Structures.Add(new StructureRule { FromItem = StructureType.Relaxed, ToItem = StructureType.BodyConscious });

//                    break;
//                case "Preppy":
//                    maximumLayers = 5;
//                    minimumLayers = 2;
//                    sr.ColorBlending.NeutralPrimaryColor = true;
//                    sr.AccessoriesAmount.Add(1);
//                    sr.AccessoriesAmount.Add(3);
//                    sr.Patterns.Add(new PatternRule { FromItem = PatternType.Solid, ToItem = PatternType.Solid });
//                    sr.Patterns.Add(new PatternRule { FromItem = PatternType.Solid, ToItem = PatternType.Minimal });

//                    sr.Structures.Add(new StructureRule { FromItem = StructureType.Structured, ToItem = StructureType.Structured });
//                    sr.Structures.Add(new StructureRule { FromItem = StructureType.Structured, ToItem = StructureType.Relaxed });
//                    sr.Structures.Add(new StructureRule { FromItem = StructureType.Relaxed, ToItem = StructureType.Relaxed });

//                    break;
//                case "Glam":
//                    maximumLayers = 3;
//                    minimumLayers = 1;
//                    sr.ColorBlending.MonotoneColor = true;
//                    sr.ColorBlending.ComplimentaryColor = true;
//                    sr.ColorBlending.NeutralPrimaryColor = true;
//                    sr.ColorBlending.NeutralSecondaryColor = true;
//                    sr.AccessoriesAmount.Add(5);
//                    sr.Patterns.Add(new PatternRule { FromItem = PatternType.Solid, ToItem = PatternType.Solid });
//                    sr.Patterns.Add(new PatternRule { FromItem = PatternType.Solid, ToItem = PatternType.Minimal });

//                    sr.Structures.Add(new StructureRule { FromItem = StructureType.Structured, ToItem = StructureType.Structured });
//                    sr.Structures.Add(new StructureRule { FromItem = StructureType.Structured, ToItem = StructureType.BodyConscious });
//                    sr.Structures.Add(new StructureRule { FromItem = StructureType.Relaxed, ToItem = StructureType.BodyConscious });
//                    sr.Structures.Add(new StructureRule { FromItem = StructureType.BodyConscious, ToItem = StructureType.BodyConscious });


//                    break;
//                case "Bohemian":
//                    maximumLayers = 5;
//                    minimumLayers = 3;
//                    sr.ColorBlending.AnalogousColor = true;
//                    sr.ColorBlending.ComplimentaryColor = true;
//                    sr.ColorBlending.NeutralPrimaryColor = true;
//                    sr.AccessoriesAmount.Add(4);
//                    sr.Patterns.Add(new PatternRule { FromItem = PatternType.Solid, ToItem = PatternType.Bold });
//                    sr.Patterns.Add(new PatternRule { FromItem = PatternType.Solid, ToItem = PatternType.Minimal });
//                    sr.Patterns.Add(new PatternRule { FromItem = PatternType.Minimal, ToItem = PatternType.Minimal });
//                    sr.Patterns.Add(new PatternRule { FromItem = PatternType.Minimal, ToItem = PatternType.Bold });
//                    sr.Patterns.Add(new PatternRule { FromItem = PatternType.Bold, ToItem = PatternType.Bold });

//                    sr.Structures.Add(new StructureRule { FromItem = StructureType.Structured, ToItem = StructureType.FlowingFluid });
//                    sr.Structures.Add(new StructureRule { FromItem = StructureType.FlowingFluid, ToItem = StructureType.FlowingFluid });
//                    sr.Structures.Add(new StructureRule { FromItem = StructureType.FlowingFluid, ToItem = StructureType.Relaxed });


//                    break;
//                case "Trendy":
//                    maximumLayers = 5;
//                    minimumLayers = 1;
//                    sr.ColorBlending.MonotoneColor = true;
//                    sr.ColorBlending.AnalogousColor = true;
//                    sr.ColorBlending.ComplimentaryColor = true;
//                    sr.ColorBlending.NeutralPrimaryColor = true;
//                    sr.ColorBlending.NeutralSecondaryColor = true;
//                    sr.ColorBlending.NeutralColor = true;
//                    sr.AccessoriesAmount.Add(4);
//                    sr.Patterns.Add(new PatternRule { FromItem = PatternType.Solid, ToItem = PatternType.Solid });
//                    sr.Patterns.Add(new PatternRule { FromItem = PatternType.Solid, ToItem = PatternType.Minimal });
//                    sr.Patterns.Add(new PatternRule { FromItem = PatternType.Solid, ToItem = PatternType.Bold });
//                    sr.Patterns.Add(new PatternRule { FromItem = PatternType.Minimal, ToItem = PatternType.Bold });

//                    sr.Structures.Add(new StructureRule { FromItem = StructureType.Structured, ToItem = StructureType.Structured });
//                    sr.Structures.Add(new StructureRule { FromItem = StructureType.Structured, ToItem = StructureType.FlowingFluid });
//                    sr.Structures.Add(new StructureRule { FromItem = StructureType.Structured, ToItem = StructureType.Relaxed });
//                    sr.Structures.Add(new StructureRule { FromItem = StructureType.Structured, ToItem = StructureType.BodyConscious });
//                    sr.Structures.Add(new StructureRule { FromItem = StructureType.FlowingFluid, ToItem = StructureType.FlowingFluid });
//                    sr.Structures.Add(new StructureRule { FromItem = StructureType.FlowingFluid, ToItem = StructureType.Relaxed });
//                    sr.Structures.Add(new StructureRule { FromItem = StructureType.FlowingFluid, ToItem = StructureType.BodyConscious });
//                    sr.Structures.Add(new StructureRule { FromItem = StructureType.Relaxed, ToItem = StructureType.Relaxed });
//                    sr.Structures.Add(new StructureRule { FromItem = StructureType.Relaxed, ToItem = StructureType.BodyConscious });
//                    sr.Structures.Add(new StructureRule { FromItem = StructureType.BodyConscious, ToItem = StructureType.BodyConscious });

//                    break;
//            }

//            sr.MaximumLayers = maximumLayers;
//            sr.MinimumLayers = minimumLayers;
//            return sr;
//        }

//        #endregion

//        #region Mocked Repositories

//        private IPreGarmentRepository CreateMockPreGarmentRepository()
//        {
//            IPreGarmentRepository repository = MockRepository.GenerateMock<IPreGarmentRepository>();
//            repository.Expect(r => r.GetAll()).Return(CreatePreGarments());
//            return repository;
//        }

//        private IStyleRuleRepository CreateMockStyleRulesRepository()
//        {
//            IStyleRuleRepository repository = MockRepository.GenerateMock<IStyleRuleRepository>();
//            repository.Expect(r => r.GetAll()).Return(CreateStyleRules());
//            return repository;
//        }

//        private IClosetRepository CreateMockClosetRepository()
//        {
//            IClosetRepository repository = MockRepository.GenerateMock<IClosetRepository>();
//            repository.Expect(delegate { repository.SaveOrUpdate(null); }).IgnoreArguments();
//            return repository;
//        }

//        private IFashionFlavorRepository CreateMockFashionFlavorRepository()
//        {
//            IFashionFlavorRepository repository = MockRepository.GenerateMock<IFashionFlavorRepository>();
//            repository.Expect(r => r.GetAll()).Return(CreateFashionFlavors());
//            return repository;
//        }

//        private IEventTypeRepository CreateMockEventTypeRepository()
//        {
//            IEventTypeRepository repository = MockRepository.GenerateMock<IEventTypeRepository>();
//            repository.Expect(r => r.GetAll()).Return(CreateEventTypes());
//            return repository;
//        }

//        #endregion

//        #region Data Creation Methods

//        IList<EventType> eventTypes = new List<EventType>();
//        private IList<EventType> CreateEventTypes()
//        {
//            if (eventTypes.Count == 0)
//            {
//                eventTypes.Add(new EventType { Name = "EventType1" });
//                eventTypes.Add(new EventType { Name = "EventType2" });
//                eventTypes.Add(new EventType { Name = "EventType3" });
//                eventTypes.Add(new EventType { Name = "EventType4" });
//                eventTypes.Add(new EventType { Name = "EventType5" });
//                eventTypes.Add(new EventType { Name = "EventType6" });
//            }
//            return eventTypes;
//        }

//        IList<FashionFlavor> flavors = new List<FashionFlavor>();
//        private IList<FashionFlavor> CreateFashionFlavors()
//        {
//            if (flavors.Count == 0)
//            {
//                flavors.Add(new FashionFlavor { Name = "Classic" });
//                flavors.Add(new FashionFlavor { Name = "Romantic" });
//                flavors.Add(new FashionFlavor { Name = "Comfortable" });
//                flavors.Add(new FashionFlavor { Name = "Sporty" });
//                flavors.Add(new FashionFlavor { Name = "Preppy" });
//                flavors.Add(new FashionFlavor { Name = "Glam" });
//                flavors.Add(new FashionFlavor { Name = "Bohemian" });
//                flavors.Add(new FashionFlavor { Name = "Trendy" });
//            }

//            return flavors;
//        }

//        public IList<PreGarment> CreatePreGarments()
//        {
//            int i = 1;
//            IList<PreGarment> lstGarments = new List<PreGarment>();
//            foreach (Silouhette sl in CreateSilouhettes())
//            {
//                foreach (ColorFamily color in CreateColorFamilies())
//                {
//                    if (sl.Layers.Contains(LayerCode.ACC1) ||
//                        sl.Layers.Contains(LayerCode.ACC2) ||
//                        sl.Layers.Contains(LayerCode.ACC3) ||
//                        sl.Layers.Contains(LayerCode.ACC4) ||
//                        sl.Layers.Contains(LayerCode.ACC5) ||
//                        sl.Layers.Contains(LayerCode.ACC6) ||
//                        sl.Layers.Contains(LayerCode.ACC7) ||
//                        sl.Layers.Contains(LayerCode.ACC8))
//                    {
//                        PreGarment g = CreatePreGarment(sl, color, PatternType.Solid);
//                        SharpArch.Testing.EntityIdSetter.SetIdOf<int>(g, i);
//                        i++;
//                        lstGarments.Add(g);
//                        continue;
//                    }

//                    foreach(PatternType patternType in Enum.GetValues(typeof(PatternType)))
//                    {
//                        PreGarment g = CreatePreGarment(sl, color, patternType);
//                        SharpArch.Testing.EntityIdSetter.SetIdOf<int>(g, i);
//                        i++;
//                        lstGarments.Add(g);
//                    }
//                }
//            }

//            return lstGarments;
//        }

//        private PreGarment CreatePreGarment(Silouhette s, ColorFamily cf, PatternType pt)
//        {
//            PreGarment g = new PreGarment();
//            //g.PreSilouhette = s.PreSilouhette;
//            g.ColorFamily = cf;
//            g.PatternType = pt;

//            return g;
//        }

//        private FashionFlavor GetRandomFlavor()
//        {
//            return CreateFashionFlavors()[rnd.Next(0, 4)];
//        }

//        private EventType GetRandomEventType()
//        {
//            return CreateEventTypes()[rnd.Next(0, 4)];
//        }

//        private Color GetRandomColor()
//        {
//            return CreateColors()[rnd.Next(0, 40)];
//        }

//        private ColorFamily GetRandomFamilyColor()
//        {
//            return CreateColorFamilies()[rnd.Next(0, 7)];
//        }


//        private Structure GetRandomStructure()
//        {
//            return CreateStructures()[rnd.Next(0, 4)];
//        }

//        private Shape GetRandomShape()
//        {
//            return CreateShapes()[rnd.Next(0, 6)];
//        }

//        private Pattern GetRandomPattern()
//        {
//            return CreatePatterns()[rnd.Next(0, 3)];
//        }

//        private IList<Silouhette> CreateSilouhettes()
//        {
//            IList<Silouhette> lst = new List<Silouhette>();
//            int i = 0;
//            for(i=1; i<=7; i++)
//                lst.Add(CreateSilouhette(string.Format("LayerAi_{0}",i), LayerCode.Ai));
//            for (i = 1; i <= 8; i++)
//                lst.Add(CreateSilouhette(string.Format("LayerA_{0}", i), LayerCode.A));
////            for (int i = 1; i <= 25; i++)
//                lst.Add(CreateSilouhette(string.Format("ACC1_{0}", i), LayerCode.ACC1));
////            for (int i = 1; i <= 10; i++)
//                lst.Add(CreateSilouhette(string.Format("ACC2_{0}", i), LayerCode.ACC2));
////            for (int i = 1; i <= 5; i++)
//                lst.Add(CreateSilouhette(string.Format("ACC3_{0}", i), LayerCode.ACC3));
////            for (int i = 1; i <= 2; i++)
//                lst.Add(CreateSilouhette(string.Format("ACC4_{0}", i), LayerCode.ACC4));
////            for (int i = 1; i <= 4; i++)
//                lst.Add(CreateSilouhette(string.Format("ACC5_{0}", i), LayerCode.ACC5));
////            for (int i = 1; i <= 4; i++)
//                lst.Add(CreateSilouhette(string.Format("ACC6_{0}", i), LayerCode.ACC6));
////            for (int i = 1; i <= 5; i++)
//                lst.Add(CreateSilouhette(string.Format("ACC7_{0}", i), LayerCode.ACC7));
////            for (int i = 1; i <= 9; i++)
//                lst.Add(CreateSilouhette(string.Format("ACC8_{0}", i), LayerCode.ACC8));
//            for (i = 1; i <= 3; i++)
//                lst.Add(CreateSilouhette(string.Format("Aii_{0}", i), LayerCode.Aii));
//            for (i = 1; i <= 3; i++)
//                lst.Add(CreateSilouhette(string.Format("C_{0}", i), LayerCode.C));
//            for (i = 1; i <= 2; i++)
//                lst.Add(CreateSilouhette(string.Format("D_{0}", i), LayerCode.D));
////            for (int i = 1; i <= 9; i++)
//                lst.Add(CreateSilouhette(string.Format("AiiB_{0}", i), LayerCode.Aii, LayerCode.B));
////            for (int i = 1; i <= 2; i++)
//                lst.Add(CreateSilouhette(string.Format("AiiBC_{0}", i), LayerCode.Aii, LayerCode.B, LayerCode.C));
//            for (i = 1; i <= 2; i++)
//                lst.Add(CreateSilouhette(string.Format("BC_{0}", i), LayerCode.B, LayerCode.C));

//            return lst;
//        }

//        private Silouhette CreateSilouhette(string description, params LayerCode[] layerCodes)
//        {
//            Silouhette sil = new Silouhette { Description = description };
//            //foreach (LayerCode lc in layerCodes)
//            //    sil.Layers.Add(lc);
//            sil.Layers.Add(layerCodes[0]);
//            sil.Shape.Type = GetRandomShape().Type;
//            sil.Structure.Type = GetRandomStructure().Type;

//            if (rnd.Next(0, 5) != 0) // Some could be for all flavors.
//            {
//                sil.FashionFlavors.Add(CreateFashionFlavors()[rnd.Next(0, 8)]);
//            }
//            else
//            {
//                foreach (FashionFlavor flavor in CreateFashionFlavors())
//                    sil.FashionFlavors.Add(flavor);
//            }

//            //if (rnd.Next(0, 5) != 0) // Some could be for all seasons.
//            //{
//            //    // Most logically they will be used on two seasons.
//            //    if (rnd.Next(0, 2) == 0)
//            //    {
//            //        sil.Seasons.Add(Season.Summer);
//            //        sil.Seasons.Add(Season.Spring);
//            //    }
//            //    else
//            //    {
//            //        sil.Seasons.Add(Season.Winter);
//            //        sil.Seasons.Add(Season.Fall);
//            //    }
//            //}
//            //else
//            //{
//                foreach (Season season in Enum.GetValues(typeof(Season)))
//                    sil.Seasons.Add(season);
//            //}

//            //if (rnd.Next(0, 5) != 0) // Some could be for all seasons.
//            //{
//            //    sil.EventTypes.Add(GetRandomEventType());
//            //    sil.EventTypes.Add(GetRandomEventType());
//            //    sil.EventTypes.Add(GetRandomEventType());
//            //}
//            //else
//            //{
//                foreach (EventType eventType in CreateEventTypes())
//                    sil.EventTypes.Add(eventType);
//            //}

//            return sil;
//        }

//        IList<Structure> structures = new List<Structure>();
//        private IList<Structure> CreateStructures()
//        {
//            if (structures.Count == 0)
//            {
//                structures.Add(new Structure { Description = "Structured", Type = StructureType.Structured });
//                structures.Add(new Structure { Description = "Flowing Fluid", Type = StructureType.FlowingFluid});
//                structures.Add(new Structure { Description = "Relaxed", Type = StructureType.Relaxed });
//                structures.Add(new Structure { Description = "Body Conscious", Type = StructureType.BodyConscious });
//            }
//            return structures;
//        }

//        IList<Shape> shapes = new List<Shape>();
//        private IList<Shape> CreateShapes()
//        {
//            if (shapes.Count == 0)
//            {
///*                shapes.Add(new Shape { Description = "Slim Top", Type = ShapeType.Slim });
//                shapes.Add(new Shape { Description = "Slim Bottom", Type = ShapeType.Slim });
//                shapes.Add(new Shape { Description = "Fitted Bottom", Type = ShapeType.Fitted });
//                shapes.Add(new Shape { Description = "Fitted Top", Type = ShapeType.Fitted });
//                shapes.Add(new Shape { Description = "Loose Bottom", Type = ShapeType.Loose });
//                shapes.Add(new Shape { Description = "Loose Top", Type = ShapeType.Loose });
// */           }
//            return shapes;
//        }

//        IList<ColorFamily> colorFamilies = new List<ColorFamily>();
//        private IList<ColorFamily> CreateColorFamilies()
//        {
//            if (colorFamilies.Count == 0)
//            {
//                colorFamilies.Add(new ColorFamily { Description = "Neutral Family", IsNeutral = true });
//                colorFamilies.Add(new ColorFamily { Description = "Green Family", IsSecondary = true });
//                colorFamilies.Add(new ColorFamily { Description = "Red Family", IsPrimary = true });
//                colorFamilies.Add(new ColorFamily { Description = "Purple Family", IsSecondary = true });
//                colorFamilies.Add(new ColorFamily { Description = "Blue Family", IsPrimary = true });
//                colorFamilies.Add(new ColorFamily { Description = "Yellow Family", IsPrimary = true });
//                colorFamilies.Add(new ColorFamily { Description = "Orange Family", IsSecondary = true });
//            }
//            return colorFamilies;
//        }

//        IList<Color> colors = new List<Color>();
//        private IList<Color> CreateColors()
//        {
//            if (colors.Count == 0)
//            {
//                for (int i=0; i<=6; i++) 
//                    colors.Add(new Color { Description = "Color" + i, Family = GetRandomFamilyColor() });
//            }
//            return colors;
//        }

//        private Pattern FindPattern(string name)
//        {
//            IList<Pattern> lst = CreatePatterns();
//            return (from p in lst where p.Description == name select p).First<Pattern>();
//        }

//        private Shape FindShape(string name)
//        {
//            IList<Shape> lst = CreateShapes();
//            return (from p in lst where p.Description == name select p).First<Shape>();
//        }

//        private Structure FindStructure(string name)
//        {
//            IList<Structure> lst = CreateStructures();
//            return (from p in lst where p.Description == name select p).First<Structure>();
//        }

//        IList<Pattern> patterns = new List<Pattern>();
//        private IList<Pattern> CreatePatterns()
//        {
//            if (patterns.Count == 0)
//            {
//                patterns.Add(new Pattern { Description = "Solid", Type = PatternType.Solid });
//                //patterns.Add(new Pattern { Description = "Minimal Pattern", Type = PatternType.Minimal });
//                //patterns.Add(new Pattern { Description = "Bold Pattern", Type = PatternType.Bold });
//                //patterns.Add(new Pattern { Description = "Pattern2", Type = PatternType.Minimal });
//                //patterns.Add(new Pattern { Description = "Pattern2", Type = PatternType.Bold });
//                //patterns.Add(new Pattern { Description = "Pattern2", Type = PatternType.Minimal });
//                //patterns.Add(new Pattern { Description = "Pattern2", Type = PatternType.Bold });
//                //patterns.Add(new Pattern { Description = "Pattern2", Type = PatternType.Minimal });
//                //patterns.Add(new Pattern { Description = "Pattern2", Type = PatternType.Bold });
//                //patterns.Add(new Pattern { Description = "Pattern2", Type = PatternType.Minimal });
//                //patterns.Add(new Pattern { Description = "Pattern2", Type = PatternType.Bold });
//                //patterns.Add(new Pattern { Description = "Pattern2", Type = PatternType.Bold });
//                //patterns.Add(new Pattern { Description = "Pattern2", Type = PatternType.Bold });
//                //patterns.Add(new Pattern { Description = "Pattern2", Type = PatternType.Bold });
//            }
//            return patterns;
//        }

//        IList<Fabric> fabrics = new List<Fabric>();
//        private IList<Fabric> CreateFabrics()
//        {
//            if (fabrics.Count == 0)
//            {
//                fabrics.Add(new Fabric { Description = "Fabric1" });
//                fabrics.Add(new Fabric { Description = "Fabric2" });
//                fabrics.Add(new Fabric { Description = "Fabric3" });
//                fabrics.Add(new Fabric { Description = "Fabric4" });
//                fabrics.Add(new Fabric { Description = "Fabric5" });
//                fabrics.Add(new Fabric { Description = "Fabric6" });
//                fabrics.Add(new Fabric { Description = "Fabric7" });
//                fabrics.Add(new Fabric { Description = "Fabric8" });
//                fabrics.Add(new Fabric { Description = "Fabric9" });
//                fabrics.Add(new Fabric { Description = "Fabric10" });
//                fabrics.Add(new Fabric { Description = "Fabric11" });
//                fabrics.Add(new Fabric { Description = "Fabric12" });
//            }

//            return fabrics;
//        }

//        #endregion

//    }
//}
