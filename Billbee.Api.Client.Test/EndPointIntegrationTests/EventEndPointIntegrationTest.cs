using Billbee.Api.Client.Test.EndPointIntegrationTests.Helpers;

namespace Billbee.Api.Client.Test.EndPointIntegrationTests;

[TestClass]
[TestCategory(TestCategories.IntegrationTests)]
public class EventEndPointIntegrationTest
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
    public void Event_GetEvents_IntegrationTest()
    {
        var result = CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Events.GetEvents());
        Assert.IsTrue(result.Data.Count > 0);
    }
}