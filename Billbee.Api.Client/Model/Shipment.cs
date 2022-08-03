using System;
using Billbee.Api.Client.Enums;

namespace Billbee.Api.Client.Model
{
    /// <summary>
    /// Represents a single shipment.
    /// </summary>
    public class Shipment
    {
        /// <summary>
        /// The billbee internal id of the shipment
        /// </summary>
        public long BillbeeId { get; set; }

        /// <summary>The id of this shipment</summary>
        public string ShippingId { get; set; }

        /// <summary>The name of the shipping provider</summary>
        public string Shipper { get; set; }

        /// <summary>The creation date</summary>
        public DateTime? Created { get; set; }

        /// <summary>
        /// The url to track this shipment
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
        public ShippingCarrierEnum? ShippingCarrier { get; set; }
        
        /// <summary>
        /// Shipment Type, 0 if Shipment, 1 if Retoure
        /// </summary>
        public ShipmentTypeEnum? ShipmentType { get; set; }
    }
}