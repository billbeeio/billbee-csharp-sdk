using Billbee.Api.Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Billbee.Api.Client.Endpoint.Interfaces;

namespace Billbee.Api.Client.EndPoint
{
    /// <inheritdoc cref="Billbee.Api.Client.Endpoint.Interfaces.ICloudStoragesEndPoint" />
    public class CloudStoragesEndPoint : RestClientBaseClass, ICloudStoragesEndPoint
    {
        internal CloudStoragesEndPoint(ApiConfiguration config, ILogger logger = null) : base(logger, config)
        {
        }

        public ApiResult<List<CloudStorage>> GetCloudStorageList()
        {
            return requestResource<ApiResult<List<CloudStorage>>>($"/cloudstorages");
        }
    }
}
