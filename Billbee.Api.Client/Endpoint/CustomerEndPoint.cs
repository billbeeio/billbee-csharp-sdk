using Billbee.Api.Client.Model;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billbee.Api.Client.EndPoint
{
    /// <summary>
    /// Endpoint to access customer base data
    /// </summary>
    public class CustomerEndPoint: RestClientBaseClass
    {
        internal CustomerEndPoint(ApiConfiguration config, ILogger logger = null) : base(logger, config)
        {
        }

        /// <summary>
        /// Queries a list of customers
        /// </summary>
        /// <param name="page">page number</param>
        /// <param name="pageSize">page size</param>
        /// <returns>List of customers on the given page.</returns>
        public ApiPagedResult<List<Customer>> GetCustomerList(int page, int pageSize)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("page", page.ToString());
            parameters.Add("pageSize", pageSize.ToString());

            return requestResource<ApiPagedResult<List<Customer>>>($"/customers", parameters);
        }

        /// <summary>
        /// Adds a new customer
        /// </summary>
        /// <param name="customer">The customer object to add</param>
        /// <returns>The customer object after adding to the account.</returns>
        public ApiResult<Customer> AddCustomer(CustomerForCreation customer)
        {
            return post<ApiResult<Customer>>($"/customers", customer);
        }

        /// <summary>
        /// Queires the customer identified by the given id
        /// </summary>
        /// <param name="id">Id of the customer</param>
        /// <returns>Customer object.</returns>
        public ApiResult<Customer> GetCustomer(long id)
        {
            return requestResource<ApiResult<Customer>>($"/customers/{id}");
        }

        /// <summary>
        /// Updates a customer
        /// </summary>
        /// <param name="customer">Updated customer object</param>
        /// <returns>The customer object after update.</returns>
        public ApiResult<Customer> UpdateCustomer(Customer customer)
        {
            if (customer.Id == null)
            {
                throw new InvalidValueException("Id must not be null.");
            }
            return put<ApiResult<Customer>>($"/customers/{customer.Id}", customer);

        }

        /// <summary>
        /// Queries orders for a specific customer
        /// </summary>
        /// <param name="id">Id of the customer</param>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Count of entries per page</param>
        /// <returns>Resultset of the queried page of orders.</returns>
        public ApiPagedResult<List<Order>> GetOrdersForCustomer(long id, int page, int pageSize)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("page", page.ToString());
            parameters.Add("pageSize", pageSize.ToString());
            return requestResource<ApiPagedResult<List<Order>>>($"/customers/{id}/orders", parameters);
        }

        /// <summary>
        /// Queries addresses for a specific user
        /// </summary>
        /// <param name="id">Id of the customer</param>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Count of entries per page</param>
        /// <returns>Resultset of the queried page of addresses.</returns>
        public ApiPagedResult<List<CustomerAddress>> GetAddressesForCustomer(long id, int page, int pageSize)
        {
            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("page", page.ToString());
            parameters.Add("pageSize", pageSize.ToString());
            return requestResource<ApiPagedResult<List<CustomerAddress>>>($"/customers/{id}/addresses", parameters);
        }
    }
}
