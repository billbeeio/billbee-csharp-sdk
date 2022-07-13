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
    public class SearchEndPoint : RestClientBaseClass, ISearchEndPoint
    {
        internal SearchEndPoint(ApiConfiguration config, ILogger logger = null) : base(logger, config)
        {
        }

        public ApiResult<SearchResult> SearchTerm(Search search)
        {
            return post<ApiResult<SearchResult>>($"/search", search );
        }
    }
}
