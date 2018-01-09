using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billbee.Api.Client.Model
{
    public class ShipmentAddress
    {
        public string Company { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name2 { get; set; }
        public string Street { get; set; }
        public string Housenumber { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        /// <summary>
        /// The ISO 2 code of the country
        /// </summary>
        public string CountryCode { get; set; }
        /// <summary>
        /// The ISO 3 code of the country
        /// </summary>
        public string CountryCodeISO3 { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string AddressAddition { get; set; }
        public bool IsExportCountry { get; set; }
        public string State { get; set; }
    }
}
