using System.Collections.Specialized;
using System.Linq.Expressions;
using Billbee.Api.Client.EndPoint;
using Billbee.Api.Client.Enums;
using Billbee.Api.Client.Model;
using Moq;

namespace Billbee.Api.Client.Test.EndPointTests;

[TestClass]
[TestCategory(TestCategories.EndpointTests)]
public class EventEndPointTest
{
    [TestMethod]
    public void Event_GetEvents_Test()
    {
        var testEvent = new Event();
        DateTime? minDate = DateTime.Now.AddDays(-2);
        DateTime? maxDate = DateTime.Now.AddDays(-1);
        int page = 1;
        int pageSize = 5;
        List<EventTypeEnum> typeIds = new List<EventTypeEnum> { EventTypeEnum.OrderImported };
        long? orderId = 4711;
        NameValueCollection parameters = new NameValueCollection
        {
            { "minDate", minDate.Value.ToString("yyyy-MM-dd HH:mm") },
            { "maxDate", maxDate.Value.ToString("yyyy-MM-dd") },
            { "page", page.ToString() },
            { "pageSize", pageSize.ToString() },
            { "orderId", orderId.ToString() }
        };
        int index = 0;
        foreach (var typeId in typeIds)
        {
            parameters.Add($"typeId[{index}]", ((int) typeId).ToString());
            index++;
        }
        
        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Get<ApiPagedResult<List<Event>>>($"/events", parameters);
        object mockResult = TestHelpers.GetApiPagedResult(new List<Event> { testEvent });
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new EventEndPoint(restClient);
            var result = uut.GetEvents(minDate, maxDate, page, pageSize, typeIds, orderId);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(1, result.Data.Count);
        }); 
    }
}