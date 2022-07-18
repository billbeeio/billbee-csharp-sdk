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
    public class CloudStoragesEndPoint : ICloudStoragesEndPoint
    {
        private readonly IBillbeeRestClient _restClient;

        internal CloudStoragesEndPoint(IBillbeeRestClient restClient)
        {
            _restClient = restClient;
        }

        public ApiResult<List<CloudStorage>> GetCloudStorageList()
        {
            return _restClient.Get<ApiResult<List<CloudStorage>>>($"/cloudstorages");
        }
    }
}
