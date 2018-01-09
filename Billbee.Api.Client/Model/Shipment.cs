using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billbee.Api.Client.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using Billbee.Api.Client.Enums;

    namespace Rechnungsdruck.WebApp.Model.Api
    {
        /// <summary>
        /// Conainer, to create a shipment without context to a billbee order.
        /// </summary>
        public class PostShipment
        {
            /// <summary>
            ///  Name of the shipping provider, to use
            /// </summary>
            public string ProviderName { get; set; }
            /// <summary>
            /// Product code of the shipping provider for the product, to use as shipment method
            /// </summary>
            public string ProductCode { get; set; }
            /// <summary>
            /// Name of the cloud printer, to send the labels to
            /// </summary>
            public string PrinterName { get; set; }
            /// <summary>
            /// List of services, to attach to the shipping product
            /// </summary>
            public List<object> Services { get; set; }
            /// <summary>
            /// Address of the adressee
            /// </summary>
            public ShipmentAddress ReceiverAddress { get; set; }
            /// <summary>
            /// Reference number for this parcel
            /// </summary>
            public string ClientReference { get; set; }
            /// <summary>
            /// Number of the customer, this parcel should be send to.
            /// </summary>
            public string CustomerNumber { get; set; }
            /// <summary>
            /// gross weight of the parcel
            /// </summary>
            public decimal? WeightInGram { get; set; }
            /// <summary>
            /// Total gross value of the parcel
            /// </summary>
            public decimal OrderSum { get; set; }
            /// <summary>
            /// Currency code, of the currency, the order was made in. <<see cref="OrderSum"/>
            /// </summary>
            public string OrderCurrencyCode { get; set; }
            /// <summary>
            /// For export parcels, the content has to be defined
            /// </summary>
            public string Content { get; set; }
            /// <summary>
            /// Date and time of shipment
            /// </summary>
            public DateTime? ShipDate { get; set; }
            /// <summary>
            /// The carrier, the parcel will be send with.
            /// </summary>
            public ShippingCarrier shippingCarrier { get; set; }
        }
    }
}
