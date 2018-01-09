using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billbee.Api.Client.Model
{
    public class UpdateStock
    {
        public string Sku { get; set; }
        public int? StockId { get; set; }
        public string Reason { get; set; }
        public decimal? OldQuantity { get; set; }
        public decimal? NewQuantity { get; set; }
        public decimal DeltaQuantity { get; set; }
    }
}
