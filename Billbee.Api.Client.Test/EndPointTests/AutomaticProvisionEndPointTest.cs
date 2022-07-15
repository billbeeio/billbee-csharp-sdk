using System.Collections.Specialized;
using System.Linq.Expressions;
using Billbee.Api.Client.EndPoint;
using Billbee.Api.Client.Model;
using Moq;

namespace Billbee.Api.Client.Test.EndPointTests;

[TestClass]
public class AutomaticProvisionEndPointTest
{
    [TestMethod]
    public void CreateAccountTest()
    {
        var testAccount = new Account();
        var testCreateUserResult = new CreateUserResult
        {
            UserId = new Guid("cf65821e-6549-4944-be0b-a71112cf50a9"),
            Password = "thePwd",
            OneTimeLoginUrl = "theOneTimeLoginUrl"
        };

        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Post<ApiResult<CreateUserResult>>("/automaticprovision/createaccount", testAccount, null);
        var mockResult = TestHelpers.GetApiResult(testCreateUserResult);
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new AutomaticProvisionEndPoint(restClient);
            var result = uut.CreateAccount(testAccount);
            Assert.IsNotNull(result.Data);
        });
    }

    [TestMethod]
    public void TermsInfoTest()
    {
        var testTermsResult = new TermsResult();
        
        Expression<Func<IBillbeeRestClient, object>> expression = x => x.Get<ApiResult<TermsResult>>("/automaticprovision/termsinfo", null);
        object mockResult = TestHelpers.GetApiResult(testTermsResult);
        TestHelpers.RestClientMockTest(expression, mockResult, (restClient) =>
        {
            var uut = new AutomaticProvisionEndPoint(restClient);
            var result = uut.TermsInfo();
            Assert.IsNotNull(result.Data);
        });
    }
}