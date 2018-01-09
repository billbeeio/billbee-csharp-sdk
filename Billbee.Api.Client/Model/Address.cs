namespace Billbee.Api.Client.Model
{
    /// <summary>
    /// Basic address for usage in orders
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Internal id of this address
        /// </summary>
        public string Id { get; set; }

        public string City { get; set; }
        public string Street { get; set; }
        public string Company { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        public string Zip { get; set; }
        public string State { get; set; }

        /// <summary>
        /// Name of the country
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// The ISO 2 code of the country
        /// </summary>
        public string CountryISO2 { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        /// <summary>
        /// Phone number of an addressee, used for notification purposes.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// E-mail address of an addressee, used for notification purposes.
        /// </summary>
        public string Email { get; set; }

        public string HouseNumber { get; set; }

        /// <summary>
        /// A comment about the address for better differentiation
        /// </summary>
        public string Comment { get; set; }

        public string NameAddition { get; set; }
    }
}
