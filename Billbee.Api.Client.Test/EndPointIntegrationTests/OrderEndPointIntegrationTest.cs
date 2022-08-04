using Billbee.Api.Client.Model;
using Billbee.Api.Client.Test.EndPointIntegrationTests.Helpers;

namespace Billbee.Api.Client.Test.EndPointIntegrationTests;

[TestClass]
public class OrderEndPointIntegrationTest
{
    public TestContext TestContext { get; set; }
    private long _countExpectedAfterTest = -1;

    [TestInitialize]
    public void TestInitialize()
    {
        IntegrationTestHelpers.CheckAccess(TestContext.ManagedType, TestContext.ManagedMethod);

        var result = CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Orders.GetOrderList(page: 1, pageSize: int.MaxValue));
        _countExpectedAfterTest = result.Data.Count;
    }

    [TestCleanup]
    public void TestCleanup()
    {
        var result = CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Orders.GetOrderList(page: 1, pageSize: int.MaxValue));
        Assert.AreEqual(_countExpectedAfterTest, result.Data.Count);
    }

    [TestMethod]
    [RequiresApiAccess]
    public void GetLayouts_IntegrationTest()
    {
        var result = CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Orders.GetLayouts());
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Data);
    }

    [TestMethod]
    [RequiresApiAccess]
    public void GetOrder_IntegrationTest()
    {
        Assert.Inconclusive();
    }

    [TestMethod]
    [RequiresApiAccess]
    public void GetPatchableFields_IntegrationTest()
    {
        Assert.Inconclusive();
    }

    [TestMethod]
    [RequiresApiAccess]
    public void PatchOrder_IntegrationTest()
    {
        Assert.Inconclusive();
    }

    [TestMethod]
    [RequiresApiAccess]
    public void GetOrderByExternalReference_IntegrationTest()
    {
        Assert.Inconclusive();
    }

    [TestMethod]
    [RequiresApiAccess]
    public void GetOrderByExternalIdAndPartner_IntegrationTest()
    {
        Assert.Inconclusive();
    }

    [TestMethod]
    [RequiresApiAccess]
    public void GetOrderList_IntegrationTest()
    {
        Assert.Inconclusive();
    }

    [TestMethod]
    [RequiresApiAccess]
    public void GetInvoiceList_IntegrationTest()
    {
        Assert.Inconclusive();
    }

    [TestMethod]
    [RequiresApiAccess]
    public void PostNewOrder_IntegrationTest()
    {
        Assert.Inconclusive();
    }

    [TestMethod]
    [RequiresApiAccess]
    public void AddTags_IntegrationTest()
    {
        Assert.Inconclusive();
    }

    [TestMethod]
    [RequiresApiAccess]
    public void UpdateTags_IntegrationTest()
    {
        Assert.Inconclusive();
    }

    [TestMethod]
    [RequiresApiAccess]
    public void AddShipment_IntegrationTest()
    {
        Assert.Inconclusive();
    }

    [TestMethod]
    [RequiresApiAccess]
    public void CreateDeliveryNote_IntegrationTest()
    {
        Assert.Inconclusive();
    }

    [TestMethod]
    [RequiresApiAccess]
    public void CreateInvoice_IntegrationTest()
    {
        Assert.Inconclusive();
    }

    [TestMethod]
    [RequiresApiAccess]
    public void ChangeOrderState_IntegrationTest()
    {
        Assert.Inconclusive();
    }

    [TestMethod]
    [RequiresApiAccess]
    public void SendMailForOrder_IntegrationTest()
    {
        Assert.Inconclusive();
    }

    [TestMethod]
    [RequiresApiAccess]
    public void CreateEventAtOrder_IntegrationTest()
    {
        Assert.Inconclusive();
    }

    [TestMethod]
    [RequiresApiAccess]
    public void ParsePlaceholders_IntegrationTest()
    {
        Assert.Inconclusive();
    }
}