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

        /// <summary>
        /// The Tracking url
        /// </summary>
        public string TrackingUrl { get; set; }

        /// <summary>
        /// The id of the used shipping provider
        /// </summary>
        public long? ShippingProviderId { get; set; }

        /// <summary>
        /// The id of the used shipping provider product
        /// </summary>
        public long? ShippingProviderProductId { get; set; }

        /// <summary>
        /// The carrier used to ship the parcel with
        /// </summary>
        public byte? ShippingCarrier { get; set; }
    }
}
