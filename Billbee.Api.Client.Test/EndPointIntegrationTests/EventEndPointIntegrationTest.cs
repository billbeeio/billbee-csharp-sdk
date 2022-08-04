using Billbee.Api.Client.Test.EndPointIntegrationTests.Helpers;

namespace Billbee.Api.Client.Test.EndPointIntegrationTests;

[TestClass]
public class EventEndPointIntegrationTest
{
    public TestContext TestContext { get; set; }

    [TestInitialize]
    public void TestInitialize()
    {
        IntegrationTestHelpers.CheckAccess(TestContext.ManagedType, TestContext.ManagedMethod);
    }

    [TestMethod]
    [RequiresApiAccess]
    public void GetEvents_IntegrationTest()
    {
        var result = CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Events.GetEvents());
        Assert.IsTrue(result.Data.Count > 0);
    }
}