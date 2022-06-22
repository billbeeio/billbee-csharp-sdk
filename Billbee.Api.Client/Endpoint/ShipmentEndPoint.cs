using System.Collections.Generic;
using System.Net;
using Billbee.Api.Client.Interfaces.Endpoint;
using Billbee.Api.Client.Model;
using Billbee.Api.Client.Model.Rechnungsdruck.WebApp.Model.Api;

namespace Billbee.Api.Client.EndPoint
{
    /// <inheritdoc cref="IShipmentEndPoint" />
    public class ShipmentEndPoint : RestClientBaseClass, IShipmentEndPoint
    {
        internal ShipmentEndPoint(ApiConfiguration config, ILogger logger) : base(logger, config)
        {
        }

        /// <inheritdoc />
        public List<ShippingProvider> GetShippingProvider()
        {
            return requestResource<List<ShippingProvider>>("/shipment/shippingproviders");
        }

        /// <inheritdoc />
        public ApiResult<ShipmentResult> PostShipment(PostShipment shipment)
        {
            return post<ApiResult<ShipmentResult>>("/shipment/shipment", shipment);
        }

        /// <inheritdoc />
        public ApiResult<ShipmentWithLabelResult> ShipOrderWithLabel(ShipmentWithLabel shipmentRequest)
        {
            return post<ApiResult<ShipmentWithLabelResult>>("/shipment/shipment", shipmentRequest);
        }

        /// <inheritdoc />
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
