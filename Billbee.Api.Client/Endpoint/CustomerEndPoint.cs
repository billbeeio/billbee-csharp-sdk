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

        public ApiPagedResult<List<Customer>> GetCustomerList(int page, int pageSize)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("page", page.ToString());
            parameters.Add("pageSize", pageSize.ToString());

            return _restClient.Get<ApiPagedResult<List<Customer>>>($"/customers", parameters);
        }

        public ApiResult<Customer> AddCustomer(CustomerForCreation customer)
        {
            return _restClient.Post<ApiResult<Customer>>($"/customers", customer);
        }

        public ApiResult<Customer> GetCustomer(long id)
        {
            return _restClient.Get<ApiResult<Customer>>($"/customers/{id}");
        }

        public ApiResult<Customer> UpdateCustomer(Customer customer)
        {
            if (customer.Id == null)
            {
                throw new InvalidValueException("Id must not be null.");
            }
            return _restClient.Put<ApiResult<Customer>>($"/customers/{customer.Id}", customer);

        }

        public ApiPagedResult<List<Order>> GetOrdersForCustomer(long id, int page, int pageSize)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("page", page.ToString());
            parameters.Add("pageSize", pageSize.ToString());
            return _restClient.Get<ApiPagedResult<List<Order>>>($"/customers/{id}/orders", parameters);
        }

        public ApiPagedResult<List<CustomerAddress>> GetAddressesForCustomer(long id, int page, int pageSize)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("page", page.ToString());
            parameters.Add("pageSize", pageSize.ToString());
            return _restClient.Get<ApiPagedResult<List<CustomerAddress>>>($"/customers/{id}/addresses", parameters);
        }
    }
}
