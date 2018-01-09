namespace Billbee.Api.Client.Model
{
    /// <summary>
    /// Information abaout the current stock of an article
    /// </summary>
    public class CurrentStockInfo
    {
        /// <summary>
        /// SKU of the article
        /// </summary>
        public string SKU { get; set; }

        /// <summary>
        /// Amount, that is currently available from the stock.
        /// </summary>
        public decimal? CurrentStock { get; set; }
    }
}
