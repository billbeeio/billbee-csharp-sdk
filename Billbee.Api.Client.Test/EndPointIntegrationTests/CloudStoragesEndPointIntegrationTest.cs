using Billbee.Api.Client.Test.EndPointIntegrationTests.Helpers;

namespace Billbee.Api.Client.Test.EndPointIntegrationTests;

[TestClass]
public class CloudStoragesEndPointIntegrationTest
{
    public TestContext TestContext { get; set; }

    [TestInitialize]
    public void TestInitialize()
    {
        IntegrationTestHelpers.CheckAccess(TestContext.ManagedType, TestContext.ManagedMethod);
    }

    [TestMethod]
    [RequiresApiAccess]
    public void GetCloudStorageList_IntegrationTest()
    {
        CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.CloudStorages.GetCloudStorageList());
    }
}