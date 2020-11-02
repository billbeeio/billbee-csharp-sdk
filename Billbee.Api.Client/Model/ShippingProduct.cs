namespace Billbee.Api.Client.Model
{
    public class ShippingProduct
    {
        /// <summary>
        /// Id of this shipping product
        /// </summary>
        public long id { get; set; }

        /// <summary>
        /// Human readable name of this shippingproduct
        /// </summary>
        public string displayName { get; set; }

        /// <summary>
        /// Provider specific product name
        /// </summary>
        public string productName { get; set; }
    }
}
