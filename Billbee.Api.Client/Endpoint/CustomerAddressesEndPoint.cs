using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Billbee.Api.Client.Endpoint.Interfaces;
using Billbee.Api.Client.Model;

namespace Billbee.Api.Client.EndPoint
{
    /// <inheritdoc cref="Billbee.Api.Client.Endpoint.Interfaces.ICustomerAddressesEndPoint" />
    public class CustomerAddressesEndPoint : ICustomerAddressesEndPoint
    {
        private readonly IBillbeeRestClient _restClient;

        internal CustomerAddressesEndPoint(IBillbeeRestClient restClient)
        {
            _restClient = restClient;
        }

        [ApiMapping("/api/v1/customer-addresses", HttpOperation.Get)]
        public ApiPagedResult<List<CustomerAddress>> GetCustomerAddresses(int page, int pageSize)
        {
            var parameters = new NameValueCollection
            {
                { "page", page.ToString() },
                { "pageSize", pageSize.ToString() }
            };

            return _restClient.Get<ApiPagedResult<List<CustomerAddress>>>("/customer-addresses", parameters);
        }

        [ApiMapping("/api/v1/customer-addresses/{id}", HttpOperation.Get)]
        public ApiResult<CustomerAddress> GetCustomerAddress(long customerAddressId)
        {
            return _restClient.Get<ApiResult<CustomerAddress>>($"/customer-addresses/{customerAddressId}");
        }

        [ApiMapping("/api/v1/customer-addresses", HttpOperation.Post)]
        public ApiResult<CustomerAddress> AddCustomerAddress(CustomerAddress customerAddress)
        {
            return _restClient.Post<ApiResult<CustomerAddress>>("/customer-addresses", customerAddress);
        }

        [ApiMapping("/api/v1/customer-addresses/{id}", HttpOperation.Put)]
        public ApiResult<CustomerAddress> UpdateCustomerAddress(CustomerAddress customerAddress)
        {
            if(customerAddress.Id == null || customerAddress.Id <= 0)
            {
                throw new InvalidValueException("Id must not be null.");
            }

            return _restClient.Put<ApiResult<CustomerAddress>>($"/customer-addresses/{customerAddress.Id.Value}", customerAddress);
        }
    }
}