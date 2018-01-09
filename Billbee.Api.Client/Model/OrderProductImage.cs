namespace Billbee.Api.Client.Model
{
    /// <summary>
    /// Information about a product image
    /// </summary>
    public class OrderProductImage
    {
        /// <summary>
        /// Url where this image is located
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Defines, wether this image should be used as default image, or not.
        /// </summary>
        public bool IsDefaultImage { get; set; }

        /// <summary>
        ///                    If more than one image was supplied, the position defines the order
        /// </summary>
        public byte? Position { get; set; }

        /// <summary>
        /// The if of the image in the external system, the corresponding order was received from
        /// </summary>
        public string ExternalId { get; set; }
    }
}
