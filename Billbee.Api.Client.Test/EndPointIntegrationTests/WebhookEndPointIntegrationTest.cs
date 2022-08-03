using Billbee.Api.Client.Model;
using Billbee.Api.Client.Test.EndPointIntegrationTests.Helpers;

namespace Billbee.Api.Client.Test.EndPointIntegrationTests;

[TestClass]
public class WebhookEndPointIntegrationTest
{
    public TestContext TestContext { get; set; }
    private long _webhookCountBeforeTest = -1;
    private long _webhookCoundExpectedAfterTest = -1;

    [TestInitialize]
    public void TestInitialize()
    {
        IntegrationTestHelpers.CheckAccess(TestContext.ManagedType, TestContext.ManagedMethod);

        var result = CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Webhooks.GetWebhooks());
        Assert.IsNotNull(result);

        _webhookCountBeforeTest = result.Count;
        _webhookCoundExpectedAfterTest = result.Count;
    }

    [TestCleanup]
    public void TestCleanup()
    {
        var result = CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Webhooks.GetWebhooks());
        Assert.IsNotNull(result);

        Assert.AreEqual(_webhookCoundExpectedAfterTest, result.Count);
    }

    [TestMethod]
    [RequiresApiAccess]
    public void GetFilters_IntegrationTest()
    {
        CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Webhooks.GetFilters());
    }

    [TestMethod]
    [RequiresApiAccess]
    public void GetWebhooks_IntegrationTest()
    {
        CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Webhooks.GetWebhooks());
    }

    [TestMethod]
    [RequiresApiAccess]
    public void GetWebhook_IntegrationTest()
    {
        var webhook = CrudHelpers.Create(() => IntegrationTestHelpers.ApiClient.Webhooks.CreateWebhook(TestData.WebHook));
        CrudHelpers.GetOne<Webhook>((id) => IntegrationTestHelpers.ApiClient.Webhooks.GetWebhook(id), webhook.Id);
        
        // cleanup
        CrudHelpers.DeleteOne<Webhook>((id) => IntegrationTestHelpers.ApiClient.Webhooks.DeleteWebhook(id), webhook.Id);
    }

    [TestMethod]
    [RequiresApiAccess]
    public void CreateWebhook_IntegrationTest()
    {
        Webhook webhook = CrudHelpers.Create(() => IntegrationTestHelpers.ApiClient.Webhooks.CreateWebhook(TestData.WebHook));
        Assert.AreEqual(TestData.WebHook.WebHookUri, webhook.WebHookUri);
        
        // cleanup
        CrudHelpers.DeleteOne<Webhook>((id) => IntegrationTestHelpers.ApiClient.Webhooks.DeleteWebhook(id), webhook.Id);
    }

    [TestMethod]
    [RequiresApiAccess]
    public void DeleteAllWebhooks_IntegrationTest()
    {
        if (_webhookCountBeforeTest > 0)
        {
            Assert.Inconclusive("The connected account contains webhooks. Cannot execute this test-method, because it would delete all existing webhooks.");
        }
        
        CrudHelpers.Create(() => IntegrationTestHelpers.ApiClient.Webhooks.CreateWebhook(TestData.WebHook));
        CrudHelpers.Create(() => IntegrationTestHelpers.ApiClient.Webhooks.CreateWebhook(TestData.WebHook));
        var webhooks = CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Webhooks.GetWebhooks());
        Assert.AreEqual(2, webhooks.Count);

        CrudHelpers.DeleteAll<Webhook>(() => IntegrationTestHelpers.ApiClient.Webhooks.DeleteAllWebhooks());
        
        webhooks = CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Webhooks.GetWebhooks());
        Assert.AreEqual(0, webhooks.Count);

        _webhookCoundExpectedAfterTest = 0;
    }

    [TestMethod]
    [RequiresApiAccess]
    public void DeleteWebhook_IntegrationTest()
    {
        Webhook webhook = CrudHelpers.Create(() => IntegrationTestHelpers.ApiClient.Webhooks.CreateWebhook(TestData.WebHook));
        var webhooks = CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Webhooks.GetWebhooks());
        Assert.IsTrue(webhooks.Count > 0);

        var webhookToDelete = webhook;
        Assert.IsNotNull(webhookToDelete);
        CrudHelpers.DeleteOne<Webhook>((id) => IntegrationTestHelpers.ApiClient.Webhooks.DeleteWebhook(id), webhookToDelete.Id);

        CrudHelpers.GetOneExpectException<Webhook>((id) => IntegrationTestHelpers.ApiClient.Webhooks.GetWebhook(id), webhookToDelete.Id);
    }

    [TestMethod]
    [RequiresApiAccess]
    public void UpdateWebhook_IntegrationTest()
    {
        Webhook webhook = CrudHelpers.Create(() => IntegrationTestHelpers.ApiClient.Webhooks.CreateWebhook(TestData.WebHook));
        webhook = CrudHelpers.GetOne<Webhook>((id) => IntegrationTestHelpers.ApiClient.Webhooks.GetWebhook(id), webhook.Id);

        webhook.Description = "modified";
        CrudHelpers.Put<Webhook>((webhook) => IntegrationTestHelpers.ApiClient.Webhooks.UpdateWebhook(webhook), webhook);
        
        webhook = CrudHelpers.GetOne<Webhook>((id) => IntegrationTestHelpers.ApiClient.Webhooks.GetWebhook(id), webhook.Id);
        Assert.AreEqual("modified", webhook.Description);
        
        // cleanup
        CrudHelpers.DeleteOne<Webhook>((id) => IntegrationTestHelpers.ApiClient.Webhooks.DeleteWebhook(id), webhook.Id);
    }
}