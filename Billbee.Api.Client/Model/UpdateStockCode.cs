using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billbee.Api.Client.Model
{
    public class UpdateStockCode
    {
        public string Sku { get; set; }
        public int? StockId { get; set; }
        public string StockCode { get; set; }
    }
}
