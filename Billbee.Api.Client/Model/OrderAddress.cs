namespace Billbee.Api.Client.Model
{
    /// <summary>
    /// Basic address for usage in orders
    /// </summary>
    public class OrderAddress
    {
        /// <summary>
        /// Internal id of this address
        /// </summary>
        public string BillbeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Company { get; set; }

        public string NameAddition { get; set; }

        public string Street { get; set; }

        public string HouseNumber { get; set; }

        public string Zip { get; set; }

        public string City { get; set; }

        /// <summary>
        /// The ISO 2 code of the country
        /// </summary>
        public string CountryISO2 { get; set; }

        /// <summary>
        /// Name of the country
        /// </summary>
        public string Country { get; set; }

        public string Line2 { get; set; }

        /// <summary>
        /// E-mail address of an addressee, used for notification purposes.
        /// </summary>
        public string Email { get; set; }

        public string State { get; set; }

        /// <summary>
        /// Phone number of an addressee, used for notification purposes.
        /// </summary>
        public string Phone { get; set; }
    }
}
