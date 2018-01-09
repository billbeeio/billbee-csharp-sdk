using Billbee.Api.Client.Model;
using Billbee.Api.Client.Model.Rechnungsdruck.WebApp.Model.Api;
using BillBee.API.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billbee.Api.Client.Endpoint
{
    /// <summary>
    /// Endpoint for generation of shipments
    /// </summary>
    public class ShipmentEndPoint : RestClientBaseClass
    {
        internal ShipmentEndPoint(ApiConfiguration config, ILogger logger) : base(logger, config)
        {

        }

        /// <summary>
        /// Requests a list of all available shipping providers and their products.
        /// </summary>
        /// <returns>List of shipping providers.</returns>
        public List<ShippingProvider> GetShippingProvider()
        {
            return requestResource<List<ShippingProvider>>("/shipment/shippingproviders");
        }

        /// <summary>
        /// Creates a new shipment.
        /// </summary>
        /// <param name="shipment">The shipment specification, that shoul be created.</param>
        /// <returns>The result of the shipment <see cref="ShipmentResult"/></returns>
        public ShipmentResult PostShipment(PostShipment shipment)
        {
            return post<ShipmentResult>("/shipment/shipment", shipment);
        }

        /// <summary>
        /// Creates a test request to the api
        /// </summary>
        /// <returns>true, when successful, false, when failed.</returns>
        internal bool Ping()
        {
            var result = get("/shipment/ping");
            switch (result)
            {
                case System.Net.HttpStatusCode.OK:
                case System.Net.HttpStatusCode.Accepted:
                case System.Net.HttpStatusCode.Created:
                    return true;
                default:
                    return false;
            }
        }
    }
}
