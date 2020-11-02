namespace Billbee.Api.Client.Model
{
    public class UpdateStock
    {
        /// <summary>
        /// The SKU of the product to update
        /// </summary>
        public string Sku { get; set; }
        /// <summary>
        /// Optional the stock id if the feature multi stock is activated
        /// </summary>
        public long? StockId { get; set; }
        /// <summary>
        /// Optional a reason text for the stock update
        /// </summary>
        public string Reason { get; set; }
        /// <summary>
        /// This parameter is currently ignored
        /// </summary>
        public decimal? OldQuantity { get; set; }
        /// <summary>
        /// The new absolute stock quantity for the product you want to set
        /// </summary>
        public decimal? NewQuantity { get; set; }
        /// <summary>
        /// This parameter is currently ignored
        /// </summary>
        public decimal DeltaQuantity { get; set; }

        /// <summary>
        /// Automatically reduce the NewQuantity by the currently not fulfilled amount
        /// </summary>
        /// <remarks>
        /// If set to true, the NewQuantity is automatically reduced bei the reserved (not fulfilled) amount of the given article
        /// </remarks>
        public bool AutosubtractReservedAmount { get; set; }
    }
}
