using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billbee.Api.Client.Model
{
    public class ProductSearchResult
    {
        public long Id { get; set; }
        public string ShortText { get; set; }
        public string SKU { get; set; }
        public string Tags { get; set; }
    }
}
