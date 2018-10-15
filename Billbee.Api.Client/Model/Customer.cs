using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billbee.Api.Client.Model
{
    public class Customer
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Tel1 { get; set; }
        public string Tel2 { get; set; }
        public int? Number { get; set; }
        public int? PriceGroupId { get; set; }
        public int? LanguageId { get; set; }
        public string VatId { get; set; }
    }
}
