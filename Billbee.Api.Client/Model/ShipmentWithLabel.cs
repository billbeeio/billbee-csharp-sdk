using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billbee.Api.Client.Model
{
    /// <summary>
    /// Request object, to ship an order in billbee
    /// </summary>
    public class ShipmentWithLabel
    {
        /// <summary>
        /// Billbee internal id of the order, that should be shipped.
        /// </summary>
        public int OrderId { get; set; }
        /// <summary>
        /// Billbee internal id of the provider, that should be used for shipping
        /// </summary>
        public int ProviderId { get; set; }
        /// <summary>
        /// Billbee internal id of the product, that should be used for shipping
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// Optional. If set to true, the state of the given order is set to send, after creation of label. Default value is true.
        /// </summary>
        public bool? ChangeStateToSend { get; set; }
        /// <summary>
        /// Optional. Name of a cloud drive or printer, that should be used to automatically store the label after creation.
        /// </summary>
        public string PrinterName { get; set; }
        /// <summary>
        /// Optional. Total weight of the parcel in gram
        /// </summary>
        public int? WeightInGram { get; set; }
        /// <summary>
        /// Optional. Date when the parcel should be send.
        /// </summary>
        public DateTime? ShipDate { get; set; }
        /// <summary>
        /// Optional. If given, this reference will be added to the parcel process as individual reference.
        /// </summary>
        public string ClientReference { get; set; }
        /// <summary>
        /// Optional. The dimension of the package.
        /// </summary>
        public ShipmentDimensions Dimension { get; set; }
    }
}
