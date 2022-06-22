using System.Collections.Generic;
using Billbee.Api.Client.Model;

namespace Billbee.Api.Client.Interfaces.Endpoint
{
    /// <summary>
    /// EndPoint to access all cloud storage relevant methods.
    /// </summary>
    public interface ICloudStoragesEndPoint
    {
        /// <summary>
        /// Requests a list of all available cloud storages of the user
        /// </summary>
        /// <returns>List of cloud storages.</returns>
        ApiResult<List<CloudStorage>> GetCloudStorageList();
    }
}