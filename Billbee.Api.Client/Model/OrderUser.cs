using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billbee.Api.Client.Model
{
    public class OrderUser
    {
        public string Platform { get; set; }
        public string BillbeeShopName { get; set; }
        public int? BillbeeShopId { get; set; }
        public string Id { get; set; }
        public string Nick { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }
        }

        public string Email { get; set; }
    }
}
