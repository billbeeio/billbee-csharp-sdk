namespace Billbee.Api.Client.Model
{
    public class ShipmentResult
    {
        /// <summary>
        /// Id of the created shipment
        /// </summary>
        public string ShippingId { get; set; }

        /// <summary>
        /// Byte array containing the label of the created shipment in PDF format
        /// </summary>
        public byte[] LabelData { get; set; }
    }
}
