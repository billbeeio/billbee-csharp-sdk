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
    public class CustomerEndPoint: RestClientBaseClass, ICustomerEndPoint
    {
        internal CustomerEndPoint(ApiConfiguration config, ILogger logger = null) : base(logger, config)
        {
        }

        public ApiPagedResult<List<Customer>> GetCustomerList(int page, int pageSize)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("page", page.ToString());
            parameters.Add("pageSize", pageSize.ToString());

            return requestResource<ApiPagedResult<List<Customer>>>($"/customers", parameters);
        }

        public ApiResult<Customer> AddCustomer(CustomerForCreation customer)
        {
            return post<ApiResult<Customer>>($"/customers", customer);
        }

        public ApiResult<Customer> GetCustomer(long id)
        {
            return requestResource<ApiResult<Customer>>($"/customers/{id}");
        }

        public ApiResult<Customer> UpdateCustomer(Customer customer)
        {
            if (customer.Id == null)
            {
                throw new InvalidValueException("Id must not be null.");
            }
            return put<ApiResult<Customer>>($"/customers/{customer.Id}", customer);

        }

        public ApiPagedResult<List<Order>> GetOrdersForCustomer(long id, int page, int pageSize)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("page", page.ToString());
            parameters.Add("pageSize", pageSize.ToString());
            return requestResource<ApiPagedResult<List<Order>>>($"/customers/{id}/orders", parameters);
        }

        public ApiPagedResult<List<CustomerAddress>> GetAddressesForCustomer(long id, int page, int pageSize)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("page", page.ToString());
            parameters.Add("pageSize", pageSize.ToString());
            return requestResource<ApiPagedResult<List<CustomerAddress>>>($"/customers/{id}/addresses", parameters);
        }
    }
}
