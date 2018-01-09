using Billbee.Api.Client.Endpoint;
using BillBee.API.Client.EndPoint;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Specialized;

namespace BillBee.API.Client
{
    /// <summary>
    /// Client for the Billbee API
    /// see https://app01.billbee.de/swagger/ui/index or https://www.billbee.de/api/ for further information
    /// </summary>
    public class ApiClient
    {
        #region external methods/ properties
        /// <summary>
        /// Configuration, used to connect to the API <see cref="ApiConfiguration"/>.
        /// </summary>
        public ApiConfiguration Configuration { get; private set; }


        /// <summary>
        /// Creates a new instance of the Billbee API client <see cref="ApiClient"/>.
        /// </summary>
        /// <param name="configuration">Gives a individual configuration. If empty, a default configuration will be created.</param>
        public ApiClient(ApiConfiguration configuration = null, ILogger logger = null)
        {
            this.Configuration = configuration ?? new ApiConfiguration();
            this.logger = logger;
        }

        /// <summary>
        /// Endpoint to access events
        /// </summary>
        public EventEndPoint Events
        {
            get
            {
                return new EventEndPoint(Configuration, logger);
            }
        }

        /// <summary>
        /// Endpoint to access order independent shipments
        /// </summary>
        public ShipmentEndPoint Shipment
        {
            get
            {
                return new ShipmentEndPoint(Configuration, logger);
            }
        }

        /// <summary>
        /// Endpoint to access Products
        /// </summary>
        public ProductEndPoint Products
        {
            get
            {
                return new ProductEndPoint(Configuration, logger);
            }
        }

        /// <summary>
        /// Endpoint to allow automatic user creation
        /// </summary>
        public AutomaticProvisionEndPoint AutomaticProvision
        {
            get
            {
                return new AutomaticProvisionEndPoint(Configuration, logger);
            }
        }

        /// <summary>
        /// Endpoint to access orders
        /// </summary>
        public OrderEndPoint Orders
        {
            get
            {
                return new OrderEndPoint(Configuration, logger);
            }
        }

        /// <summary>
        /// Validates, that access to the api is possible with the given configuration
        /// </summary>
        /// <returns></returns>
        public bool TestConfiguration()
        {
            try
            {
                return this.Shipment.Ping();
            }
                catch(Exception ex) // If user selectes to throw exceptions on server side errors, or other errors occurs.
            {
                return false;
            }

        }

        #endregion

        #region internal+private methods/ properties
        ILogger logger { get; set; }
        #endregion
    }
}
