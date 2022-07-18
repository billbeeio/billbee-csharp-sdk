using System.Collections.Generic;
using System.Net;
using Billbee.Api.Client.Endpoint.Interfaces;
using Billbee.Api.Client.Model;
using Billbee.Api.Client.Model.Rechnungsdruck.WebApp.Model.Api;

namespace Billbee.Api.Client.EndPoint
{
    /// <inheritdoc cref="Billbee.Api.Client.Endpoint.Interfaces.IShipmentEndPoint" />
    public class ShipmentEndPoint : IShipmentEndPoint
    {
        private readonly IBillbeeRestClient _restClient;

        internal ShipmentEndPoint(IBillbeeRestClient restClient)
        {
            _restClient = restClient;
        }

        public List<ShippingProvider> GetShippingProvider()
        {
            return _restClient.Get<List<ShippingProvider>>("/shipment/shippingproviders");
        }

        public ApiResult<ShipmentResult> PostShipment(PostShipment shipment)
        {
            return _restClient.Post<ApiResult<ShipmentResult>>("/shipment/shipment", shipment);
        }

        public ApiResult<ShipmentWithLabelResult> ShipOrderWithLabel(ShipmentWithLabel shipmentRequest)
        {
            return _restClient.Post<ApiResult<ShipmentWithLabelResult>>("/shipment/shipment", shipmentRequest);
        }
        
        public List<ShippingCarrier> GetShippingCarriers()
        {
            return _restClient.Get<List<ShippingCarrier>>("/shipment/shippingcarriers");
        }

        internal bool Ping()
        {
            var result = _restClient.Get("/shipment/ping");
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
