using System.Linq.Expressions;
using Billbee.Api.Client.EndPoint;
using Billbee.Api.Client.Endpoint.Interfaces;
using Billbee.Api.Client.Enums;
using Billbee.Api.Client.Model;

namespace Billbee.Api.Client.Test.EndPointTests;

[TestClass]
public class EnumEndPointTest
{
    [TestMethod]
    public void GetPaymentTypesTest()
    {
        ExecuteEnumTest("paymenttypes", x => x.GetPaymentTypes());
    }

    [TestMethod]
    public void GetShippingCarriersTest()
    {
        ExecuteEnumTest("shippingcarriers", x => x.GetShippingCarriers());
    }
    
    [TestMethod]
    public void GetShipmentTypesTest()
    {
        ExecuteEnumTest("shipmenttypes", x => x.GetShipmentTypes());
    }

    [TestMethod]
    public void GetOrderStatesTest()
    {
        ExecuteEnumTest("orderstates", x => x.GetOrderStates());
    }
    
    private void ExecuteEnumTest(string endpoint, Func<IEnumEndPoint, List<EnumEntry>> endpointFunc)
    {
        var testEnumEntryList = new List<EnumEntry>
        {
            new() { Id = 1, Name = "entry1" },
            new() { Id = 2, Name = "entry2" },
        };

        Expression<Func<IBillbeeRestClient, object>> expression = x =>
            x.Get<List<EnumEntry>>($"/enums/{endpoint}", null);
        object mockResult = testEnumEntryList;
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new EnumEndPoint(restClient);
            var result = endpointFunc(uut);
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        });
    }
}