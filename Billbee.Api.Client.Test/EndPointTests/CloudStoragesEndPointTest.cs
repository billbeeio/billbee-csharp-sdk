using System.Collections.Specialized;
using System.Linq.Expressions;
using Billbee.Api.Client.EndPoint;
using Billbee.Api.Client.Model;
using Moq;

namespace Billbee.Api.Client.Test.EndPointTests;

[TestClass]
public class CloudStoragesEndPointTest
{
    [TestMethod]
    public void CloudStorages_GetCloudStorageList_Test()
    {
        var testCloudStorage = new CloudStorage();
        
        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Get<ApiResult<List<CloudStorage>>>($"/cloudstorages", null);
        object mockResult = TestHelpers.GetApiResult(new List<CloudStorage> { testCloudStorage });
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new CloudStoragesEndPoint(restClient);
            var result = uut.GetCloudStorageList();
            Assert.IsNotNull(result.Data);
        });
    }
}