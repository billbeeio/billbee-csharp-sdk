using System.Collections.Generic;
using System.Net;
using Billbee.Api.Client.Endpoint.Interfaces;
using Billbee.Api.Client.Model;
using Billbee.Api.Client.Model.Rechnungsdruck.WebApp.Model.Api;

namespace Billbee.Api.Client.EndPoint
{
    /// <inheritdoc cref="Billbee.Api.Client.Endpoint.Interfaces.IShipmentEndPoint" />
    public class ShipmentEndPoint : RestClientBaseClass, IShipmentEndPoint
    {
        internal ShipmentEndPoint(ApiConfiguration config, ILogger logger) : base(logger, config)
        {
        }

        public List<ShippingProvider> GetShippingProvider()
        {
            return requestResource<List<ShippingProvider>>("/shipment/shippingproviders");
        }

        public ApiResult<ShipmentResult> PostShipment(PostShipment shipment)
        {
            return post<ApiResult<ShipmentResult>>("/shipment/shipment", shipment);
        }

        public ApiResult<ShipmentWithLabelResult> ShipOrderWithLabel(ShipmentWithLabel shipmentRequest)
        {
            return post<ApiResult<ShipmentWithLabelResult>>("/shipment/shipment", shipmentRequest);
        }
        
        public List<ShippingCarrier> GetShippingCarriers()
        {
            return requestResource<List<ShippingCarrier>>("/shipment/shippingcarriers");
        }

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
