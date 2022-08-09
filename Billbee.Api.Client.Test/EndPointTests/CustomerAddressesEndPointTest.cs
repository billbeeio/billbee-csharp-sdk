using System.Collections.Specialized;
using System.Linq.Expressions;
using Billbee.Api.Client.EndPoint;
using Billbee.Api.Client.Model;

namespace Billbee.Api.Client.Test.EndPointTests;

[TestClass]
public class CustomerAddressesEndPointTest
{
    [TestMethod]
    public void CustomerAddresses_GetCustomerAddresses_Test()
    {
        var testCustomerAddress = new CustomerAddress { Id = 4711 };
        var page = 1;
        var pageSize = 5;
        NameValueCollection parameters = new NameValueCollection
        {
            { "page", page.ToString() },
            { "pageSize", pageSize.ToString() }
        };
        
        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Get<ApiPagedResult<List<CustomerAddress>>>($"/customer-addresses", parameters);
        object mockResult = TestHelpers.GetApiPagedResult(new List<CustomerAddress> { testCustomerAddress });
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new CustomerAddressesEndPoint(restClient);
            var result = uut.GetCustomerAddresses(page, pageSize);
            Assert.IsNotNull(result.Data);
        });
    }   
    
    [TestMethod]
    public void CustomerAddresses_GetCustomerAddress_Test()
    {
        var testCustomerAddress = new CustomerAddress { Id = 4711 };
        
        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Get<ApiResult<CustomerAddress>>($"/customer-addresses/{testCustomerAddress.Id}", null);
        object mockResult = TestHelpers.GetApiResult(testCustomerAddress);
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new CustomerAddressesEndPoint(restClient);
            var result = uut.GetCustomerAddress(testCustomerAddress.Id.Value);
            Assert.IsNotNull(result.Data);
        });
    }
    
    [TestMethod]
    public void CustomerAddresses_AddCustomerAddress_Test()
    {
        var testCustomerAddress = new CustomerAddress { Id = 4711 };
        
        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Post<ApiResult<CustomerAddress>>($"/customer-addresses", testCustomerAddress, null);
        object mockResult = TestHelpers.GetApiResult(testCustomerAddress);
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new CustomerAddressesEndPoint(restClient);
            var result = uut.AddCustomerAddress(testCustomerAddress);
            Assert.IsNotNull(result.Data);
        });
    }
    
    [TestMethod]
    public void CustomerAddresses_UpdateCustomerAddress_Test()
    {
        var testCustomerAddress = new CustomerAddress { Id = 4711 };
        
        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Put<ApiResult<CustomerAddress>>($"/customer-addresses/{testCustomerAddress.Id}", testCustomerAddress, null);
        object mockResult = TestHelpers.GetApiResult(testCustomerAddress);
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new CustomerAddressesEndPoint(restClient);
            
            testCustomerAddress.Id = null;
            Assert.ThrowsException<InvalidValueException>(() => uut.UpdateCustomerAddress(testCustomerAddress));
            
            testCustomerAddress.Id = 4711;
            var result = uut.UpdateCustomerAddress(testCustomerAddress);
            Assert.IsNotNull(result.Data);
        });
    }
}