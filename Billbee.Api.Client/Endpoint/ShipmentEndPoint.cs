using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
        
        [ApiMapping("/api/v1/shipment/shipments", HttpOperation.Get)]
        public ApiPagedResult<List<Shipment>> GetShipments(int page = 1, int pageSize = 50, DateTime? createdAtMin = null, DateTime? createdAtMax = null, long? orderId = null, long? minimumShipmentId = null, long? shippingProviderId = null)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("page", page.ToString());
            parameters.Add("pageSize", pageSize.ToString());
            if (createdAtMin != null)
            {
                parameters.Add("createdAtMin", createdAtMin.Value.ToString("yyyy-MM-dd"));
            }
            if (createdAtMax != null)
            {
                parameters.Add("createdAtMax", createdAtMax.Value.ToString("yyyy-MM-dd"));
            }
            if (orderId != null)
            {
                parameters.Add("orderId", orderId.Value.ToString());
            }
            if (minimumShipmentId != null)
            {
                parameters.Add("minimumShipmentId", minimumShipmentId.Value.ToString());
            }
            if (shippingProviderId != null)
            {
                parameters.Add("shippingProviderId", shippingProviderId.Value.ToString());
            }
            
            return _restClient.Get<ApiPagedResult<List<Shipment>>>($"/shipment/shipments", parameters);
        }

        [ApiMapping("/api/v1/shipment/shippingproviders", HttpOperation.Get)]
        public List<ShippingProvider> GetShippingProvider()
        {
            return _restClient.Get<List<ShippingProvider>>("/shipment/shippingproviders");
        }

        [ApiMapping("/api/v1/shipment/shipment", HttpOperation.Post)]
        public ApiResult<ShipmentResult> PostShipment(PostShipment shipment)
        {
            return _restClient.Post<ApiResult<ShipmentResult>>("/shipment/shipment", shipment);
        }

        [ApiMapping("/api/v1/shipment/shipwithlabel", HttpOperation.Post)]
        public ApiResult<ShipmentWithLabelResult> ShipOrderWithLabel(ShipmentWithLabel shipmentRequest)
        {
            return _restClient.Post<ApiResult<ShipmentWithLabelResult>>("/shipment/shipwithlabel", shipmentRequest);
        }
        
        [ApiMapping("/api/v1/shipment/shippingcarriers", HttpOperation.Get)]
        public List<ShippingCarrier> GetShippingCarriers()
        {
            return _restClient.Get<List<ShippingCarrier>>("/shipment/shippingcarriers");
        }

        [ApiMapping("/api/v1/shipment/ping", HttpOperation.Get)]
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
