using Billbee.Api.Client.Model;
using Billbee.Api.Client.Test.EndPointIntegrationTests.Helpers;

namespace Billbee.Api.Client.Test.EndPointIntegrationTests.Helpers
{
    public static partial class TestData
    {
        public static CustomerForCreation Customer => new CustomerForCreation
        {
            Name = "John Doe",
            Address = TestData.CustomerAddress,
            Email = "john@doe.com",
            Type = 0
        };
    }
}

namespace Billbee.Api.Client.Test.EndPointIntegrationTests
{
    [TestClass]
    public class CustomerEndPointIntegrationTest
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
        public void Customer_GetCustomerList_IntegrationTest()
        {
            CrudHelpers.GetAll(() => IntegrationTestHelpers.ApiClient.Customer.GetCustomerList(1, int.MaxValue));
        }

        [TestMethod]
        [RequiresApiAccess]
        public void Customer_AddCustomer_IntegrationTest()
        {
            var customer = CrudHelpers.CreateApiResult(c => IntegrationTestHelpers.ApiClient.Customer.AddCustomer(c),
                TestData.Customer);
            Assert.AreEqual("john@doe.com", customer.Data.Email);
            Assert.IsNotNull(customer.Data.Id);

            CrudHelpers.GetOneApiResult<Customer>((id) => IntegrationTestHelpers.ApiClient.Customer.GetCustomer(id),
                customer.Data.Id.Value);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void Customer_AddAddressToCustomer_IntegrationTest()
        {
            var customer = CrudHelpers.CreateApiResult(c => IntegrationTestHelpers.ApiClient.Customer.AddCustomer(c),
                TestData.Customer);

            var customerAddress = TestData.GetCustomerAddress(customer.Data.Id);
            var result =
                CrudHelpers.CreateApiResult(a => IntegrationTestHelpers.ApiClient.Customer.AddAddressToCustomer(a),
                    customerAddress);
            Assert.IsNotNull(result.Data);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void Customer_GetCustomerAddress_IntegrationTest()
        {
            var customer = CrudHelpers.CreateApiResult(c => IntegrationTestHelpers.ApiClient.Customer.AddCustomer(c),
                TestData.Customer);
            Assert.IsNotNull(customer.Data.Id);

            var address = CrudHelpers.GetAll(() =>
                    IntegrationTestHelpers.ApiClient.Customer.GetAddressesForCustomer(customer.Data.Id.Value, 1, 5))
                .Data
                .FirstOrDefault();
            Assert.IsNotNull(address);
            Assert.IsNotNull(address.Id);
            
            CrudHelpers.GetOneApiResult<CustomerAddress>(
                (id) => IntegrationTestHelpers.ApiClient.Customer.GetCustomerAddress(id), address.Id.Value);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void Customer_UpdateCustomerAddress_IntegrationTest()
        {
            var customer = CrudHelpers.CreateApiResult(c => IntegrationTestHelpers.ApiClient.Customer.AddCustomer(c),
                TestData.Customer);
            Assert.IsNotNull(customer.Data.Id);

            var address = CrudHelpers.GetAll(() =>
                    IntegrationTestHelpers.ApiClient.Customer.GetAddressesForCustomer(customer.Data.Id.Value, 1, 5))
                .Data
                .FirstOrDefault();
            Assert.IsNotNull(address);            
            Assert.AreEqual(TestData.CustomerAddress.FirstName, address.FirstName);

            address.FirstName = "Modified";
            var result = CrudHelpers.Put<CustomerAddress>(
                c => IntegrationTestHelpers.ApiClient.Customer.UpdateCustomerAddress(c),
                address);

            Assert.AreEqual("Modified", result.Data.FirstName);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void Customer_PatchCustomerAddress_IntegrationTest()
        {
            var customer = CrudHelpers.CreateApiResult(c => IntegrationTestHelpers.ApiClient.Customer.AddCustomer(c),
                TestData.Customer);
            Assert.IsNotNull(customer.Data.Id);

            var address = CrudHelpers.GetAll(() =>
                    IntegrationTestHelpers.ApiClient.Customer.GetAddressesForCustomer(customer.Data.Id.Value, 1, 5))
                .Data
                .FirstOrDefault();
            Assert.IsNotNull(address);
            Assert.IsNotNull(address.Id);
            Assert.AreEqual(TestData.CustomerAddress.FirstName, address.FirstName);

            var fieldsToPatch = new Dictionary<string, string>
            {
                { "FirstName", "Modified" }
            };
            var result = CrudHelpers.Patch<CustomerAddress>(
                (id, fields) => IntegrationTestHelpers.ApiClient.Customer.PatchCustomerAddress(id, fields),
                address.Id.Value, fieldsToPatch);

            Assert.AreEqual("Modified", result.Data.FirstName);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void Customer_GetCustomer_IntegrationTest()
        {
            var customer = CrudHelpers.CreateApiResult(c => IntegrationTestHelpers.ApiClient.Customer.AddCustomer(c),
                TestData.Customer);
            Assert.IsNotNull(customer.Data.Id);

            CrudHelpers.GetOneApiResult<Customer>((id) => IntegrationTestHelpers.ApiClient.Customer.GetCustomer(id),
                customer.Data.Id.Value);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void Customer_UpdateCustomer_IntegrationTest()
        {
            var result = CrudHelpers.CreateApiResult(c => IntegrationTestHelpers.ApiClient.Customer.AddCustomer(c),
                TestData.Customer);
            Assert.IsNotNull(result.Data.Id);
            
            result = CrudHelpers.GetOneApiResult<Customer>(
                (id) => IntegrationTestHelpers.ApiClient.Customer.GetCustomer(id), result.Data.Id.Value);
            var customer = result.Data;
            Assert.IsNotNull(result.Data.Id);

            Assert.AreNotEqual("Modified", customer.Name);
            customer.Name = "Modified";
            CrudHelpers.Put<Customer>((x) => IntegrationTestHelpers.ApiClient.Customer.UpdateCustomer(x), customer);

            result = CrudHelpers.GetOneApiResult<Customer>(
                (id) => IntegrationTestHelpers.ApiClient.Customer.GetCustomer(id), result.Data.Id.Value);
            Assert.AreEqual("Modified", result.Data.Name);
        }

        [TestMethod]
        [RequiresApiAccess]
        public void Customer_GetOrdersForCustomer_IntegrationTest()
        {
            var customer = CrudHelpers.CreateApiResult(c => IntegrationTestHelpers.ApiClient.Customer.AddCustomer(c),
                TestData.Customer);
            Assert.IsNotNull(customer.Data.Id);

            CrudHelpers.GetAll(() =>
                    IntegrationTestHelpers.ApiClient.Customer.GetOrdersForCustomer(customer.Data.Id.Value, 1, 5));
        }

        [TestMethod]
        [RequiresApiAccess]
        public void Customer_GetAddressesForCustomer_IntegrationTest()
        {
            var customer = CrudHelpers.CreateApiResult(c => IntegrationTestHelpers.ApiClient.Customer.AddCustomer(c),
                TestData.Customer);
            Assert.IsNotNull(customer.Data.Id);

            var result = CrudHelpers.GetAll(() =>
                IntegrationTestHelpers.ApiClient.Customer.GetAddressesForCustomer(customer.Data.Id.Value, 1, 5));
            Assert.IsTrue(result.Data.Count > 0);
        }
    }
}