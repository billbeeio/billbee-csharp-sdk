using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billbee.Api.Client.Model
{
    public class OrderSearchResult
    {
        public long Id { get; set; }
        public string ExternalReference { get; set; }
        public string BuyerName { get; set; }
        public string InvoiceNumber { get; set; }
        public string CustomerName { get; set; }
        public string ArticleTexts { get; set; }
    }
}
