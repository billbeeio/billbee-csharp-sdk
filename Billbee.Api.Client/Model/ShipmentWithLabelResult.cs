using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billbee.Api.Client.Model
{
    public class ShipmentWithLabelResult
    {
        /// <summary>
        /// Id of the order, that was shipped
        /// </summary>
        public int OrderId { get; set; }
        /// <summary>
        /// Reference number of the carrier or provider
        /// </summary>
        public string OrderReference { get; set; }
        /// <summary>
        /// Order or tracking number of the carrier or provider
        /// </summary>
        public string ShippingId { get; set; }
        /// <summary>
        /// URL to track the shipment
        /// </summary>
        public string TrackingUrl { get; set; }
        /// <summary>
        /// If applicable, the pdf data of the label as base 64 encoded data.
        /// </summary>
        public byte[] LabelDataPdf { get; set; }
        /// <summary>
        /// If applicable, the pdf data of the export documents as base 64 encoded data.
        /// </summary>
        public byte[] ExportDocsPdf { get; set; }

        /// <summary>
        /// Text representation of the used carrier
        /// </summary>
        public string Carrier { get; set; }

        /// <summary>
        /// The datetime, defining, when the order should be send
        /// </summary>
        public DateTime ShippingDate { get; set; }
    }
}
