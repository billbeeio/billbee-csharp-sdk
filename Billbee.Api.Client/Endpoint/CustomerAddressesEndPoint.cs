using Billbee.Api.Client.Endpoint.Interfaces;
using Billbee.Api.Client.Model;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

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

        public ApiPagedResult<List<CustomerAddress>> GetCustomerAddresses(int page, int pageSize)
        {
            var parameters = new NameValueCollection
            {
                { "page", page.ToString() },
                { "pageSize", pageSize.ToString() }
            };

            return _restClient.Get<ApiPagedResult<List<CustomerAddress>>>("/customer-addresses", parameters);
        }

        public ApiResult<CustomerAddress> GetCustomerAddress(long customerAddressId)
        {
            return _restClient.Get<ApiResult<CustomerAddress>>($"/customer-addresses/{customerAddressId}");
        }

        public ApiResult<CustomerAddress> AddCustomerAddresses(CustomerAddress customerAddress)
        {
            return _restClient.Post<ApiResult<CustomerAddress>>("/customer-addresses", customerAddress);
        }

        public ApiResult<CustomerAddress> UpdateCustomerAddress(CustomerAddress customerAddress)
        {
            if(customerAddress.Id == null || customerAddress.Id <= 0)
            {
                throw new InvalidOperationException("For an update operation a Customer-Address-ID must be provided.");
            }

            return _restClient.Put<ApiResult<CustomerAddress>>($"/customer-addresses/{customerAddress.Id.Value}", customerAddress);
        }
    }
}
