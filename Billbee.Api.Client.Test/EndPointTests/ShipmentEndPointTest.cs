using System.Collections.Specialized;
using System.Linq.Expressions;
using System.Net;
using Billbee.Api.Client.EndPoint;
using Billbee.Api.Client.Model;
using Billbee.Api.Client.Model.Rechnungsdruck.WebApp.Model.Api;
using Moq;

namespace Billbee.Api.Client.Test.EndPointTests;

[TestClass]
public class ShipmentEndPointTest
{
    [TestMethod]
    public void GetShippingProviderTest()
    {
        var testShippingProvider = new ShippingProvider();
        
        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Get<List<ShippingProvider>>($"/shipment/shippingproviders", null);
        object mockResult = new List<ShippingProvider> { testShippingProvider };
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new ShipmentEndPoint(restClient);
            var result = uut.GetShippingProvider();
            Assert.AreEqual(1, result.Count);
        });
    }

    [TestMethod]
    public void GetShipmentsTest()
    {
        var testShipment = new Shipment();
        
        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Get<ApiPagedResult<List<Shipment>>>($"/shipment/shipments", It.IsAny<NameValueCollection>());
        object mockResult = TestHelpers.GetApiPagedResult(new List<Shipment> { testShipment });
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new ShipmentEndPoint(restClient);
            var result = uut.GetShipments(1, 20);
            Assert.AreEqual(1, result.Data.Count);
        });
    }
    
    [TestMethod]
    public void PostShipmentTest()
    {
        var testShipmentResult = new ShipmentResult();
        var testPostShipment = new PostShipment();
        
        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Post<ApiResult<ShipmentResult>>($"/shipment/shipment", testPostShipment, null);
        object mockResult = TestHelpers.GetApiResult(testShipmentResult);
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new ShipmentEndPoint(restClient);
            var result = uut.PostShipment(testPostShipment);
            Assert.IsNotNull(result.Data);
        });
    }
    
    [TestMethod]
    public void ShipOrderWithLabelTest()
    {
        var testShipmentWithLabel = new ShipmentWithLabel();
        var testShipmentWithLabelResult = new ShipmentWithLabelResult();
        
        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Post<ApiResult<ShipmentWithLabelResult>>($"/shipment/shipwithlabel", testShipmentWithLabel, null);
        object mockResult = TestHelpers.GetApiResult(testShipmentWithLabelResult);
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new ShipmentEndPoint(restClient);
            var result = uut.ShipOrderWithLabel(testShipmentWithLabel);
            Assert.IsNotNull(result.Data);
        });
    }
    
    [TestMethod]
    public void GetShippingCarriersTest()
    {
        var testShippingCarrier = new ShippingCarrier();
        
        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Get<List<ShippingCarrier>>($"/shipment/shippingcarriers", null);
        object mockResult = new List<ShippingCarrier> { testShippingCarrier };
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new ShipmentEndPoint(restClient);
            var result = uut.GetShippingCarriers();
            Assert.AreEqual(1, result.Count);
        });
    }

    [TestMethod]
    public void PingTest()
    {
        var restClientMock = new Mock<IBillbeeRestClient>();
        restClientMock
            .Setup(x => x.Get(It.IsAny<string>()))
            .Returns(HttpStatusCode.OK);
        
        var uut = new ShipmentEndPoint(restClientMock.Object);
        var result = uut.Ping();
        Assert.AreEqual(true, result);
        
        restClientMock
            .Setup(x => x.Get(It.IsAny<string>()))
            .Returns(HttpStatusCode.NotFound);
        result = uut.Ping();
        restClientMock.Verify(x => x.Get($"/shipment/ping"));
        Assert.AreEqual(false, result);
    }
}