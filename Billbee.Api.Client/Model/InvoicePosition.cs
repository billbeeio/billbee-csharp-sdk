namespace Billbee.Api.Client.Model
{
    /// <summary>
    /// Detail information about one position in an invoice
    /// </summary>
    public class InvoicePosition
    {
        /// <summary>
        /// Rank of this position in the order
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// The amount, of the given article in this position
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// The net value of the article
        /// </summary>
        public decimal NetValue { get; set; }

        /// <summary>
        /// The net value of all articles of this position
        /// </summary>
        public decimal TotalNetValue { get; set; }

        /// <summary>
        /// The gross value of this article
        /// </summary>
        public decimal GrossValue { get; set; }

        /// <summary>
        /// The gross value of all articles in this position
        /// </summary>
        public decimal TotalGrossValue { get; set; }

        /// <summary>
        /// The vat rate, that is used for this position
        /// </summary>
        public decimal? VatRate { get; set; }

        /// <summary>
        /// Internal id of the referenced article
        /// </summary>
        public int? ArticleBillbeeId { get; set; }

        /// <summary>
        /// The sku of the article of this position
        /// </summary>
        public string Sku { get; set; }

        /// <summary>
        /// The title to be shown in the invoice of this position
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The unique internal id of this position
        /// </summary>
        public int BillbeeId { get; set; }
    }
}
