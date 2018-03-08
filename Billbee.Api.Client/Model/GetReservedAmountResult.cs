using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billbee.Api.Client.Model
{
    public class GetReservedAmountResult
    {
        /// <summary>
        /// The reserve (not fulfilled) qty of the article
        /// </summary>
        public decimal ReservedAmount { get; set; }
    }
}
