using Billbee.Api.Client.Test.EndPointIntegrationTests.Helpers;

namespace Billbee.Api.Client.Test.EndPointIntegrationTests;

[TestClass]
public class CloudStoragesEndPointIntegrationTest
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
    public void CloudStorages_GetCloudStorageList_IntegrationTest()
    {
        CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.CloudStorages.GetCloudStorageList());
    }
}