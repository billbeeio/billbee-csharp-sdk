using System.Collections.Generic;

namespace Billbee.Api.Client.Model
{
    public class ShippingProvider
    {
        /// <summary>
        /// internal id of this provider
        /// </summary>
        public long id { get; set; }

        /// <summary>
        /// Name of this provider
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Available products
        /// </summary>
        public List<ShippingProduct> products { get; set; }
    }
}
