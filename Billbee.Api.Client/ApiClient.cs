using Billbee.Api.Client.EndPoint;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;
using Billbee.Api.Client.Interfaces;
using Billbee.Api.Client.Interfaces.Endpoint;

namespace Billbee.Api.Client
{
    /// <inheritdoc />
    public class ApiClient : IApiClient
    {
        #region external methods/ properties

        /// <inheritdoc />
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


        /// <inheritdoc />
        public IEventEndPoint Events => new EventEndPoint(Configuration, logger);

        private ShipmentEndPoint ShipmentEndPoint => new ShipmentEndPoint(Configuration, logger);

        /// <inheritdoc />
        public IShipmentEndPoint Shipment => ShipmentEndPoint;

        /// <inheritdoc />
        public IWebhookEndPoint Webhooks => new WebhookEndPoint(Configuration, logger);

        /// <inheritdoc />
        public IProductEndPoint Products => new ProductEndPoint(Configuration, logger);

        /// <inheritdoc />
        public IAutomaticProvisionEndPoint AutomaticProvision => new AutomaticProvisionEndPoint(Configuration, logger);

        /// <inheritdoc />
        public ICustomerEndPoint Customer => new CustomerEndPoint(Configuration, logger);

        /// <inheritdoc />
        public ISearchEndPoint Search => new SearchEndPoint(Configuration, logger);

        /// <inheritdoc />
        public IOrderEndPoint Orders => new OrderEndPoint(Configuration, logger);

        /// <inheritdoc />
        public CloudStoragesEndPoint CloudStorages => new CloudStoragesEndPoint(Configuration, logger);

        /// <inheritdoc />
        public bool TestConfiguration()
        {
            try
            {
                return ShipmentEndPoint.Ping();
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