using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FashionAde.ApplicationServices;
using FashionAde.Core.OutfitCombination;
using FashionAde.Core.DataInterfaces;
using FashionAde.Data.Repository;
using SharpArch.Testing.NHibernate;
using FashionAde.Data.NHibernateMaps;
using SharpArch.Data.NHibernate;
using FashionAde.Core.OutfitEngine;
using FashionAde.Core;
using FashionAde.Core.Clothing;
using FashionAde.Core.Accounts;
using FashionAde.Services;

namespace Tests.FashionAde.Data
{
    [TestFixture]
    [Category("DB Tests")]
    public class ApplicationServicesTest
    {
        #region Setup
        private NHibernate.Cfg.Configuration configuration;

        [SetUp]
        public virtual void SetUp()
        {
            log4net.Config.XmlConfigurator.Configure();

            string[] mappingAssemblies = RepositoryTestsHelper.GetMappingAssemblies();
            configuration = NHibernateSession.Init(new SimpleSessionStorage(), mappingAssemblies,
                       new AutoPersistenceModelGenerator().Generate(),
                       "../../../../tests/FashionAde.Tests/Hibernate.Test.cfg.xml");
        }

        [TearDown]
        public virtual void TearDown()
        {
            NHibernateSession.CloseAllSessions();
            NHibernateSession.Reset();
        }
        #endregion

        public void CreateNewCategory()
        {
            CategoryRepository cr = new CategoryRepository();
            cr.DbContext.BeginTransaction();
            Category c = new Category();
            c.Description = "AAAAA";
            cr.SaveOrUpdate(c);
            cr.DbContext.CommitTransaction();

        }

        public void CreateStyleRules()
        {
            IFashionFlavorRepository repFlavors = new FashionFlavorRepository();
            IStyleRuleRepository repStyleRule = new StyleRuleRepository();

            foreach (FashionFlavor fv in repFlavors.GetAll())
                repStyleRule.SaveOrUpdate(CreateStyleRule(fv));
        }

        public void CanReadStyleRuleAccessories()
        {
            IStyleRuleRepository repStyleRule = new StyleRuleRepository();
            Assert.IsTrue(repStyleRule.Get(1).AccessoriesAmount.Count > 0);
        }

        [Test]
        public void CanCreatePreCombinations()
        {
            IClosetRepository closetRepository = new ClosetRepository();
            IStyleRuleRepository repStyleRule = new StyleRuleRepository();
            IFashionFlavorRepository fashionFlavorRepository = new FashionFlavorRepository();

            IOutfitUpdaterService ous = new OutfitUpdaterService(new OutfitUpdaterRepository(),
                new PreCombinationRepository(), repStyleRule);

            IOutfitEngineProcessor processor = new OutfitEngineProcessor(repStyleRule, closetRepository, ous);

            IGarmentRepository repGarments = new GarmentRepository();
            IOutfitEngineService oes = new OutfitEngineService(
                repGarments,
                closetRepository,
                processor, fashionFlavorRepository);

            oes.CreateOutfits(154);
            oes.CreateOutfits(154);
        }

        [Test]
        public void CanAddOutfits()
        {
            IClosetRepository closetRepository = new ClosetRepository();
            IStyleRuleRepository repStyleRule = new StyleRuleRepository();
            IFashionFlavorRepository fashionFlavorRepository = new FashionFlavorRepository();
            IOutfitUpdaterService ous = new OutfitUpdaterService(new OutfitUpdaterRepository(),
                new PreCombinationRepository(), repStyleRule);

            IOutfitEngineProcessor processor = new OutfitEngineProcessor(repStyleRule, closetRepository, ous);

            IGarmentRepository repGarments = new GarmentRepository();
            IOutfitEngineService oes = new OutfitEngineService(
                repGarments,
                closetRepository,
                processor, fashionFlavorRepository);

            List<int> lst = new List<int>();
            lst.Add(638377);
            oes.AddOutfits(154, lst);
        }

        #region Create Style Rules

        private StyleRule CreateStyleRule(FashionFlavor fv)
        {
            StyleRule sr = new StyleRule();
            sr.MaximumGarments = 10;
            sr.MinimumGarments = 2;
            sr.FashionFlavor = fv;

            int maximumLayers = 5;
            int minimumLayers = 2;

            switch (fv.Name)
            {
                case "Classic":
                    maximumLayers = 5;
                    minimumLayers = 3;

                    sr.ColorBlending.MonotoneColor = true;
                    sr.ColorBlending.AnalogousColor = true;
                    sr.ColorBlending.NeutralPrimaryColor = true;
                    sr.ColorBlending.NeutralColor = true;

                    sr.Patterns.Add(new PatternRule { FromItem = PatternType.Solid, ToItem = PatternType.Solid, StyleRule = sr });
                    sr.Patterns.Add(new PatternRule { FromItem = PatternType.Solid, ToItem = PatternType.Minimal, StyleRule = sr });

                    sr.Structures.Add(new StructureRule { FromItem = StructureType.Structured, ToItem = StructureType.Structured, StyleRule = sr });
                    sr.Structures.Add(new StructureRule { FromItem = StructureType.Structured, ToItem = StructureType.FlowingFluid, StyleRule = sr });
                    sr.Structures.Add(new StructureRule { FromItem = StructureType.Structured, ToItem = StructureType.Relaxed, StyleRule = sr });
                    sr.Structures.Add(new StructureRule { FromItem = StructureType.Structured, ToItem = StructureType.BodyConscious, StyleRule = sr });

                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Slim100});
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Slim200});
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Slim300});
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Slim400});
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Slim500});
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Fitted100});
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Fitted200});
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Fitted300});
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Fitted400});
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Fitted500});

                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Slim100});
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Slim200});
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Slim300});
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Slim400});
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Slim500});
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Fitted100});
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Fitted200});
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Fitted300});
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Fitted400});
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Fitted500});

                    sr.AccessoriesAmount.Add(3);
                    break;
                case "Romantic":
                    maximumLayers = 5;
                    minimumLayers = 1;

                    sr.ColorBlending.AnalogousColor = true;
                    sr.ColorBlending.ComplimentaryColor = true;
                    sr.ColorBlending.NeutralSecondaryColor = true;
                    sr.ColorBlending.NeutralColor = true;

                    sr.Patterns.Add(new PatternRule { FromItem = PatternType.Solid, ToItem = PatternType.Solid, StyleRule = sr });
                    sr.Patterns.Add(new PatternRule { FromItem = PatternType.Solid, ToItem = PatternType.Minimal, StyleRule = sr });

                    sr.AccessoriesAmount.Add(4);

                    sr.Structures.Add(new StructureRule { FromItem = StructureType.Structured, ToItem = StructureType.FlowingFluid, StyleRule = sr });
                    sr.Structures.Add(new StructureRule { FromItem = StructureType.FlowingFluid, ToItem = StructureType.FlowingFluid, StyleRule = sr });
                    sr.Structures.Add(new StructureRule { FromItem = StructureType.FlowingFluid, ToItem = StructureType.Relaxed, StyleRule = sr });

                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Fitted100 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Fitted200 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Fitted300 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Fitted400 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Fitted500 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Loose100 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Loose200 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Loose300 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Loose400 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Loose500 });

                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Loose100 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Loose200 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Loose300 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Loose400 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Loose500 });

                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Loose600Plus, ToItem = ShapeType.Slim100 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Loose600Plus, ToItem = ShapeType.Slim200 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Loose600Plus, ToItem = ShapeType.Slim300 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Loose600Plus, ToItem = ShapeType.Slim400 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Loose600Plus, ToItem = ShapeType.Slim500 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Loose600Plus, ToItem = ShapeType.Fitted100 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Loose600Plus, ToItem = ShapeType.Fitted200 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Loose600Plus, ToItem = ShapeType.Fitted300 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Loose600Plus, ToItem = ShapeType.Fitted400 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Loose600Plus, ToItem = ShapeType.Fitted500 });

                    break;
                case "Comfortable":
                    maximumLayers = 3;
                    minimumLayers = 1;

                    sr.ColorBlending.ComplimentaryColor = true;
                    sr.ColorBlending.NeutralPrimaryColor = true;
                    sr.ColorBlending.NeutralSecondaryColor = true;
                    sr.ColorBlending.NeutralColor = true;

                    sr.Patterns.Add(new PatternRule { FromItem = PatternType.Solid, ToItem = PatternType.Solid, StyleRule = sr });
                    sr.Patterns.Add(new PatternRule { FromItem = PatternType.Solid, ToItem = PatternType.Minimal, StyleRule = sr });

                    sr.AccessoriesAmount.Add(1);
                    sr.AccessoriesAmount.Add(3);

                    sr.Structures.Add(new StructureRule { FromItem = StructureType.Structured, ToItem = StructureType.Relaxed, StyleRule = sr });
                    sr.Structures.Add(new StructureRule { FromItem = StructureType.FlowingFluid, ToItem = StructureType.Relaxed, StyleRule = sr });
                    sr.Structures.Add(new StructureRule { FromItem = StructureType.Relaxed, ToItem = StructureType.BodyConscious, StyleRule = sr });

                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Fitted100 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Fitted200 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Fitted300 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Fitted400 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Fitted500 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Loose100 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Loose200 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Loose300 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Loose400 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Loose500 });

                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Loose600Plus, ToItem = ShapeType.Fitted100 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Loose600Plus, ToItem = ShapeType.Fitted200 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Loose600Plus, ToItem = ShapeType.Fitted300 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Loose600Plus, ToItem = ShapeType.Fitted400 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Loose600Plus, ToItem = ShapeType.Fitted500 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Loose600Plus, ToItem = ShapeType.Loose100 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Loose600Plus, ToItem = ShapeType.Loose200 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Loose600Plus, ToItem = ShapeType.Loose300 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Loose600Plus, ToItem = ShapeType.Loose400 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Loose600Plus, ToItem = ShapeType.Loose500 });
                    break;
                case "Sporty":
                    maximumLayers = 3;
                    minimumLayers = 1;

                    sr.ColorBlending.ComplimentaryColor = true;
                    sr.ColorBlending.NeutralPrimaryColor = true;
                    sr.ColorBlending.NeutralSecondaryColor = true;
                    sr.ColorBlending.NeutralColor = true;

                    sr.AccessoriesAmount.Add(1);
                    sr.AccessoriesAmount.Add(3);
                    sr.Patterns.Add(new PatternRule { FromItem = PatternType.Solid, ToItem = PatternType.Solid, StyleRule = sr });
                    sr.Patterns.Add(new PatternRule { FromItem = PatternType.Solid, ToItem = PatternType.Minimal, StyleRule = sr });

                    sr.Structures.Add(new StructureRule { FromItem = StructureType.Structured, ToItem = StructureType.BodyConscious, StyleRule = sr });
                    sr.Structures.Add(new StructureRule { FromItem = StructureType.Relaxed, ToItem = StructureType.Relaxed, StyleRule = sr });
                    sr.Structures.Add(new StructureRule { FromItem = StructureType.Relaxed, ToItem = StructureType.BodyConscious, StyleRule = sr });

                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Slim100 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Slim200 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Slim300 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Slim400 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Slim500 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Fitted100 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Fitted200 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Fitted300 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Fitted400 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Fitted500 });

                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Slim100 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Slim200 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Slim300 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Slim400 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Slim500 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Fitted100 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Fitted200 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Fitted300 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Fitted400 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Fitted500 });

                    break;
                case "Preppy":
                    maximumLayers = 5;
                    minimumLayers = 2;

                    sr.ColorBlending.ComplimentaryColor = true;
                    sr.ColorBlending.NeutralPrimaryColor = true;
                    sr.ColorBlending.NeutralSecondaryColor = true;

                    sr.AccessoriesAmount.Add(1);
                    sr.AccessoriesAmount.Add(3);
                    sr.Patterns.Add(new PatternRule { FromItem = PatternType.Solid, ToItem = PatternType.Solid, StyleRule = sr });
                    sr.Patterns.Add(new PatternRule { FromItem = PatternType.Solid, ToItem = PatternType.Minimal, StyleRule = sr });

                    sr.Structures.Add(new StructureRule { FromItem = StructureType.Structured, ToItem = StructureType.Structured, StyleRule = sr });
                    sr.Structures.Add(new StructureRule { FromItem = StructureType.Structured, ToItem = StructureType.Relaxed, StyleRule = sr });
                    sr.Structures.Add(new StructureRule { FromItem = StructureType.Relaxed, ToItem = StructureType.Relaxed, StyleRule = sr });

                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Fitted100 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Fitted200 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Fitted300 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Fitted400 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Fitted500 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Loose100 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Loose200 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Loose300 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Loose400 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Loose500 });

                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Loose600Plus, ToItem = ShapeType.Fitted100 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Loose600Plus, ToItem = ShapeType.Fitted200 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Loose600Plus, ToItem = ShapeType.Fitted300 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Loose600Plus, ToItem = ShapeType.Fitted400 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Loose600Plus, ToItem = ShapeType.Fitted500 });
                    break;
                case "Glam":
                    maximumLayers = 3;
                    minimumLayers = 1;

                    sr.ColorBlending.MonotoneColor = true;
                    sr.ColorBlending.ComplimentaryColor = true;
                    sr.ColorBlending.NeutralPrimaryColor = true;
                    sr.ColorBlending.NeutralSecondaryColor = true;

                    sr.AccessoriesAmount.Add(5);
                    sr.Patterns.Add(new PatternRule { FromItem = PatternType.Solid, ToItem = PatternType.Solid, StyleRule = sr });
                    sr.Patterns.Add(new PatternRule { FromItem = PatternType.Solid, ToItem = PatternType.Minimal, StyleRule = sr });

                    sr.Structures.Add(new StructureRule { FromItem = StructureType.Structured, ToItem = StructureType.Structured, StyleRule = sr });
                    sr.Structures.Add(new StructureRule { FromItem = StructureType.Structured, ToItem = StructureType.BodyConscious, StyleRule = sr });
                    sr.Structures.Add(new StructureRule { FromItem = StructureType.Relaxed, ToItem = StructureType.BodyConscious, StyleRule = sr });
                    sr.Structures.Add(new StructureRule { FromItem = StructureType.BodyConscious, ToItem = StructureType.BodyConscious, StyleRule = sr });

                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Slim100 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Slim200 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Slim300 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Slim400 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Slim500 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Fitted100 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Fitted200 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Fitted300 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Fitted400 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Fitted500 });

                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Slim100 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Slim200 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Slim300 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Slim400 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Slim500 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Fitted100 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Fitted200 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Fitted300 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Fitted400 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Fitted500 });

                    break;
                case "Bohemian":
                    maximumLayers = 5;
                    minimumLayers = 3;

                    sr.ColorBlending.AnalogousColor = true;
                    sr.ColorBlending.ComplimentaryColor = true;
                    sr.ColorBlending.NeutralPrimaryColor = true;
                    sr.ColorBlending.NeutralSecondaryColor = true;

                    sr.AccessoriesAmount.Add(4);
                    sr.Patterns.Add(new PatternRule { FromItem = PatternType.Solid, ToItem = PatternType.Bold, StyleRule = sr });
                    sr.Patterns.Add(new PatternRule { FromItem = PatternType.Solid, ToItem = PatternType.Minimal, StyleRule = sr });
                    sr.Patterns.Add(new PatternRule { FromItem = PatternType.Minimal, ToItem = PatternType.Minimal, StyleRule = sr });
                    sr.Patterns.Add(new PatternRule { FromItem = PatternType.Minimal, ToItem = PatternType.Bold, StyleRule = sr });
                    sr.Patterns.Add(new PatternRule { FromItem = PatternType.Bold, ToItem = PatternType.Bold, StyleRule = sr });

                    sr.Structures.Add(new StructureRule { FromItem = StructureType.Structured, ToItem = StructureType.FlowingFluid, StyleRule = sr });
                    sr.Structures.Add(new StructureRule { FromItem = StructureType.FlowingFluid, ToItem = StructureType.FlowingFluid, StyleRule = sr });
                    sr.Structures.Add(new StructureRule { FromItem = StructureType.FlowingFluid, ToItem = StructureType.Relaxed, StyleRule = sr });

                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Fitted100 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Fitted200 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Fitted300 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Fitted400 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Fitted500 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Loose100 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Loose200 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Loose300 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Loose400 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Loose500 });

                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Fitted100 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Fitted200 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Fitted300 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Fitted400 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Fitted500 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Loose100 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Loose200 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Loose300 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Loose400 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Loose500 });

                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Loose600Plus, ToItem = ShapeType.Fitted100 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Loose600Plus, ToItem = ShapeType.Fitted200 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Loose600Plus, ToItem = ShapeType.Fitted300 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Loose600Plus, ToItem = ShapeType.Fitted400 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Loose600Plus, ToItem = ShapeType.Fitted500 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Loose600Plus, ToItem = ShapeType.Loose100 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Loose600Plus, ToItem = ShapeType.Loose200 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Loose600Plus, ToItem = ShapeType.Loose300 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Loose600Plus, ToItem = ShapeType.Loose400 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Loose600Plus, ToItem = ShapeType.Loose500 });
                    break;
                case "Trendy":
                    maximumLayers = 5;
                    minimumLayers = 1;

                    sr.ColorBlending.MonotoneColor = true;
                    sr.ColorBlending.AnalogousColor = true;
                    sr.ColorBlending.ComplimentaryColor = true;
                    sr.ColorBlending.NeutralPrimaryColor = true;
                    sr.ColorBlending.NeutralSecondaryColor = true;
                    sr.ColorBlending.NeutralColor = true;

                    sr.AccessoriesAmount.Add(4);
                    sr.Patterns.Add(new PatternRule { FromItem = PatternType.Solid, ToItem = PatternType.Solid, StyleRule = sr });
                    sr.Patterns.Add(new PatternRule { FromItem = PatternType.Solid, ToItem = PatternType.Minimal, StyleRule = sr });
                    sr.Patterns.Add(new PatternRule { FromItem = PatternType.Solid, ToItem = PatternType.Bold, StyleRule = sr });
                    sr.Patterns.Add(new PatternRule { FromItem = PatternType.Minimal, ToItem = PatternType.Bold, StyleRule = sr });

                    sr.Structures.Add(new StructureRule { FromItem = StructureType.Structured, ToItem = StructureType.Structured, StyleRule = sr });
                    sr.Structures.Add(new StructureRule { FromItem = StructureType.Structured, ToItem = StructureType.FlowingFluid, StyleRule = sr });
                    sr.Structures.Add(new StructureRule { FromItem = StructureType.Structured, ToItem = StructureType.Relaxed, StyleRule = sr });
                    sr.Structures.Add(new StructureRule { FromItem = StructureType.Structured, ToItem = StructureType.BodyConscious, StyleRule = sr });
                    sr.Structures.Add(new StructureRule { FromItem = StructureType.FlowingFluid, ToItem = StructureType.FlowingFluid, StyleRule = sr });
                    sr.Structures.Add(new StructureRule { FromItem = StructureType.FlowingFluid, ToItem = StructureType.Relaxed, StyleRule = sr });
                    sr.Structures.Add(new StructureRule { FromItem = StructureType.FlowingFluid, ToItem = StructureType.BodyConscious, StyleRule = sr });
                    sr.Structures.Add(new StructureRule { FromItem = StructureType.Relaxed, ToItem = StructureType.Relaxed, StyleRule = sr });
                    sr.Structures.Add(new StructureRule { FromItem = StructureType.Relaxed, ToItem = StructureType.BodyConscious, StyleRule = sr });
                    sr.Structures.Add(new StructureRule { FromItem = StructureType.BodyConscious, ToItem = StructureType.BodyConscious, StyleRule = sr });

                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Fitted100 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Fitted200 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Fitted300 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Fitted400 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Fitted500 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Loose100 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Loose200 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Loose300 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Loose400 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Slim600Plus, ToItem = ShapeType.Loose500 });

                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Slim100 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Fitted100 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Fitted200 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Fitted300 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Fitted600Plus, ToItem = ShapeType.Fitted400 });

                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Loose600Plus, ToItem = ShapeType.Slim100 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Loose600Plus, ToItem = ShapeType.Slim200 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Loose600Plus, ToItem = ShapeType.Slim300 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Loose600Plus, ToItem = ShapeType.Slim400 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Loose600Plus, ToItem = ShapeType.Slim500 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Loose600Plus, ToItem = ShapeType.Fitted100 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Loose600Plus, ToItem = ShapeType.Fitted200 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Loose600Plus, ToItem = ShapeType.Fitted300 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Loose600Plus, ToItem = ShapeType.Fitted400 });
                    sr.Shapes.Add(new ShapeRule { StyleRule = sr, FromItem = ShapeType.Loose600Plus, ToItem = ShapeType.Fitted500 });
                    break;
            }

            sr.MaximumLayers = maximumLayers;
            sr.MinimumLayers = minimumLayers;
            return sr;
        }

        #endregion

        [Test]
        public void CanCheckHasValidCombinations()
        {
            IClosetRepository closetRepository = new ClosetRepository();
            IStyleRuleRepository repStyleRule = new StyleRuleRepository();
            IFashionFlavorRepository fashionFlavorRepository = new FashionFlavorRepository();
            IOutfitUpdaterService ous = new OutfitUpdaterService(new OutfitUpdaterRepository(),
                new PreCombinationRepository(), repStyleRule);

            IOutfitEngineProcessor processor = new OutfitEngineProcessor(repStyleRule, closetRepository, ous);

            IGarmentRepository repGarments = new GarmentRepository();
            IOutfitEngineService oes = new OutfitEngineService(
                repGarments,
                closetRepository,
                processor, fashionFlavorRepository);

            List<int> lstgarments = new List<int>();
            lstgarments.Add(2383557);
            List<int> lstflavors = new List<int>();
            lstflavors.Add(1);
            lstflavors.Add(7);
            oes.HasValidCombinations(lstgarments, lstflavors);
            Assert.IsFalse(oes.HasValidCombinations(lstgarments, lstflavors));
        }


    }
}
