using System.Collections.Generic;

namespace Billbee.Api.Client.Model
{
    public class OrderItemProduct
    {
        /// <summary>
        /// This is for migration scenarios when the internal id of a product changes
        /// I.E. Etsy when switching to the new inventory management, the ids for variants will change.
        /// </summary>
        public string OldId { get; set; }

        public string Id { get; set; }
        public string Title { get; set; }

        /// <summary>
        /// Weight of one item in gram
        /// </summary>
        public int? Weight { get; set; }

        public string SKU { get; set; }

        public string SkuOrId
        {
            get { return string.IsNullOrEmpty(SKU) ? Id : SKU; }
        }

        public bool? IsDigital { get; set; }

        public List<OrderProductImage> Images { get; set; }

        public string EAN { get; set; }

        /// <summary>
        /// Optional platform specific Data as serialized JSON Object for the product
        /// </summary>
        public string PlatformData { get; set; }

        public string TARICCode { get; set; }
        public string CountryOfOrigin { get; set; }
        public int? BillbeeId { get; set; }
    }
}
