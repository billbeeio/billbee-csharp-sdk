using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billbee.Api.Client.Model
{
    public class OrderItemAttribute
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public decimal Price { get; set; }
    }
}
