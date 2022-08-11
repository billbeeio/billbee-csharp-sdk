using System.Collections.Specialized;
using System.Linq.Expressions;
using Billbee.Api.Client.EndPoint;
using Billbee.Api.Client.Model;
using Moq;
using RestSharp;

namespace Billbee.Api.Client.Test.EndPointTests;

[TestClass]
public class WebhookEndPointTest
{
    private static Webhook CreateTestWebHook()
    {
        return new Webhook
        {
            Description = "theDesc",
            Filters = new List<string> { "filter1", "filter2" },
            Headers = new Dictionary<string, string> { { "key1", "value1" }, { "key2", "value2" } },
            Id = "theId",
            Properties = new Dictionary<string, object> { { "prop1", "foo" }, { "prop2", 5 } },
            Secret = "theSecret",
            IsPaused = false,
            WebHookUri = "theWebHookUri"
        };
    }

    private static readonly WebhookFilter TestWebHookFilter = new WebhookFilter
    {
        Description = "theDesc",
        Name = "theName"
    };
    
    [TestMethod]
    public void Webhook_GetWebhooks_Test()
    {
        var testWebHook = CreateTestWebHook();
        
        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Get<List<Webhook>>("/webhooks", null);
        object mockResult = new List<Webhook> { testWebHook };
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new WebhookEndPoint(restClient);
            var result = uut.GetWebhooks();
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(testWebHook.Id, result.First().Id);
        });
    }
 
    [TestMethod]
    public void Webhook_GetWebhook_Test()
    {
        var testWebHook = CreateTestWebHook();
        
        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Get<Webhook>($"/webhooks/{testWebHook.Id}", null);
        object mockResult = testWebHook;
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new WebhookEndPoint(restClient);
            var result = uut.GetWebhook(testWebHook.Id);
            Assert.AreEqual(testWebHook.Id, result.Id);
        });
    }

    [TestMethod]
    public void Webhook_GetFilters_Test()
    {
        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Get<List<WebhookFilter>>("/webhooks/filters", null);
        object mockResult = new List<WebhookFilter> { TestWebHookFilter };
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new WebhookEndPoint(restClient);
            var result = uut.GetFilters();
            Assert.AreEqual(TestWebHookFilter.Name, result.First().Name);
        });
    }
    
    [TestMethod]
    public void Webhook_DeleteAllWebhooks_Test()
    {
        var restClientMock = new Mock<IBillbeeRestClient>();
        restClientMock
            .Setup(x => x.Delete(It.IsAny<string>(), It.IsAny<NameValueCollection>(), It.IsAny<ParameterType>()));
        var uut = new WebhookEndPoint(restClientMock.Object);

        uut.DeleteAllWebhooks();
        
        restClientMock.Verify(x => x.Delete("/webhooks", null, ParameterType.QueryString));
    }
    
    [TestMethod]
    public void Webhook_DeleteWebhook_Test()
    {
        var testWebHook = CreateTestWebHook();
        var restClientMock = new Mock<IBillbeeRestClient>();
        restClientMock
            .Setup(x => x.Delete(It.IsAny<string>(), It.IsAny<NameValueCollection>(), It.IsAny<ParameterType>()));
        var uut = new WebhookEndPoint(restClientMock.Object);

        uut.DeleteWebhook(testWebHook.Id);
        
        restClientMock.Verify(x => x.Delete($"/webhooks/{testWebHook.Id}", null, ParameterType.QueryString));
    }

    [TestMethod]
    public void Webhook_UpdateWebhook_Test()
    {
        var testWebHook = CreateTestWebHook();
        var restClientMock = new Mock<IBillbeeRestClient>();
        restClientMock
            .Setup(x => x.Put(It.IsAny<string>(), It.IsAny<Webhook>(), It.IsAny<NameValueCollection>()));
        var uut = new WebhookEndPoint(restClientMock.Object);

        uut.UpdateWebhook(testWebHook);
        
        restClientMock.Verify(x => x.Put($"/webhooks/{testWebHook.Id}", testWebHook, null));
    }

    [TestMethod]
    public void Webhook_CreateWebhook_Test()
    {
        var testWebHook = CreateTestWebHook();
        testWebHook.Id = "4711";
        var restClientMock = new Mock<IBillbeeRestClient>();
        restClientMock
            .Setup(x => x.Post(It.IsAny<string>(), It.IsAny<Webhook>()));
        var uut = new WebhookEndPoint(restClientMock.Object);

        Assert.ThrowsException<InvalidValueException>(() => uut.CreateWebhook(testWebHook));

        testWebHook.Id = null;
        testWebHook.Secret = "theSecret";
        Assert.ThrowsException<InvalidValueException>(() => uut.CreateWebhook(testWebHook));

        testWebHook.Secret = "0123456789012345678901234567890123456789";
        uut.CreateWebhook(testWebHook);
        restClientMock.Verify(x => x.Post<Webhook>("/webhooks", testWebHook, null));
    }
}