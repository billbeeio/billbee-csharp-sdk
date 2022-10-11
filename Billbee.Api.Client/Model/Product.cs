using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Billbee.Api.Client.Model
{
    /// <summary>
    /// Defines a product/ article in billbee
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Text to display in the invoice
        /// </summary>
        public List<MultiLanguageString> InvoiceText { get; set; }

        /// <summary>
        /// Name of the manufacturer, if given
        /// </summary>
        public string Manufacturer { get; set; }

        /// <summary>
        /// Internal if of this product
        /// </summary>
        public long? Id { get; set; }

        /// <summary>
        /// Name of the product
        /// </summary>
        public List<MultiLanguageString> Title { get; set; }

        /// <summary>
        /// Description of the product
        /// </summary>
        public List<MultiLanguageString> Description { get; set; }

        /// <summary>
        /// Short description of the product, to show in summaries
        /// </summary>
        public List<MultiLanguageString> ShortDescription { get; set; }

        /// <summary>
        /// Basic attributes, to define this product
        /// </summary>
        public List<MultiLanguageString> BasicAttributes { get; set; }

        /// <summary>
        /// Images of this product
        /// </summary>
        public List<ArticleImage> Images { get; set; }

        /// <summary>
        /// Index of the correct vat rate, to attach to this product.
        /// </summary>
        public byte VatIndex { get; set; }

        /// <summary>
        /// Gross price of this product
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// If applicable, the price, to pay, when buying
        /// </summary>
        public decimal? CostPrice { get; set; }

        public decimal Vat1Rate { get; set; }
        public decimal Vat2Rate { get; set; }

        /// <summary>
        /// Defines, how big the amount of stock of this article should be
        /// </summary>
        public decimal? StockDesired { get; set; }

        /// <summary>
        /// Show, how big the current stock of this article is.
        /// </summary>
        public decimal? StockCurrent { get; set; }

        /// <summary>
        /// Defines, when to alert, because of low stock
        /// </summary>
        public decimal? StockWarning { get; set; }

        public string SKU { get; set; }
        public string EAN { get; set; }

        /// <summary>
        /// A list of materials, this products consists of
        /// </summary>
        public List<MultiLanguageString> Materials { get; set; }

        /// <summary>
        /// List if tags, that are marking this product
        /// </summary>
        public List<MultiLanguageString> Tags { get; set; }

        /// <summary>
        /// Sources, this article is attached to
        /// </summary>
        public List<ArticleSource> Sources { get; set; }

        /// <summary>
        /// The gross weight of this article
        /// </summary>
        public int? Weight { get; set; }

        /// <summary>
        /// The new weight of this article
        /// </summary>
        public int? WeightNet { get; set; }
        /// <summary>
        /// The stock code, where this article is stored
        /// </summary>
        public string StockCode { get; set; }

        public decimal? StockReduceItemsPerSale { get; set; }
        public List<StockArticle> Stocks { get; set; }
        public ArticleCategory Category1 { get; set; }
        public ArticleCategory Category2 { get; set; }
        public ArticleCategory Category3 { get; set; }
        public byte Type { get; set; }
        public short? Unit { get; set; }
        public decimal? UnitsPerItem { get; set; }
        public decimal? SoldAmount { get; set; }
        public decimal? SoldSumGross { get; set; }
        public decimal? SoldSumNet { get; set; }
        public decimal? SoldSumNetLast30Days { get; set; }
        public decimal? SoldSumGrossLast30Days { get; set; }
        public decimal? SoldAmountLast30Days { get; set; }

        /// <summary>
        /// If this article is bundled with a specific shipping product
        /// </summary>
        public long? ShippingProductId { get; set; }

        public bool IsDigital { get; set; }
        public bool IsCustomizable { get; set; }
        public byte? DeliveryTime { get; set; }
        public byte? Recipient { get; set; }
        public byte? Occasion { get; set; }
        public string CountryOfOrigin { get; set; }
        
        [Obsolete("Use ExportDescriptionMultiLanguage instead.")]
        public string ExportDescription { get; set; }
        public List<MultiLanguageString> ExportDescriptionMultiLanguage { get; set; } = new List<MultiLanguageString>();
        
        public string TaricNumber { get; set; }

        public List<AtticleCustomFieldValue> CustomFields { get; set; } = new List<AtticleCustomFieldValue>();
        
        public bool? IsDeactivated { get; set; }
        
        public List<BomSubArticleApiModel> BillOfMaterial { get; set; }
        public byte? Condition { get; set; }
        public decimal? WidthCm { get; set; }
        public decimal? LengthCm { get; set; }
        public decimal? HeightCm { get; set; }

        [JsonProperty("LowStock")]
        public bool IsLowStock { get; set; }
    }

    public class BomSubArticleApiModel
    {
        public decimal Amount { get; set; }
        public long? ArticleId { get; set; }
        public string SKU { get; set; }
    }
}
