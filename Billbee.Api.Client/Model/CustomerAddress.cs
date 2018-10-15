using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billbee.Api.Client.Model
{
    public class CustomerAddress
    {

        public int? Id { get; set; }
        /// <summary>
        /// 1 = Invoiceaddress
        /// 2 = Shippingaddress
        /// </summary>
        public int AddressType { get; set; }
        public int? CustomerId { get; set; }
        public string Company { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name2 { get; set; }
        public string Street { get; set; }
        public string Housenumber { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string CountryCode { get; set; }
        public string Email { get; set; }
        public string Tel1 { get; set; }
        public string Tel2 { get; set; }
        public string Fax { get; set; }
        public string FullAddr { get; }
        public string AddressAddition { get; set; }
    }
}
