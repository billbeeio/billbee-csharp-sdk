using Newtonsoft.Json.Linq;

namespace Billbee.Api.Client.Model
{
    /// <summary>
    /// Defines the sources in external systems, to which this article is attached to
    /// </summary>
    public class ArticleSource
    {
        /// <summary>
        /// Name of the source
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// The id on the source
        /// </summary>
        public string SourceId { get; set; }

        /// <summary>
        /// The internal id of this article source definition
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the api account, this source belongs to
        /// </summary>
        public string ApiAccountName { get; set; }

        /// <summary>
        /// Id of the api account, this source belongs to 
        /// </summary>
        public int? ApiAccountId { get; set; }

        public decimal? ExportFactor { get; set; }

        public bool? StockSyncInactive { get; set; }
        public decimal? StockSyncMin { get; set; }
        public decimal? StockSyncMax { get; set; }
        public decimal? UnitsPerItem { get; set; }

        public JObject Custom { get; set; }
    }
}
