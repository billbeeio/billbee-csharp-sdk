using Billbee.Api.Client.EndPoint;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;
using Billbee.Api.Client.Endpoint.Interfaces;

namespace Billbee.Api.Client
{
    /// <inheritdoc cref="Billbee.Api.Client.IApiClient"/>
    public class ApiClient : IApiClient
    {
        private ILogger _logger;
        private BillbeeRestClient _restClient;
        
        public ApiClient(ApiConfiguration configuration = null, ILogger logger = null)
        {
            var config = configuration ?? new ApiConfiguration();
            _init(config, logger);
        }

        public ApiClient(string configurationPath, ILogger logger = null)
        {
            var config = LoadConfigFromFile(configurationPath);
            _init(config, logger);
        }

        private void _init(ApiConfiguration configuration, ILogger logger)
        {
            _logger = logger;
            Configuration = configuration;
            _restClient = new BillbeeRestClient(_logger, Configuration);
        }
        
        public ApiConfiguration Configuration { get; private set; }
        
        public IEventEndPoint Events => new EventEndPoint(_restClient);

        public IShipmentEndPoint Shipment => new ShipmentEndPoint(_restClient);

        public IWebhookEndPoint Webhooks => new WebhookEndPoint(_restClient);

        public IProductEndPoint Products => new ProductEndPoint(_restClient);

        public IAutomaticProvisionEndPoint AutomaticProvision => new AutomaticProvisionEndPoint(_restClient);

        public ICustomerEndPoint Customer => new CustomerEndPoint(_restClient);

        public ICustomerAddressesEndPoint CustomerAddresses => new CustomerAddressesEndPoint(_restClient);

        public ISearchEndPoint Search => new SearchEndPoint(_restClient);

        public IOrderEndPoint Orders => new OrderEndPoint(_restClient);

        public ICloudStoragesEndPoint CloudStorages => new CloudStoragesEndPoint(_restClient);
        
        public bool TestConfiguration()
        {
            try
            {
                return ((ShipmentEndPoint)Shipment).Ping();
            }
            catch (Exception) // If user selects to throw exceptions on server side errors, or other errors occurs.
            {
                return false;
            }
        }

        private ApiConfiguration LoadConfigFromFile(string path = null)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                path = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location);
            }
            
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new FileNotFoundException($"The assembly file path could not be found.");
            }
            
            if (!path.ToLower().EndsWith(".json"))
            {
                path += ".json";
            }

            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"The config file {path} could not be found.");
            }

            var configStr = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<ApiConfiguration>(configStr);
        }
    }
}
