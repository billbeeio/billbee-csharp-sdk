using Billbee.Api.Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Billbee.Api.Client.Interfaces.Endpoint;

namespace Billbee.Api.Client.EndPoint
{
    /// <inheritdoc cref="Billbee.Api.Client.Interfaces.Endpoint.ICloudStoragesEndPoint" />
    public class CloudStoragesEndPoint : RestClientBaseClass, ICloudStoragesEndPoint
    {
        internal CloudStoragesEndPoint(ApiConfiguration config, ILogger logger = null) : base(logger, config)
        {
        }

        /// <inheritdoc />
        public ApiResult<List<CloudStorage>> GetCloudStorageList()
        {
            return requestResource<ApiResult<List<CloudStorage>>>($"/cloudstorages");
        }
    }
}
