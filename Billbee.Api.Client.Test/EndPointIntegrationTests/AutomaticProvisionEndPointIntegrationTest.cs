using System.Net;
using Billbee.Api.Client.Model;
using Billbee.Api.Client.Test.EndPointIntegrationTests.Helpers;

namespace Billbee.Api.Client.Test.EndPointIntegrationTests.Helpers
{
    public static partial class TestData
    {
        public static Account Account(string name) => new Account
        {
            EMail = "john+" + name + "@test.com",
            Password = "1Ufuyk2s",
            AcceptTerms = true,
            Address = new Account.UserAddress
            {
                Company = "Test Company GmbH",
                Name = name,
                Address1 = "Paulinenstr. 4",
                Address2 = "address 2",
                Zip = "12345",
                City = "Flipstadt",
                Country = "DE",
                VatId = name + "-VatId"
            },
            AffiliateCouponCode = "affiliateCouponCode",
            Vat1Rate = 19,
            Vat2Rate = 7,
            DefaultVatMode = 0,
            DefaultCurrrency = "EUR",
            DefaultVatIndex = 0
        };
    }
}

namespace Billbee.Api.Client.Test.EndPointIntegrationTests
{
    [TestClass]
    [TestCategory(TestCategories.IntegrationTests)]
    public class AutomaticProvisionEndPointIntegrationTest
    {
#pragma warning disable CS8618
        public TestContext TestContext { get; set; }
#pragma warning restore CS8618

        [TestInitialize]
        public void TestInitialize()
        {
            IntegrationTestHelpers.CheckAccess(TestContext.ManagedType, TestContext.ManagedMethod);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void AutomaticProvision_CreateAccount_IntegrationTest()
        {
            Assert.Inconclusive();
            return;
            
            // var account = TestData.Account(Guid.NewGuid().ToString());
            // var result =
            //     CrudHelpers.CreateApiResult(x => IntegrationTestHelpers.ApiClient.AutomaticProvision.CreateAccount(x),
            //         account, false);
            // var createUserResult = result.Data;
            // Assert.IsNotNull(createUserResult.Password);
            // Assert.IsNotNull(createUserResult.UserId);
        }

        [TestMethod]
        [RequiresApiAccess]
        public async Task AutomaticProvision_TermsInfo_IntegrationTest()
        {
            var result = IntegrationTestHelpers.ApiClient.AutomaticProvision.TermsInfo();
            Assert.IsNotNull(result);

            var termsResult = result.Data;

            Assert.IsNotNull(termsResult);
            Assert.IsNotNull(termsResult.LinkToTermsWebPage);
            await _checkLink(termsResult.LinkToTermsWebPage);
            Assert.IsNotNull(termsResult.LinkToPrivacyWebPage);
            await _checkLink(termsResult.LinkToPrivacyWebPage);

            Assert.AreEqual(string.Empty, termsResult.LinkToTermsHtmlContent);
            Assert.AreEqual(string.Empty, termsResult.LinkToPrivacyHtmlContent);
        }

        private async Task _checkLink(string url)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}