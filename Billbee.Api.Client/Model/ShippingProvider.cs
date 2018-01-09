using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billbee.Api.Client.Model
{
    public class ShippingProvider
    {
        /// <summary>
        /// internal id of this provider
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Name of this provider
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Available products
        /// </summary>
        public List<ShippingProduct> products { get; set; }
    }



}
