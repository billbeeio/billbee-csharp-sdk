using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billbee.Api.Client.Model
{
    public class StockArticle
    {
        public string Name { get; set; }
        public int StockId { get; set; }
        public decimal? StockCurrent { get; set; }
        public decimal? StockWarning { get; set; }
        public string StockCode { get; set; }
        public decimal? UnfulfilledAmount { get; set; }
        public decimal? StockDesired { get; set; }
    }
   
}
