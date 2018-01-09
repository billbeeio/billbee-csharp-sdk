using System;

namespace Billbee.Api.Client.Model
{
    /// <summary>
    /// Shows shipping ids, that are attached to an order.
    /// </summary>
    public class OrderShippingId
    {
        /// <summary>
        /// Id if the shipment
        /// </summary>
        public string ShippingId { get; set; }

        /// <summary>
        /// The name of the shipping provider
        /// </summary>
        public string Shipper { get; set; }

        /// <summary>
        /// The date, this shipment was created
        /// </summary>
        public DateTime? Created { get; set; }
    }
}
