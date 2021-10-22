using System.Collections.Generic;
using System.Net;
using Billbee.Api.Client.Model;
using Billbee.Api.Client.Model.Rechnungsdruck.WebApp.Model.Api;

namespace Billbee.Api.Client.EndPoint
{
    /// <summary>
    /// EndPoint for generation of shipments
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
        /// <param name="shipment">The shipment specification, that should be created.</param>
        /// <returns>The result of the shipment <see cref="ShipmentResult"/></returns>
        public ApiResult<ShipmentResult> PostShipment(PostShipment shipment)
        {
            return post<ApiResult<ShipmentResult>>("/shipment/shipment", shipment);
        }

        public ApiResult<ShipmentWithLabelResult> ShipOrderWithLabel(ShipmentWithLabel shipmentRequest)
        {
            return post<ApiResult<ShipmentWithLabelResult>>("/shipment/shipment", shipmentRequest);
        }

        /// <summary>
        /// Delivers a list of all registered shipping carriers
        /// </summary>
        /// <returns>List of available shipping carriers</returns>
        public List<ShippingCarrier> GetShippingCarriers()
        {
            return requestResource<List<ShippingCarrier>>("/shipment/shippingcarriers");
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
                case HttpStatusCode.OK:
                case HttpStatusCode.Accepted:
                case HttpStatusCode.Created:
                    return true;
                default:
                    return false;
            }
        }
    }
}
