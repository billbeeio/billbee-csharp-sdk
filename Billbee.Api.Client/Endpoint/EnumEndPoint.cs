using System.Collections.Generic;
using Billbee.Api.Client.Endpoint.Interfaces;
using Billbee.Api.Client.Model;

namespace Billbee.Api.Client.EndPoint
{
    public class EnumEndPoint : IEnumEndPoint
    {
        private readonly IBillbeeRestClient _restClient;

        internal EnumEndPoint(IBillbeeRestClient restClient)
        {
            _restClient = restClient;
        }
        
        [ApiMapping("/api/v1/enums/paymenttypes", HttpOperation.Get)]
        public List<EnumEntry> GetPaymentTypes()
        {
            return _restClient.Get<List<EnumEntry>>("/enums/paymenttypes");
        }

        [ApiMapping("/api/v1/enums/shippingcarriers", HttpOperation.Get)]
        public List<EnumEntry> GetShippingCarriers()
        {
            return _restClient.Get<List<EnumEntry>>("/enums/shippingcarriers");
        }

        [ApiMapping("/api/v1/enums/shipmenttypes", HttpOperation.Get)]
        public List<EnumEntry> GetShipmentTypes()
        {
            return _restClient.Get<List<EnumEntry>>("/enums/shipmenttypes");
        }

        [ApiMapping("/api/v1/enums/orderstates", HttpOperation.Get)]
        public List<EnumEntry> GetOrderStates()
        {
            return _restClient.Get<List<EnumEntry>>("/enums/orderstates");
        }
    }
}