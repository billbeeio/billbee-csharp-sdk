using System.Collections.Specialized;
using System.Linq.Expressions;
using Billbee.Api.Client.EndPoint;
using Billbee.Api.Client.Model;
using Moq;

namespace Billbee.Api.Client.Test.EndPointTests;

[TestClass]
[TestCategory(TestCategories.EndpointTests)]
public class SearchEndPointTest
{
    private static SearchResult CreateTestSearchResult() => new();

    [TestMethod]
    public void Search_SearchTerm_Test()
    {
        var testSearchResult = CreateTestSearchResult();
        var search = new Search
        {
            Term = "foo",
            Type = new List<string> { "customer", "order" }
        };
        
        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Post<SearchResult>($"/search", search, null);
        object mockResult = testSearchResult;
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new SearchEndPoint(restClient);
            var result = uut.SearchTerm(search);
            Assert.IsNotNull(result);
        });
    }

}