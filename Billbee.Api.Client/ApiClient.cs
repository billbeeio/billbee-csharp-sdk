using Billbee.Api.Client.EndPoint;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;

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
            Configuration = configuration ?? new ApiConfiguration();
            this.logger = logger;
        }

        public ApiClient(string ConfigurationPath, ILogger logger = null)
        {
            this.logger = logger;
            LoadConfigFromFile(ConfigurationPath);
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

            Configuration = JsonConvert.DeserializeObject<ApiConfiguration>(configStr);

        }

        /// <summary>
        /// EndPoint to access events
        /// </summary>
        public EventEndPoint Events => new EventEndPoint(Configuration, logger);

        /// <summary>
        /// EndPoint to access order independent shipments
        /// </summary>
        public ShipmentEndPoint Shipment => new ShipmentEndPoint(Configuration, logger);

        public WebhookEndPoint Webhooks => new WebhookEndPoint(Configuration, logger);

        /// <summary>
        /// EndPoint to access Products
        /// </summary>
        public ProductEndPoint Products => new ProductEndPoint(Configuration, logger);

        /// <summary>
        /// EndPoint to allow automatic user creation
        /// </summary>
        public AutomaticProvisionEndPoint AutomaticProvision => new AutomaticProvisionEndPoint(Configuration, logger);

        /// <summary>
        /// EndPoint to access customer base data
        /// </summary>
        public CustomerEndPoint Customer => new CustomerEndPoint(Configuration, logger);

        /// <summary>
        /// EndPoint for searches in customers, orders and products
        /// </summary>
        public SearchEndPoint Search => new SearchEndPoint(Configuration, logger);

        /// <summary>
        /// EndPoint to access orders
        /// </summary>
        public OrderEndPoint Orders => new OrderEndPoint(Configuration, logger);

        /// <summary>
        /// EndPoint to access cloud storages
        /// </summary>
        public CloudStoragesEndPoint CloudStorages => new CloudStoragesEndPoint(Configuration, logger);

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
            catch (Exception) // If user selectes to throw exceptions on server side errors, or other errors occurs.
            {
                return false;
            }
        }

        #endregion

        #region internal+private methods/ properties

        private ILogger logger { get; set; }

        #endregion
    }
}
