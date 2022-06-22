using Billbee.Api.Client.EndPoint;
using Billbee.Api.Client.Interfaces.Endpoint;

namespace Billbee.Api.Client.Interfaces
{
    /// <summary>
    /// Client for the Billbee API
    /// see https://app.billbee.io/swagger/ui/index or https://www.billbee.de/api/ for further information
    /// </summary>
    public interface IApiClient
    {
        /// <summary>
        /// Configuration, used to connect to the API <see cref="ApiConfiguration"/>.
        /// </summary>
        ApiConfiguration Configuration { get; }

        /// <summary>
        /// EndPoint to access events
        /// </summary>
        IEventEndPoint Events { get; }

        /// <summary>
        /// EndPoint to access order independent shipments
        /// </summary>
        IShipmentEndPoint Shipment { get; }

        IWebhookEndPoint Webhooks { get; }

        /// <summary>
        /// EndPoint to access Products
        /// </summary>
        IProductEndPoint Products { get; }

        /// <summary>
        /// EndPoint to allow automatic user creation
        /// </summary>
        IAutomaticProvisionEndPoint AutomaticProvision { get; }

        /// <summary>
        /// EndPoint to access customer base data
        /// </summary>
        ICustomerEndPoint Customer { get; }

        /// <summary>
        /// EndPoint for searches in customers, orders and products
        /// </summary>
        ISearchEndPoint Search { get; }

        /// <summary>
        /// EndPoint to access orders
        /// </summary>
        IOrderEndPoint Orders { get; }

        /// <summary>
        /// EndPoint to access cloud storages
        /// </summary>
        CloudStoragesEndPoint CloudStorages { get; }

        /// <summary>
        /// Validates, that access to the api is possible with the given configuration
        /// </summary>
        /// <returns></returns>
        bool TestConfiguration();
    }
}