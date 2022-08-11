using System.Collections.Generic;
using Billbee.Api.Client.Model;

namespace Billbee.Api.Client.Endpoint.Interfaces
{
    /// <summary>
    /// Endpoint to access customer addresses
    /// </summary>
    public interface ICustomerAddressesEndPoint
    {
        /// <summary>
        /// Queries a list of customers addresses
        /// </summary>
        /// <param name="page">page number</param>
        /// <param name="pageSize">page size</param>
        /// <returns>List of customers on the given page.</returns>
        ApiPagedResult<List<CustomerAddress>> GetCustomerAddresses(int page, int pageSize);

        /// <summary>
        /// Gets a single customer address by its id
        /// <param name="customerAddressId">customer address id</param>
        /// </summary>
        ApiResult<CustomerAddress> GetCustomerAddress(long customerAddressId);

        /// <summary>
        /// Creates a new customer address
        /// <param name="customerAddress">customer address model</param>
        /// </summary>
        ApiResult<CustomerAddress> AddCustomerAddress(CustomerAddress customerAddress);

        /// <summary>
        /// Updates a new customer address
        /// </summary>
        /// <param name="customerAddress">customer address model</param>
        ApiResult<CustomerAddress> UpdateCustomerAddress(CustomerAddress customerAddress);
    }
}