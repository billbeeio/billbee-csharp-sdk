using Billbee.Api.Client.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billbee.Api.Client.Model
{
    /// <summary>
    /// Accountinformation for creation of a new account
    /// </summary>
    public class Account
    {
        public override string ToString()
        {
            return $"EMail {EMail} Name {Address?.Name} Country {Address?.Country} Terms {AcceptTerms}";
        }

        /// <summary>
        /// The Email address of the user to create
        /// </summary>
        public string EMail { get; set; }
        public string Password { get; set; }

        /// <summary>
        /// Set to true, if the user has accepted the Billbee terms & conditions
        /// </summary>
        public bool AcceptTerms { get; set; }

        /// <summary>
        /// Represents the invoice address of a Billbee user
        /// </summary>
        public class UserAddress
        {
            public string Company { get; set; }
            public string Name { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string Zip { get; set; }
            public string City { get; set; }
            /// <summary>
            /// The ISO2 country code of the users country
            /// </summary>
            public string Country { get; set; }
            public string VatId { get; set; }

        }
        /// <summary>
        /// Gets or sets the invoice address of the Billbee user
        /// </summary>
        public UserAddress Address { get; set; }
        /// <summary>
        /// Gets or sets if the user is interested in the Billbee newsletter
        /// </summary>
        public bool Newsletter { get; set; }
        /// <summary>
        /// Specifies an billbee affiliate code to attach to the user
        /// </summary>
        public string AffiliateCouponCode { get; set; }

        /// <summary>
        /// Optionally specify the vat1 (normal) rate of the user
        /// </summary>
        public decimal? Vat1Rate { get; set; }
        /// <summary>
        /// Optionally specify the vat2 (reduced) rate of the user
        /// </summary>
        public decimal? Vat2Rate { get; set; }
        /// <summary>
        /// Optionally specify the default vat mode of the user
        /// </summary>
        /// <remarks>0: Show vat, 1: Kleinunternehmer</remarks>
        public VatModeEnum? DefaultVatMode { get; set; }
        /// <summary>
        /// Optionally specify the default currency of the user
        /// </summary>
        public string DefaultCurrrency { get; set; }
        /// <summary>
        /// Optionally specify the default vat index of the user
        /// </summary>
        /// <remarks>1: normal vat, 2: reduced vat</remarks>
        public byte? DefaultVatIndex { get; set; }
    }
}
