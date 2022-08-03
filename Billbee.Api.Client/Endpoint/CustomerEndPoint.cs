using Billbee.Api.Client.Model;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Billbee.Api.Client.Endpoint.Interfaces;

namespace Billbee.Api.Client.EndPoint
{
    /// <inheritdoc cref="Billbee.Api.Client.Endpoint.Interfaces.ICustomerEndPoint" />
    public class CustomerEndPoint: ICustomerEndPoint
    {
        private readonly IBillbeeRestClient _restClient;

        internal CustomerEndPoint(IBillbeeRestClient restClient)
        {
            _restClient = restClient;
        }

        [ApiMapping("/api/v1/customers", HttpOperation.Get)]
        public ApiPagedResult<List<Customer>> GetCustomerList(int page, int pageSize)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("page", page.ToString());
            parameters.Add("pageSize", pageSize.ToString());

            return _restClient.Get<ApiPagedResult<List<Customer>>>($"/customers", parameters);
        }

        [ApiMapping("/api/v1/customers", HttpOperation.Post)]
        public ApiResult<Customer> AddCustomer(CustomerForCreation customer)
        {
            return _restClient.Post<ApiResult<Customer>>($"/customers", customer);
        }

        [ApiMapping("/api/v1/customers/{id}", HttpOperation.Get)]
        public ApiResult<Customer> GetCustomer(long id)
        {
            return _restClient.Get<ApiResult<Customer>>($"/customers/{id}");
        }

        [ApiMapping("/api/v1/customers/{id}", HttpOperation.Put)]
        public ApiResult<Customer> UpdateCustomer(Customer customer)
        {
            if (customer.Id == null)
            {
                throw new InvalidValueException("Id must not be null.");
            }
            return _restClient.Put<ApiResult<Customer>>($"/customers/{customer.Id}", customer);

        }

        [ApiMapping("/api/v1/customers/{id}/orders", HttpOperation.Get)]
        public ApiPagedResult<List<Order>> GetOrdersForCustomer(long id, int page, int pageSize)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("page", page.ToString());
            parameters.Add("pageSize", pageSize.ToString());
            return _restClient.Get<ApiPagedResult<List<Order>>>($"/customers/{id}/orders", parameters);
        }

        [ApiMapping("/api/v1/customers/{id}/addresses", HttpOperation.Get)]
        public ApiPagedResult<List<CustomerAddress>> GetAddressesForCustomer(long id, int page, int pageSize)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("page", page.ToString());
            parameters.Add("pageSize", pageSize.ToString());
            return _restClient.Get<ApiPagedResult<List<CustomerAddress>>>($"/customers/{id}/addresses", parameters);
        }

        [ApiMapping("/api/v1/customers/{id}/addresses", HttpOperation.Post)]
        public ApiResult<CustomerAddress> AddAddressToCustomer(CustomerAddress customerAddress)
        {
            return _restClient.Post<ApiResult<CustomerAddress>>($"/customers/{customerAddress.CustomerId}/addresses", customerAddress);
        }

        [ApiMapping("/api/v1/customers/addresses/{id}", HttpOperation.Get)]
        public ApiResult<CustomerAddress> GetCustomerAddress(long customerAddressId)
        {
            return _restClient.Get<ApiResult<CustomerAddress>>($"/customers/addresses/{customerAddressId}");
        }

        [ApiMapping("/api/v1/customers/addresses/{id}", HttpOperation.Put)]
        public ApiResult<CustomerAddress> UpdateCustomerAddress(CustomerAddress customerAddress)
        {
            return _restClient.Put<ApiResult<CustomerAddress>>($"/customers/addresses/{customerAddress.Id}", customerAddress);
        }
        
        [ApiMapping("/api/v1/customers/addresses/{id}", HttpOperation.Patch)]
        public ApiResult<CustomerAddress> PatchCustomerAddress(long customerAddressId, Dictionary<string, string> fieldsToPatch)
        {
            return _restClient.Patch<ApiResult<CustomerAddress>>($"/customers/addresses/{customerAddressId}", null, fieldsToPatch);
        }
    }
}
