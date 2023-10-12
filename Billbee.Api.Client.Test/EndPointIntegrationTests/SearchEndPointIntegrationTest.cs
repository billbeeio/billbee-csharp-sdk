using Billbee.Api.Client.Model;
using Billbee.Api.Client.Test.EndPointIntegrationTests.Helpers;

namespace Billbee.Api.Client.Test.EndPointIntegrationTests;

[TestClass]
[TestCategory(TestCategories.IntegrationTests)]
public class SearchEndPointIntegrationTest
{
#pragma warning disable CS8618
    public TestContext TestContext { get; set; }
#pragma warning restore CS8618

    [TestInitialize]
    public void TestInitialize()
    {
        IntegrationTestHelpers.CheckAccess(TestContext.ManagedType, TestContext.ManagedMethod);
    }
    
    [TestMethod]
    [RequiresApiAccess]
    public void Search_SearchTerm_IntegrationTest()
    {
        var customer = CrudHelpers.CreateApiResult(c => IntegrationTestHelpers.ApiClient.Customer.AddCustomer(c), TestData.Customer);
        Assert.IsNotNull(customer);
        
        var search = new Search
        {
            Term = "john",
            Type = new List<string> { "customer" }
        };
        var result = IntegrationTestHelpers.ApiClient.Search.SearchTerm(search);
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Customers);
        Assert.IsTrue(result.Customers.Count > 0);
    }
}