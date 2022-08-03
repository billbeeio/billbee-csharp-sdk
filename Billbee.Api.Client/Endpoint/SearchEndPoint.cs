using Billbee.Api.Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Billbee.Api.Client.Endpoint.Interfaces;

namespace Billbee.Api.Client.EndPoint
{
    /// <inheritdoc cref="Billbee.Api.Client.Endpoint.Interfaces.ISearchEndPoint" />
    public class SearchEndPoint : ISearchEndPoint
    {
        private readonly IBillbeeRestClient _restClient;

        internal SearchEndPoint(IBillbeeRestClient restClient)
        {
            _restClient = restClient;
        }

        [ApiMapping("/api/v1/search", HttpOperation.Post)]
        public ApiResult<SearchResult> SearchTerm(Search search)
        {
            return _restClient.Post<ApiResult<SearchResult>>($"/search", search );
        }
    }
}
