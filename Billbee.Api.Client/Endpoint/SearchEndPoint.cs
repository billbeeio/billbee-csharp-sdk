using Billbee.Api.Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billbee.Api.Client.EndPoint
{
    /// <summary>
    /// Endpoint to search in orders, customers and products
    /// </summary>
    public class SearchEndPoint : RestClientBaseClass
    {
        internal SearchEndPoint(ApiConfiguration config, ILogger logger = null) : base(logger, config)
        {
        }

        /// <summary>
        /// Executes the given search
        /// </summary>
        /// <param name="search">search parameters</param>
        /// <returns>The result of the search</returns>
        public ApiResult<SearchResult> SearchTerm(Search search)
        {
            return post<ApiResult<SearchResult>>($"/search", search );
        }
    }
}
