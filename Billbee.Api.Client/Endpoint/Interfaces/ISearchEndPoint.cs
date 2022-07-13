using Billbee.Api.Client.Model;

namespace Billbee.Api.Client.Endpoint.Interfaces
{
    /// <summary>
    /// Endpoint to search in orders, customers and products
    /// </summary>
    public interface ISearchEndPoint
    {
        /// <summary>
        /// Executes the given search
        /// </summary>
        /// <param name="search">search parameters</param>
        /// <returns>The result of the search</returns>
        ApiResult<SearchResult> SearchTerm(Search search);
    }
}