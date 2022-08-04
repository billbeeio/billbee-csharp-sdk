using Billbee.Api.Client.Model;
using Billbee.Api.Client.Test.EndPointIntegrationTests.Helpers;

namespace Billbee.Api.Client.Test.EndPointIntegrationTests.Helpers
{
    public static partial class TestData
    {
        public static Webhook WebHook => new Webhook
        {
            Id = null,
            WebHookUri = "https://webhook.site/5290627f-b5e3-4123-a715-26a721054617?noecho",
            Secret = "4e4451af-63c5-44f4-a3c5-1dcf8617fc5c",
            Description = "A simple description",
            IsPaused = true,
            Filters = new List<string> { "order.created" },
            Headers = new Dictionary<string, string>
                { { "TestHeader", "TestHeaderValue" }, { "Another Testheader", "Another Value" } },
            Properties = new Dictionary<string, object>()
        };
    }
}

namespace Billbee.Api.Client.Test.EndPointIntegrationTests
{
    [TestClass]
    public class WebhookEndPointIntegrationTest
    {
#pragma warning disable CS8618
        public TestContext TestContext { get; set; }
#pragma warning restore CS8618
        private long _countBeforeTest = -1;
        private long _countExpectedAfterTest = -1;

        [TestInitialize]
        public void TestInitialize()
        {
            IntegrationTestHelpers.CheckAccess(TestContext.ManagedType, TestContext.ManagedMethod);

            var result = CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Webhooks.GetWebhooks());
            _countBeforeTest = result.Count;
            _countExpectedAfterTest = result.Count;
        }

        [TestCleanup]
        public void TestCleanup()
        {
            var result = CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Webhooks.GetWebhooks());
            Assert.AreEqual(_countExpectedAfterTest, result.Count);
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
            var webhook = CrudHelpers.Create(w => IntegrationTestHelpers.ApiClient.Webhooks.CreateWebhook(w),
                TestData.WebHook);
            CrudHelpers.GetOne<Webhook>((id) => IntegrationTestHelpers.ApiClient.Webhooks.GetWebhook(id), webhook.Id);

            // cleanup
            CrudHelpers.DeleteOne<Webhook>((id) => IntegrationTestHelpers.ApiClient.Webhooks.DeleteWebhook(id),
                webhook.Id);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void CreateWebhook_IntegrationTest()
        {
            var webhook = CrudHelpers.Create(w => IntegrationTestHelpers.ApiClient.Webhooks.CreateWebhook(w),
                TestData.WebHook);
            Assert.AreEqual(TestData.WebHook.WebHookUri, webhook.WebHookUri);

            // cleanup
            CrudHelpers.DeleteOne<Webhook>((id) => IntegrationTestHelpers.ApiClient.Webhooks.DeleteWebhook(id),
                webhook.Id);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void DeleteAllWebhooks_IntegrationTest()
        {
            if (_countBeforeTest > 0)
            {
                Assert.Inconclusive(
                    "The connected account contains webhooks. Cannot execute this test-method, because it would delete all existing webhooks.");
            }

            CrudHelpers.Create(w => IntegrationTestHelpers.ApiClient.Webhooks.CreateWebhook(w), TestData.WebHook);
            CrudHelpers.Create(w => IntegrationTestHelpers.ApiClient.Webhooks.CreateWebhook(w), TestData.WebHook);
            var webhooks = CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Webhooks.GetWebhooks());
            Assert.AreEqual(2, webhooks.Count);

            CrudHelpers.DeleteAll<Webhook>(() => IntegrationTestHelpers.ApiClient.Webhooks.DeleteAllWebhooks());

            webhooks = CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Webhooks.GetWebhooks());
            Assert.AreEqual(0, webhooks.Count);

            _countExpectedAfterTest = 0;
        }

        [TestMethod]
        [RequiresApiAccess]
        public void DeleteWebhook_IntegrationTest()
        {
            var webhook = CrudHelpers.Create(w => IntegrationTestHelpers.ApiClient.Webhooks.CreateWebhook(w),
                TestData.WebHook);
            var webhooks = CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Webhooks.GetWebhooks());
            Assert.IsTrue(webhooks.Count > 0);

            var webhookToDelete = webhook;
            Assert.IsNotNull(webhookToDelete);
            CrudHelpers.DeleteOne<Webhook>((id) => IntegrationTestHelpers.ApiClient.Webhooks.DeleteWebhook(id),
                webhookToDelete.Id);

            CrudHelpers.GetOneExpectException<Webhook>((id) => IntegrationTestHelpers.ApiClient.Webhooks.GetWebhook(id),
                webhookToDelete.Id);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void UpdateWebhook_IntegrationTest()
        {
            var webhook = CrudHelpers.Create(w => IntegrationTestHelpers.ApiClient.Webhooks.CreateWebhook(w),
                TestData.WebHook);
            webhook = CrudHelpers.GetOne<Webhook>((id) => IntegrationTestHelpers.ApiClient.Webhooks.GetWebhook(id),
                webhook.Id);

            webhook.Description = "modified";
            CrudHelpers.Put<Webhook>((webhook) => IntegrationTestHelpers.ApiClient.Webhooks.UpdateWebhook(webhook),
                webhook);

            webhook = CrudHelpers.GetOne<Webhook>((id) => IntegrationTestHelpers.ApiClient.Webhooks.GetWebhook(id),
                webhook.Id);
            Assert.AreEqual("modified", webhook.Description);

            // cleanup
            CrudHelpers.DeleteOne<Webhook>((id) => IntegrationTestHelpers.ApiClient.Webhooks.DeleteWebhook(id),
                webhook.Id);
        }
    }
}