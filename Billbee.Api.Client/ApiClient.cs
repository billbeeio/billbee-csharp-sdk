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
        private readonly ILogger _logger;

        public ApiClient(ApiConfiguration configuration = null, ILogger logger = null)
        {
            Configuration = configuration ?? new ApiConfiguration();
            _logger = logger;
        }

        public ApiClient(string configurationPath, ILogger logger = null)
        {
            _logger = logger;
            LoadConfigFromFile(configurationPath);
        }
        
        public ApiConfiguration Configuration { get; private set; }
        
        public IEventEndPoint Events => new EventEndPoint(Configuration, _logger);

        public IShipmentEndPoint Shipment => new ShipmentEndPoint(Configuration, _logger);

        public IWebhookEndPoint Webhooks => new WebhookEndPoint(Configuration, _logger);

        public IProductEndPoint Products => new ProductEndPoint(Configuration, _logger);

        public IAutomaticProvisionEndPoint AutomaticProvision => new AutomaticProvisionEndPoint(Configuration, _logger);

        public ICustomerEndPoint Customer => new CustomerEndPoint(Configuration, _logger);

        public ISearchEndPoint Search => new SearchEndPoint(Configuration, _logger);

        public IOrderEndPoint Orders => new OrderEndPoint(Configuration, _logger);

        public ICloudStoragesEndPoint CloudStorages => new CloudStoragesEndPoint(Configuration, _logger);

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

        private void LoadConfigFromFile(string path = null)
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
            Configuration = JsonConvert.DeserializeObject<ApiConfiguration>(configStr);
        }
    }
}
