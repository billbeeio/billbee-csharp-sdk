using Billbee.Api.Client.Model;
using Billbee.Api.Client.Test.EndPointIntegrationTests.Helpers;

namespace Billbee.Api.Client.Test.EndPointIntegrationTests.Helpers
{
    public static partial class TestData
    {
        public static CustomerAddress CustomerAddress =>
            new CustomerAddress
            {
                FirstName = "John",
                LastName = "Doe",
                Street = "Mustergasse",
                Housenumber = "1",
                Zip = "12345",
                City = "Musterstadt",
                AddressType = 1,
                CountryCode = "DE",
                CustomerId = 0
            };
        
        public static CustomerAddress GetCustomerAddress(long? customerId)
        {
            var address = CustomerAddress;
            address.CustomerId = customerId;
            return address;
        }
    }
}

namespace Billbee.Api.Client.Test.EndPointIntegrationTests
{
    [TestClass]
    public class CustomerAddressesEndPointIntegrationTest
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            IntegrationTestHelpers.CheckAccess(TestContext.ManagedType, TestContext.ManagedMethod);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void GetCustomerAddresses_IntegrationTest()
        {
            CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.CustomerAddresses.GetCustomerAddresses(1, 5));
        }

        [TestMethod]
        [RequiresApiAccess]
        public void GetCustomerAddress_IntegrationTest()
        {
            var customer =
                CrudHelpers.CreateApiResult(c => IntegrationTestHelpers.ApiClient.Customer.AddCustomer(c),
                    TestData.Customer).Data;

            var customerAddress =
                CrudHelpers.CreateApiResult(
                    a => IntegrationTestHelpers.ApiClient.CustomerAddresses.AddCustomerAddress(a),
                    TestData.GetCustomerAddress(customer.Id)).Data;
            CrudHelpers.GetOneApiResult<CustomerAddress>(
                (id) => IntegrationTestHelpers.ApiClient.CustomerAddresses.GetCustomerAddress(id),
                customerAddress.Id!.Value);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void AddCustomerAddress_IntegrationTest()
        {
            GetCustomerAddress_IntegrationTest();
        }

        [TestMethod]
        [RequiresApiAccess]
        public void UpdateCustomerAddress_IntegrationTest()
        {
            var customer =
                CrudHelpers.CreateApiResult(c => IntegrationTestHelpers.ApiClient.Customer.AddCustomer(c),
                    TestData.Customer).Data;

            var customerAddress =
                CrudHelpers.CreateApiResult(
                    a => IntegrationTestHelpers.ApiClient.CustomerAddresses.AddCustomerAddress(a),
                    TestData.GetCustomerAddress(customer.Id)).Data;
            var result = CrudHelpers.GetOneApiResult<CustomerAddress>(
                (id) => IntegrationTestHelpers.ApiClient.CustomerAddresses.GetCustomerAddress(id),
                customerAddress.Id!.Value);
            var address = result.Data;

            address.LastName = "Modified";
            CrudHelpers.Put<CustomerAddress>(
                c => IntegrationTestHelpers.ApiClient.CustomerAddresses.UpdateCustomerAddress(c),
                address);
            CrudHelpers.GetOneApiResult<CustomerAddress>(
                (id) => IntegrationTestHelpers.ApiClient.CustomerAddresses.GetCustomerAddress(id),
                customerAddress.Id!.Value);
            Assert.AreEqual("Modified", address.LastName);
        }
    }
}