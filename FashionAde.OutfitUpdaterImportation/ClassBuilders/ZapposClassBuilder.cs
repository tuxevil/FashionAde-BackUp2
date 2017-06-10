using FashionAde.OutfitUpdaterImportation.Interfaces;
using FileHelpers.RunTime;

namespace FashionAde.OutfitUpdaterImportation.Core
{
    //REVIEW: No comments at all.
    public class ZapposClassBuilder: IOUImportationClassBuilder
    {
        // REVIEW: This class is not useful this way. It should be much more complex, reading the file
        // REVIEW: line at line and throwing an event with an aggred object like OUImportationLine.
        public DelimitedClassBuilder CreateClassBuilder(string separator, bool haveHeader)
        {
            DelimitedClassBuilder cb = new DelimitedClassBuilder("TmpOutfitUpdater", separator);
            if (haveHeader)
                cb.IgnoreFirstLines = 1;

            cb.AddField("ProgramName", typeof(string));
            cb.LastField.FieldOptional = true;
            cb.AddField("ProgramUrl", typeof(string));
            cb.LastField.FieldOptional = true;
            cb.AddField("CatalogName", typeof(string));
            cb.LastField.FieldOptional = true;
            cb.AddField("LastUpdated", typeof(string));
            cb.LastField.FieldOptional = true;
            cb.AddField("Name", typeof(string));
            cb.LastField.FieldOptional = true;
            cb.AddField("Keywords", typeof(string));
            cb.LastField.FieldOptional = true;
            cb.AddField("Description", typeof(string));
            cb.LastField.FieldOptional = true;
            cb.AddField("Sku", typeof(string));
            cb.LastField.FieldOptional = true;
            cb.AddField("Manufacturer", typeof(string));
            cb.LastField.FieldOptional = true;
            cb.AddField("ManufacturerId", typeof(string));
            cb.LastField.FieldOptional = true;
            cb.AddField("Upc", typeof(string));
            cb.LastField.FieldOptional = true;
            cb.AddField("Isbn", typeof(string));
            cb.LastField.FieldOptional = true;
            cb.AddField("Currency", typeof(string));
            cb.LastField.FieldOptional = true;
            cb.AddField("SalePrice", typeof(string));
            cb.LastField.FieldNullValue = null;
            cb.LastField.FieldOptional = true;
            cb.AddField("Price", typeof(string));
            cb.LastField.FieldNullValue = null;
            cb.LastField.FieldOptional = true;
            cb.AddField("RetailPrice", typeof(string));
            cb.LastField.FieldNullValue = null;
            cb.LastField.FieldOptional = true;
            cb.AddField("FromPrice", typeof(string));
            cb.LastField.FieldOptional = true;
            cb.AddField("BuyUrl", typeof(string));
            cb.LastField.FieldOptional = true;
            cb.AddField("ImpressionUrl", typeof(string));
            cb.LastField.FieldOptional = true;
            cb.AddField("ImageUrl", typeof(string));
            cb.LastField.FieldOptional = true;
            cb.AddField("AdvertiserCategory", typeof(string));
            cb.LastField.FieldOptional = true;
            cb.AddField("ThirdPartyId", typeof(string));
            cb.LastField.FieldOptional = true;
            cb.AddField("ThirdPartyCategory", typeof(string));
            cb.LastField.FieldOptional = true;
            cb.AddField("Author", typeof(string));
            cb.LastField.FieldOptional = true;
            cb.AddField("Artist", typeof(string));
            cb.LastField.FieldOptional = true;
            cb.AddField("Title", typeof(string));
            cb.LastField.FieldOptional = true;
            cb.AddField("Publisher", typeof(string));
            cb.LastField.FieldOptional = true;
            cb.AddField("Label", typeof(string));
            cb.LastField.FieldOptional = true;
            cb.AddField("Format", typeof(string));
            cb.LastField.FieldOptional = true;
            cb.AddField("Special", typeof(string));
            cb.LastField.FieldOptional = true;
            cb.AddField("Gift", typeof(string));
            cb.LastField.FieldOptional = true;
            cb.AddField("PromotionalText", typeof(string));
            cb.LastField.FieldOptional = true;
            cb.AddField("StartDate", typeof(string));
            cb.LastField.FieldOptional = true;
            cb.AddField("EndDate", typeof(string));
            cb.LastField.FieldOptional = true;
            cb.AddField("OffLine", typeof(string));
            cb.LastField.FieldOptional = true;
            cb.AddField("OnLine", typeof(string));
            cb.LastField.FieldOptional = true;
            cb.AddField("InStock", typeof(string));
            cb.LastField.FieldOptional = true;
            cb.AddField("Condition", typeof(string));
            cb.LastField.FieldOptional = true;
            cb.AddField("Warranty", typeof(string));
            cb.LastField.FieldOptional = true;
            cb.AddField("StandardShippingCost", typeof(string));
            cb.LastField.FieldOptional = true;

            cb.IgnoreEmptyLines = true;

            return cb;
        }
    }
}
