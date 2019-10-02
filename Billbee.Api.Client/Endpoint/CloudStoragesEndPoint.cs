using Billbee.Api.Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billbee.Api.Client.EndPoint
{
    /// <summary>
    /// EndPoint to access all cloud storage relevant methods.
    /// </summary>
    public class CloudStoragesEndPoint : RestClientBaseClass
    {
        internal CloudStoragesEndPoint(ApiConfiguration config, ILogger logger = null) : base(logger, config)
        {
        }

        /// <summary>
        /// Requests a list of all available cloud storages of the user
        /// </summary>
        /// <returns>List of cloud storages.</returns>
        public ApiResult<List<CloudStorage>> GetCloudStorageList()
        {
            return requestResource<ApiResult<List<CloudStorage>>>($"/cloudstorages");
        }
    }
}
