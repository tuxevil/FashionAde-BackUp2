using FashionAde.Core.MVCInteraction;
using FashionAde.Core.ThirdParties;
using FashionAde.Core.Trends;
using FashionAde.Core.UserCloset;
using FluentNHibernate.Automapping;
using FashionAde.Core;
using SharpArch.Data.NHibernate.FluentNHibernate;
using FluentNHibernate.Automapping.Alterations;
using FashionAde.Core.Clothing;
using FashionAde.Core.ContentManagement;
using FashionAde.Core.OutfitCombination;
using FashionAde.Core.Accounts;
using FluentNHibernate;
using FashionAde.Core.OutfitEngine;
using FluentNHibernate.Conventions.Instances;
using FluentNHibernate.Mapping;

namespace FashionAde.Data.NHibernateMappings
{
    #region Accounts

    public class FriendMap : IAutoMappingOverride<Friend>
    {
        public void Override(AutoMapping<Friend> mapping)
        {
            mapping.Id(x => x.Id).GeneratedBy.HiLo("_uniquekey", "next_hi", "0", string.Format("table_name = '{0}'", "friends"));
            mapping.References<BasicUser>(x => x.User).Cascade.SaveUpdate();
        }
    }

    public class FriendRatingMap : IAutoMappingOverride<FriendRating>
    {
        public void Override(AutoMapping<FriendRating> mapping)
        {
            mapping.Id(x => x.Id).GeneratedBy.HiLo("_uniquekey", "next_hi", "0", string.Format("table_name = '{0}'", "friendratings"));
        }
    }

    public class InvitationCodeMap : IAutoMappingOverride<InvitationCode>
    {
        public void Override(AutoMapping<InvitationCode> mapping)
        {
            mapping.Map(x => x.Code).Not.Nullable();
            mapping.Map(x => x.EmailAddress).Nullable();
        }
    }

    public class InvitedUserMap : IAutoMappingOverride<InvitedUser>
    {
        public void Override(AutoMapping<InvitedUser> mapping)
        {
            mapping.Map(x => x.EmailAddressReplaced).Nullable();
        }
    }

    public class UserFlavorMap : IAutoMappingOverride<UserFlavor>
    {
        public void Override(AutoMapping<UserFlavor> mapping)
        {
            mapping.Id(x => x.Id).GeneratedBy.HiLo("_uniquekey", "next_hi", "0", string.Format("table_name = '{0}'", "userflavors"));            
        }
    }

    public class RegisteredUserMap : IAutoMappingOverride<RegisteredUser>
    {
        public void Override(AutoMapping<RegisteredUser> mapping)
        {
            mapping.Map(x => x.NewMail).Nullable();
        }
    }

    public class BasicUserMap : IAutoMappingOverride<BasicUser>
    {
        public void Override(AutoMapping<BasicUser> mapping)
        {
            mapping.Map(x => x.FirstName).Nullable();
            mapping.Map(x => x.LastName).Nullable();

            mapping.Id(x => x.Id).GeneratedBy.HiLo("_uniquekey", "next_hi", "0", string.Format("table_name = '{0}'", "basicusers"));

            mapping.HasManyToMany<EventType>(Reveal.Property<BasicUser>("eventTypes"))
                .Table("UserByEventTypes")
                .ParentKeyColumn("GarmentId")
                .ChildKeyColumn("EventTypeId")
                .Cascade.None();

            mapping.HasMany<Friend>(Reveal.Property<BasicUser>("friends")).Inverse().Cascade.AllDeleteOrphan();

            mapping.HasMany<Friend>(Reveal.Property<BasicUser>("friendsThatInvitedMe"))
                .KeyColumn("UserId")
                .Where(string.Format("Status = {0}", (int)FriendStatus.Pending));

            mapping.HasMany<Friend>(Reveal.Property<BasicUser>("friendsAccepted")).
                Where(string.Format("Status = {0}", (int)FriendStatus.Accepted));

            mapping.HasMany<UserFlavor>(Reveal.Property<BasicUser>("userFlavors"))
                .Cascade.SaveUpdate();
        }
    }

    #endregion

    #region ContentManagement

    public class ContentMap : IAutoMappingOverride<Content>
    {
        public void Override(AutoMapping<Content> mapping)
        {
            mapping.Id(x => x.Id).GeneratedBy.HiLo("_uniquekey", "next_hi", "0", string.Format("table_name = '{0}'", "contents"));

            mapping.Map(x => x.PromotionalText).Nullable();
            mapping.Map(x => x.Keywords).Nullable();
            mapping.Map(x => x.ScheduleFrom).Nullable();
            mapping.Map(x => x.ScheduleTo).Nullable();
            mapping.Map(x => x.EditedBy).Nullable();
            mapping.Map(x => x.ApprovedBy).Nullable();
            mapping.Map(x => x.AssignedTo).Nullable();
            mapping.Map(x => x.CreatedOn).Not.Update();
            mapping.Map(x => x.CreatedOn).Not.Insert();

            mapping.HasManyToMany<FashionFlavor>(x => x.Flavors)
                .Table("contentFlavors")
                .ParentKeyColumn("ContentId")
                .ChildKeyColumn("FlavorId");

            mapping.HasMany<ContentSection>(x => x.Sections).Inverse().Cascade.AllDeleteOrphan();
        }
    }

    public class ContentPublishedSectionMap : IAutoMappingOverride<ContentPublishedSection>
    {
        public void Override(AutoMapping<ContentPublishedSection> mapping)
        {
            mapping.Id(x => x.Id).GeneratedBy.HiLo("_uniquekey", "next_hi", "0", string.Format("table_name = '{0}'", "contentpublishedsections"));
            mapping.Map(x => x.Summary).Nullable();
        }
    }

    public class ContentSectionMap : IAutoMappingOverride<ContentSection>
    {
        public void Override(AutoMapping<ContentSection> mapping)
        {
            mapping.Id(x => x.Id).GeneratedBy.HiLo("_uniquekey", "next_hi", "0", string.Format("table_name = '{0}'", "contentsections"));
            mapping.Map(x => x.Summary).Nullable();
        }
    }

    public class ContentPublishedMap : IAutoMappingOverride<ContentPublished>
    {
        public void Override(AutoMapping<ContentPublished> mapping)
        {
            mapping.Id(x => x.Id).GeneratedBy.HiLo("_uniquekey", "next_hi", "0", string.Format("table_name = '{0}'", "contentpublisheds"));

            mapping.Map(x => x.PromotionalText).Nullable();
            mapping.Map(x => x.Keywords).Nullable();
            mapping.Map(x => x.ScheduleFrom).Nullable();
            mapping.Map(x => x.ScheduleTo).Nullable();
            mapping.Map(x => x.CreatedOn).Not.Update();
            mapping.Map(x => x.CreatedOn).Not.Insert();

            mapping.Map(x => x.EditedBy).Nullable();
            mapping.Map(x => x.ApprovedBy).Nullable();
            mapping.Map(x => x.AssignedTo).Nullable();

            mapping.HasManyToMany<FashionFlavor>(x => x.Flavors)
                .Table("contentPublishedFlavors")
                .ParentKeyColumn("ContentPublishedId")
                .ChildKeyColumn("FlavorId");

            mapping.HasMany<ContentPublishedSection>(x => x.Sections).Inverse().Cascade.AllDeleteOrphan();
        }
    }

    public class ContentCategoryMap : IAutoMappingOverride<ContentCategory>
    {
        public void Override(AutoMapping<ContentCategory> mapping)
        {
            mapping.Id(x => x.Id).GeneratedBy.HiLo("_uniquekey", "next_hi", "0", string.Format("table_name = '{0}'", "contentcategories"));

            mapping.HasManyToMany<ContentCategory>(x => x.Childs)
                .Table("contentcategorieschilds")
                .ParentKeyColumn("CategoryId")
                .ChildKeyColumn("ChildCategoryId");
        }
    }

    #endregion

    #region Garments

    public class GarmentDetailsMap : IAutoMappingOverride<GarmentDetails>
    {
        public void Override(AutoMapping<GarmentDetails> mapping)
        {
            mapping.Id(x => x.Id).GeneratedBy.HiLo("_uniquekey", "next_hi", "0", string.Format("table_name = '{0}'", "garmentdetails"));
            mapping.Map(x => x.PurchasedOn).Nullable();
        }
    }

    public class GarmentMap : IAutoMappingOverride<Garment>
    {
        public void Override(AutoMapping<Garment> mapping)
        {
            mapping.Id(x => x.Id).GeneratedBy.HiLo("_uniquekey", "next_hi", "0", string.Format("table_name = '{0}'", "garments"));
            mapping.DiscriminateSubClassesOnColumn("discriminator").CustomType<int>();
        }
    }

    public class SilouhetteMap : IAutoMappingOverride<Silouhette>
    {
        public void Override(AutoMapping<Silouhette> mapping)
        {
            mapping.HasManyToMany<Pattern>(x => x.AvailablePatterns)
                .Table("SilouhettesByPatterns")
                .ParentKeyColumn("SilouhetteId")
                .ChildKeyColumn("PatternId");

            mapping.HasManyToMany<Color>(x => x.AvailableColors)
                .Table("SilouhettesByColors")
                .ParentKeyColumn("SilouhetteId")
                .ChildKeyColumn("ColorId");

            mapping.HasManyToMany<FashionFlavor>(x => x.FashionFlavors)
                .Table("SilouhettesByFlavors")
                .ParentKeyColumn("SilouhetteId")
                .ChildKeyColumn("FlavorId");

            mapping.HasManyToMany<Fabric>(x => x.AvailableFabrics)
                .Table("SilouhettesByFabrics")
                .ParentKeyColumn("SilouhetteId")
                .ChildKeyColumn("FabricId");

            mapping.HasMany<Season>(x => x.Seasons)
                .Table("SilouhettesBySeasons")
                .KeyColumn("SilouhetteId")
                .Element("Season");

            mapping.HasManyToMany<EventType>(x => x.EventTypes)
                .Table("SilouhettesByEventTypes")
                .ParentKeyColumn("SilouhetteId")
                .ChildKeyColumn("EventTypeId");

            mapping.HasMany<LayerCode>(x => x.Layers)
                .Element("Layer");

            mapping.HasMany<Garment>(x => x.Garments)
                .Table("Garments")
                .KeyColumn("SilouhetteId");
                
        }
    }

    public class GarmentTagsMap : IAutoMappingOverride<GarmentTags>
    {
        public void Override(AutoMapping<GarmentTags> mapping)
        {
            mapping.HasMany<Season>(x => x.Seasons)
                .Table("GarmentsBySeasons")
                .Element("Season");

            mapping.HasManyToMany<EventType>(x => x.EventTypes)
                .Table("GarmentsByEventTypes")
                .ParentKeyColumn("GarmentId")
                .ChildKeyColumn("EventTypeId");

            mapping.HasManyToMany<Color>(x => x.Colors)
                .Table("GarmentsByColors")
                .ParentKeyColumn("GarmentId")
                .ChildKeyColumn("ColorId");
        }
    }

    public class WishGarmentMap : IAutoMappingOverride<WishGarment>
    {
        public void Override(AutoMapping<WishGarment> mapping)
        {
            mapping.Id(x => x.Id).GeneratedBy.HiLo("_uniquekey", "next_hi", "0", string.Format("table_name = '{0}'", "wishgarments"));
        }
    }

    public class WishListMap : IAutoMappingOverride<WishList>
    {
        public void Override(AutoMapping<WishList> mapping)
        {
            mapping.Id(x => x.Id).GeneratedBy.HiLo("_uniquekey", "next_hi", "0", string.Format("table_name = '{0}'", "wishlists"));

            mapping.HasMany<WishGarment>(x => x.Garments)
                .Cascade.SaveUpdate();
        }
    }


    #endregion

    #region Style Rules

    public class ColorMap : IAutoMappingOverride<Color>
    {
        public void Override(AutoMapping<Color> mapping)
        {
            mapping.HasManyToMany<Color>(x => x.ComplimentaryColors)
                .Table("ColorComplimentaries")
                .ParentKeyColumn("ColorId")
                .ChildKeyColumn("ComplimentaryColorId");
        }
    }

    public class StructureRuleMap : IAutoMappingOverride<StructureRule>
    {
        public void Override(AutoMapping<StructureRule> mapping)
        {
            mapping.Table("StyleRulesStructures");
        }
    }

    public class PatternRuleMap : IAutoMappingOverride<PatternRule>
    {
        public void Override(AutoMapping<PatternRule> mapping)
        {
            mapping.Table("StyleRulesPatterns");
        }
    }

    public class ShapeRuleMap : IAutoMappingOverride<ShapeRule>
    {
        public void Override(AutoMapping<ShapeRule> mapping)
        {
            mapping.Table("StyleRulesShapes");
        }
    }

    public class StyleRuleMap : IAutoMappingOverride<StyleRule>
    {
        public void Override(AutoMapping<StyleRule> mapping)
        {
            mapping.HasMany<int>(x => x.AccessoriesAmount)
                .Table("StyleRulesAccessories")
                .KeyColumn("StyleRuleId")
                .Element("Amount")
                .Cascade.SaveUpdate();

            mapping.HasMany<StructureRule>(x => x.Structures)
                .Cascade.SaveUpdate();

            mapping.HasMany<PatternRule>(x => x.Patterns)
                .Cascade.SaveUpdate();

            mapping.HasMany<ShapeRule>(x => x.Shapes)
                .Cascade.SaveUpdate();

        }
    }

    #endregion

    #region Rating

    public class RatingMap : IAutoMappingOverride<Rating>
    {
        public void Override(AutoMapping<Rating> mapping)
        {
            mapping.HasMany<FriendRating>(Reveal.Property<Rating>("friendRatings")).Cascade.SaveUpdate().KeyColumn("ClosetOutfitId");

            mapping.HasMany<UserRating>(Reveal.Property<Rating>("userRatings")).Cascade.SaveUpdate().KeyColumn("ClosetOutfitId");
        }

    }

    public class FriendRatingInvitationMap : IAutoMappingOverride<FriendRatingInvitation>
    {
        public void Override(AutoMapping<FriendRatingInvitation> mapping)
        {
            mapping.Id(x => x.Id).GeneratedBy.HiLo("_uniquekey", "next_hi", "0", string.Format("table_name = '{0}'", "friendratinginvitations"));

            mapping.IgnoreProperty(x => x.Outfit);
        }
    }


    #endregion

    #region Closet

    public class ClosetMap : IAutoMappingOverride<Closet>
    {
        public void Override(AutoMapping<Closet> mapping)
        {
            mapping.Id(x => x.Id).GeneratedBy.HiLo("_uniquekey", "next_hi", "0", string.Format("table_name = '{0}'", "closets"));

            mapping.HasMany<ClosetGarment>(Reveal.Property<Closet>("garments"))
                .Cascade.SaveUpdate();

            mapping.HasMany<ClosetOutfit>(Reveal.Property<Closet>("outfits"))
                .Table("ClosetOutfits")
                .KeyColumn("ClosetId")
                .Cascade.SaveUpdate();
        }
    }

    public class ClosetGarmentMap : IAutoMappingOverride<ClosetGarment>
    {
        public void Override(AutoMapping<ClosetGarment> mapping)
        {
            mapping.Id(x => x.Id).GeneratedBy.HiLo("_uniquekey", "next_hi", "10",  string.Format("table_name = '{0}'", "closetgarments"));

            mapping.References(x => x.Details)
                .Cascade.SaveUpdate();
        }
    }

    #endregion

    #region Outfits & Combinations

    public class OutfitDetailsMap : IAutoMappingOverride<OutfitDetails>
    {
        public void Override(AutoMapping<OutfitDetails> mapping)
        {
            mapping.Id(x => x.Id).GeneratedBy.HiLo("_uniquekey", "next_hi", "0", string.Format("table_name = '{0}'", "outfitdetails"));
        }
    }

    public class ClosetOutfitMap : IAutoMappingOverride<ClosetOutfit>
    {
        public void Override(AutoMapping<ClosetOutfit> mapping)
        {
            mapping.HasMany<OutfitDetails>(x => x.Details).Cascade.SaveUpdate();
            mapping.HasOne(x => x.Detail);
        }
    }

    public class TrendMap : IAutoMappingOverride<Trend>
    {
        public void Override(AutoMapping<Trend> mapping)
        {
            mapping.HasMany<string>(x => x.Keywords)
                .Table("trendkeywords")
                .KeyColumn("trendid")
                .Element("keyword")
                .Cascade.SaveUpdate();
        }
    }

    public class PreCombinationMap : IAutoMappingOverride<PreCombination>
    {
        public void Override(AutoMapping<PreCombination> mapping)
        {
            mapping.HasManyToMany<OutfitUpdater>(x => x.OutfitUpdaters)
                .Table("outfitupdaterbyprecombinations")
                .ParentKeyColumn("PreCombinationId")
                .ChildKeyColumn("OutfitUpdaterId")
                .Cascade.SaveUpdate();
        }
    }


    public class OutfitUpdaterMap : IAutoMappingOverride<OutfitUpdater>
    {
        public void Override(AutoMapping<OutfitUpdater> mapping)
        {
            mapping.Id(x => x.Id).GeneratedBy.HiLo("_uniquekey", "next_hi", "500", string.Format("table_name = '{0}'", "outfitupdaters"));

            mapping.HasManyToMany<Trend>(x => x.Trends)
                .Table("OutfitUpdatersByTrend")
                .ParentKeyColumn("OutfitUpdaterId")
                .ChildKeyColumn("TrendId")
                .Cascade.SaveUpdate();

            mapping.HasManyToMany<PreCombination>(x => x.PreCombinations)
                .Table("outfitupdaterbyprecombinations")
                .ParentKeyColumn("OutfitUpdaterId")
                .ChildKeyColumn("PreCombinationId")
                .Cascade.SaveUpdate();
        }
    }

    #endregion
}
