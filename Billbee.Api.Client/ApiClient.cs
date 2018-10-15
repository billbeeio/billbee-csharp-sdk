using System;
using System.IO;
using System.Reflection;
using Billbee.Api.Client.EndPoint;
using Newtonsoft.Json;

namespace Billbee.Api.Client
{
    /// <summary>
    /// Client for the Billbee API
    /// see https://app.billbee.io/swagger/ui/index or https://www.billbee.de/api/ for further information
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

        public ApiClient(string ConfigurationPath, ILogger logger = null)
        {
            this.logger = logger;
            this.LoadConfigFromFile(ConfigurationPath);
        }

        private void LoadConfigFromFile(string path = null)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                path = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            }

            if (!path.ToLower().EndsWith(".json"))
            {
                path += ".json";
            }

            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"The config file {path} could not be found.");
            }
            
            string configStr = System.IO.File.ReadAllText(path);

            this.Configuration = JsonConvert.DeserializeObject<ApiConfiguration>(configStr);

        }

        /// <summary>
        /// EndPoint to access events
        /// </summary>
        public EventEndPoint Events
        {
            get { return new EventEndPoint(Configuration, logger); }
        }

        /// <summary>
        /// EndPoint to access order independent shipments
        /// </summary>
        public ShipmentEndPoint Shipment
        {
            get { return new ShipmentEndPoint(Configuration, logger); }
        }

        public WebhookEndPoint Webhooks
        {
            get { return new WebhookEndPoint(Configuration, logger); }
        }

        /// <summary>
        /// EndPoint to access Products
        /// </summary>
        public ProductEndPoint Products
        {
            get { return new ProductEndPoint(Configuration, logger); }
        }

        /// <summary>
        /// EndPoint to allow automatic user creation
        /// </summary>
        public AutomaticProvisionEndPoint AutomaticProvision
        {
            get { return new AutomaticProvisionEndPoint(Configuration, logger); }
        }

        /// <summary>
        /// EndPoint to access orders
        /// </summary>
        public OrderEndPoint Orders
        {
            get { return new OrderEndPoint(Configuration, logger); }
        }

        /// <summary>
        /// Validates, that access to the api is possible with the given configuration
        /// </summary>
        /// <returns></returns>
        public bool TestConfiguration()
        {
            try
            {
                return Shipment.Ping();
            }
            catch (Exception ex) // If user selectes to throw exceptions on server side errors, or other errors occurs.
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
