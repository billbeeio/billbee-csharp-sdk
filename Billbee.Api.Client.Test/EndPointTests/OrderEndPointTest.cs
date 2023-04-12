using System.Collections.Specialized;
using System.Linq.Expressions;
using Billbee.Api.Client.EndPoint;
using Billbee.Api.Client.Enums;
using Billbee.Api.Client.Model;
using Moq;
using RestSharp;

namespace Billbee.Api.Client.Test.EndPointTests;

[TestClass]
[TestCategory(TestCategories.EndpointTests)]
public class OrderEndPointTest
{
    [TestMethod]
    public void Order_GetOrder_Test()
    {
        var testOrder = new Order();
        var id = "4711";
        var articleTitleSource = 0;
        
        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Get<ApiResult<Order>>($"/orders/{id}?articleTitleSource={articleTitleSource}", null);
        object mockResult = TestHelpers.GetApiResult(testOrder);
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new OrderEndPoint(restClient);
            var result = uut.GetOrder(id);
            Assert.IsNotNull(result.Data);
        }); 
    }

    [TestMethod]
    public void Order_GetPatchableFields_Test()
    {
        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Get<ApiResult<List<string>>>($"/orders/PatchableFields", null);
        object mockResult = TestHelpers.GetApiResult( new List<string> { "SellerComment", "Foo" });
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new OrderEndPoint(restClient);
            var result = uut.GetPatchableFields();
            Assert.IsNotNull(result.Data);
        }); 
    }
    
    [TestMethod]
    public void Order_PatchOrder_Test()
    {
        var orderId = 4711;
        var fieldsToPatch = new Dictionary<string, object>
        {
            { "SellerComment", "my comment..." }
        };
            
        Expression<Func<IBillbeeRestClient, ApiResult<Order>>> expression = x => x.Patch<ApiResult<Order>>($"/orders/{orderId}", null, It.IsAny<object>());
        var mockResult = TestHelpers.GetApiResult(new Order());
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new OrderEndPoint(restClient);
            var result = uut.PatchOrder(orderId, fieldsToPatch);
            Assert.IsNotNull(result.Data);
        }); 
    }
    
    [TestMethod]
    public void Order_GetOrderByExternalReference_Test()
    {
        var testOrder = new Order();
        var extRef = "extRef";
        
        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Get<ApiResult<Order>>($"/orders/findbyextref/{extRef}", null);
        object mockResult = TestHelpers.GetApiResult(testOrder);
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new OrderEndPoint(restClient);
            var result = uut.GetOrderByExternalReference(extRef);
            Assert.IsNotNull(result.Data);
        });
    }
    
    [TestMethod]
    public void Order_GetOrderByExternalIdAndPartner_Test()
    {
        var testOrder = new Order();
        var partner = "thePartner";
        var extRef = "extRef";
        
        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Get<ApiResult<Order>>($"/orders/find/{extRef}/{partner}", null);
        object mockResult = TestHelpers.GetApiResult(testOrder);
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new OrderEndPoint(restClient);
            var result = uut.GetOrderByExternalIdAndPartner(partner, extRef);
            Assert.IsNotNull(result.Data);
        });
    }
    
    [TestMethod]
    public void Order_GetOrderList_Test()
    {
        var testOrder = new Order();
        DateTime? minOrderDate = DateTime.Now.AddDays(-2);
        DateTime? maxOrderDate = DateTime.Now.AddDays(-1);
        int page = 1;
        int pageSize = 50;
        List<long> shopId = new List<long> { 22, 33 };
        List<OrderStateEnum> orderStateId = new List<OrderStateEnum> { OrderStateEnum.Abgeschlossen, OrderStateEnum.Angeboten };
        List<string> tag = new List<string> { "tag1", "tag2" };
        long? minimumBillBeeOrderId = 22;
        DateTime? modifiedAtMin = DateTime.Now.AddDays(-2);
        DateTime? modifiedAtMax = DateTime.Now.AddDays(-1);
        bool excludeTags = false;
        NameValueCollection parameters = new NameValueCollection
        {
            { "minOrderDate", minOrderDate.Value.ToString("yyyy-MM-dd HH:mm") },
            { "maxOrderDate", maxOrderDate.Value.ToString("yyyy-MM-dd HH:mm") },
            { "modifiedAtMin", modifiedAtMin.Value.ToString("yyyy-MM-dd HH:mm") },
            { "modifiedAtMax", modifiedAtMax.Value.ToString("yyyy-MM-dd HH:mm") },
            { "minimumBillBeeOrderId", minimumBillBeeOrderId.ToString() }
        };
        int i = 0;
        foreach (var id in shopId)
        {
            parameters.Add($"shopId[{i++}]", id.ToString());
        }
        i = 0;
        foreach (var id in tag)
        {
            parameters.Add($"tag[{i++}]", id);
        }
        i = 0;
        foreach (var id in orderStateId)
        {
            parameters.Add($"orderStateId[{i++}]", ((int)id).ToString());
        }
        parameters.Add("page", page.ToString());
        parameters.Add("pageSize", pageSize.ToString());
        parameters.Add("excludeTags", excludeTags.ToString());
        
        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Get<ApiPagedResult<List<Order>>>($"/orders", parameters);
        object mockResult = TestHelpers.GetApiPagedResult(new List<Order> { testOrder });
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new OrderEndPoint(restClient);
            var result = uut.GetOrderList(minOrderDate, maxOrderDate, page, pageSize, shopId, orderStateId, tag, minimumBillBeeOrderId, modifiedAtMin, modifiedAtMax, excludeTags);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(1, result.Data.Count);
        });
    }
    
    [TestMethod]
    public void Order_GetInvoiceList_Test()
    {
        var testInvoiceDetail = new InvoiceDetail();
        DateTime? minInvoiceDate = DateTime.Now.AddDays(-2);
        DateTime? maxInvoiceDate = DateTime.Now.AddDays(-1);
        int page = 1; 
        int pageSize = 50;
        List<long> shopId = new List<long> { 22, 33 };
        List<int> orderStateId = new List<int> { 1, 2 };
        List<string> tag = new List<string> { "tag1", "tag2" };
        DateTime? minPayDate = DateTime.Now.AddDays(-2);
        DateTime? maxPayDate = DateTime.Now.AddDays(-1);
        bool includePositions = true;
        bool excludeTags = false;
        NameValueCollection parameters = new NameValueCollection
        {
            { "minInvoiceDate", minInvoiceDate.Value.ToString("yyyy-MM-dd HH:mm") },
            { "maxInvoiceDate", maxInvoiceDate.Value.ToString("yyyy-MM-dd HH:mm") },
            { "minPayDate", minPayDate.Value.ToString("yyyy-MM-dd HH:mm") },
            { "maxPayDate", maxPayDate.Value.ToString("yyyy-MM-dd HH:mm") }
        };
        int i = 0;
        foreach (var id in shopId)
        {
            parameters.Add($"shopId[{i++}]", id.ToString());
        }
        i = 0;
        foreach (var id in tag)
        {
            parameters.Add($"tag[{i++}]", id);
        }
        i = 0;
        foreach (var id in orderStateId)
        {
            parameters.Add($"orderStateId[{i++}]", id.ToString());
        }
        parameters.Add("includePositions", includePositions.ToString());
        parameters.Add("page", page.ToString());
        parameters.Add("pageSize", pageSize.ToString());
        parameters.Add("excludeTags", excludeTags.ToString());
        
        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Get<ApiResult<List<InvoiceDetail>>>($"/orders/invoices", parameters);
        object mockResult = TestHelpers.GetApiResult(new List<InvoiceDetail> { testInvoiceDetail });
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new OrderEndPoint(restClient);
            var result = uut.GetInvoiceList(minInvoiceDate, maxInvoiceDate, page, pageSize, shopId, orderStateId, tag, minPayDate, maxPayDate, includePositions, excludeTags);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(1, result.Data.Count);
        });
    }
    
     
    [TestMethod]
    public void Order_PostNewOrder_Test()
    {
        var testOrder = new Order();
        var testOrderResult = new OrderResult();
        var shopId = 1234;
        NameValueCollection parameters = new NameValueCollection { { "shopId", shopId.ToString() } };

        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Post<ApiResult<OrderResult>>($"/orders", testOrder, parameters);
        object mockResult = TestHelpers.GetApiResult(testOrderResult);
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new OrderEndPoint(restClient);
            var result = uut.PostNewOrder(testOrder, shopId);
            Assert.IsNotNull(result.Data);
        });
    }
    
    [TestMethod]
    public void Order_AddTags_Test()
    {
        var orderId = 4711;
        var tags = new List<string> { "tag1", "tag2" };
        
        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Post<ApiResult<dynamic>>($"/orders/{orderId}/tags", It.IsAny<object>(), null);
        object mockResult = TestHelpers.GetApiResult(new object());
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new OrderEndPoint(restClient);
            var result = uut.AddTags(tags, orderId);
            Assert.IsNotNull(result.Data);
        });
    }
    
    [TestMethod]
    public void Order_UpdateTags_Test()
    {
        var orderId = 4711;
        var tags = new List<string> { "tag1", "tag2" };
        
        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Put<ApiResult<dynamic>>($"/orders/{orderId}/tags", It.IsAny<object>(), null);
        object mockResult = TestHelpers.GetApiResult(new object());
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new OrderEndPoint(restClient);
            var result = uut.UpdateTags(tags, orderId);
            Assert.IsNotNull(result.Data);
        });
    }
    
    [TestMethod]
    public void Order_AddShipment_Test()
    {
        var testOrderShipment = new OrderShipment();
        
        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Post($"/orders/{testOrderShipment.OrderId}/shipment", testOrderShipment);
        object mockResult = null!;
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new OrderEndPoint(restClient);
            uut.AddShipment(testOrderShipment);
        });
    }
    
    [TestMethod]
    public void Order_CreateDeliveryNote_Test()
    {
        var orderId = 4711;
        var includePdf = true;
        var sendToCloudId = 1234;
        NameValueCollection parameters = new NameValueCollection
        {
            { "includePdf", includePdf.ToString() },
            { "sendToCloudId", sendToCloudId.ToString() }
        };
        
        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Post<ApiResult<DeliveryNote>>($"/orders/CreateDeliveryNote/{orderId}", parameters, null, ParameterType.QueryString);
        object mockResult = TestHelpers.GetApiResult(new DeliveryNote());
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new OrderEndPoint(restClient);
            var result = uut.CreateDeliveryNote(orderId, includePdf, sendToCloudId);
            Assert.IsNotNull(result.Data);
        });
    }
    
    [TestMethod]
    public void Order_CreateInvoice_Test()
    {
        var orderId = 4711;
        var includePdf = true;
        var templateId = 1111;
        var sendToCloudId = 1234; NameValueCollection parameters = new NameValueCollection
        {
            { "includeInvoicePdf", includePdf.ToString() },
            { "sendToCloudId", sendToCloudId.ToString() },
            { "templateId", templateId.ToString() }
        };
        
        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Post<ApiResult<Invoice>>($"/orders/CreateInvoice/{orderId}", parameters, null, ParameterType.QueryString);
        object mockResult = TestHelpers.GetApiResult(new Invoice());
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new OrderEndPoint(restClient);
            var result = uut.CreateInvoice(orderId, includePdf, templateId, sendToCloudId);
            Assert.IsNotNull(result.Data);
        });
    }
    
    [TestMethod]
    public void Order_ChangeOrderState_Test()
    {
        var orderId = 4711;
        
        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Put<object>($"/orders/{orderId}/orderstate", It.IsAny<object>(), null);
        object mockResult = TestHelpers.GetApiResult(new Invoice());
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new OrderEndPoint(restClient);
            uut.ChangeOrderState(orderId, OrderStateEnum.Abgeschlossen);
        });
    }
   
    [TestMethod]
    public void Order_SendMailForOrder_Test()
    {
        var testSendMessage = new SendMessage();
        var orderId = 4711;
        
        var restClientMock = new Mock<IBillbeeRestClient>();
        var uut = new OrderEndPoint(restClientMock.Object);
        uut.SendMailForOrder(orderId, testSendMessage);
        
        restClientMock.Verify(x => x.Post($"/orders/{orderId}/send-message", testSendMessage));
    }
    
    [TestMethod]
    public void Order_CreateEventAtOrder_Test()
    {
        var restClientMock = new Mock<IBillbeeRestClient>();
        var uut = new OrderEndPoint(restClientMock.Object);

        var orderId = 4711;
        var eventName = "theEventName";
        uint delayInMinutes = 1;
        uut.CreateEventAtOrder(orderId, eventName, delayInMinutes);
       
        restClientMock.Verify(x => x.Post($"/orders/{orderId}/trigger-event", It.IsAny<TriggerEventContainer>()));
    }
    
    [TestMethod]
    public void Order_GetLayouts_Test()
    {
        var testLayoutList = new List<LayoutTemplate>
        {
            new() { Id = 1, Name = "layout1", Type = ReportTemplates.Invoice },
            new() { Id = 2, Name = "layout2", Type = ReportTemplates.Label },
        };
        
        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Get<ApiResult<List<LayoutTemplate>>>($"/layouts", null);
        object mockResult = TestHelpers.GetApiResult(testLayoutList);
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new OrderEndPoint(restClient);
            var result = uut.GetLayouts();
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(2, result.Data.Count);
        });
    }
    
    [TestMethod]
    public void Order_ParsePlaceholders_Test()
    {
        var restClientMock = new Mock<IBillbeeRestClient>();
        var uut = new OrderEndPoint(restClientMock.Object);
        
        var orderId = 4711;
        var parsePlaceholdersQuery = new ParsePlaceholdersQuery
        {
            TextToParse = "This is my text for Order {OrderNumber}"
        };

        uut.ParsePlaceholders(orderId, parsePlaceholdersQuery);
        
        restClientMock.Verify(x => x.Post<ParsePlaceholdersResult>($"/orders/{orderId}/parse-placeholders", It.IsAny<ParsePlaceholdersQuery>(), null));
    }
}