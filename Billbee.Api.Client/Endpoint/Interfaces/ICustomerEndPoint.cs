using System.Collections.Generic;
using Billbee.Api.Client.Model;

namespace Billbee.Api.Client.Endpoint.Interfaces
{
    /// <summary>
    /// Endpoint to access customer base data
    /// </summary>
    public interface ICustomerEndPoint
    {
        /// <summary>
        /// Queries a list of customers
        /// </summary>
        /// <param name="page">page number</param>
        /// <param name="pageSize">page size</param>
        /// <returns>List of customers on the given page.</returns>
        ApiPagedResult<List<Customer>> GetCustomerList(int page, int pageSize);

        /// <summary>
        /// Adds a new customer
        /// </summary>
        /// <param name="customer">The customer object to add</param>
        /// <returns>The customer object after adding to the account.</returns>
        ApiResult<Customer> AddCustomer(CustomerForCreation customer);

        /// <summary>
        /// Queires the customer identified by the given id
        /// </summary>
        /// <param name="id">Id of the customer</param>
        /// <returns>Customer object.</returns>
        ApiResult<Customer> GetCustomer(long id);

        /// <summary>
        /// Updates a customer
        /// </summary>
        /// <param name="customer">Updated customer object</param>
        /// <returns>The customer object after update.</returns>
        ApiResult<Customer> UpdateCustomer(Customer customer);

        /// <summary>
        /// Queries orders for a specific customer
        /// </summary>
        /// <param name="id">Id of the customer</param>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Count of entries per page</param>
        /// <returns>Resultset of the queried page of orders.</returns>
        ApiPagedResult<List<Order>> GetOrdersForCustomer(long id, int page, int pageSize);

        /// <summary>
        /// Queries addresses for a specific user
        /// </summary>
        /// <param name="id">Id of the customer</param>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Count of entries per page</param>
        /// <returns>Resultset of the queried page of addresses.</returns>
        ApiPagedResult<List<CustomerAddress>> GetAddressesForCustomer(long id, int page, int pageSize);

        /// <summary>
        /// Create new address for a specific customer 
        /// </summary>
        /// <param name="customerAddress">The new customerAddress</param>
        /// <returns>The created customerAddress</returns>
        ApiResult<CustomerAddress> AddAddressToCustomer(CustomerAddress customerAddress);

        /// <summary>
        /// Gets a specific customer addresses
        /// </summary>
        /// <param name="customerAddressId">Id of the customer address</param>
        /// <returns>The specific customer address</returns>
        ApiResult<CustomerAddress> GetCustomerAddress(long customerAddressId);

        /// <summary>
        /// Updates all fields of an address
        /// </summary>
        /// <param name="customerAddress">The customer address</param>
        /// <returns>The updated customer address</returns>
        ApiResult<CustomerAddress> UpdateCustomerAddress(CustomerAddress customerAddress);

        /// <summary>
        /// Updates one or more fields of an address
        /// </summary>
        /// <param name="customerAddressId">The id of the customer address to be updated</param>
        /// /// <param name="fieldsToPatch">Dictionary which uses the field name as key and the new value as value.</param>
        /// <returns>The updated customer address</returns>
        ApiResult<CustomerAddress> PatchCustomerAddress(long customerAddressId, Dictionary<string, string> fieldsToPatch);
    }
}