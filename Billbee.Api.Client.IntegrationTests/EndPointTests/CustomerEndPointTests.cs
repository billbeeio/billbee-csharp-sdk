using Billbee.Api.Client.Model;
using System;
using Xunit;

namespace Billbee.Api.Client.IntegrationTests.EndPointTests
{
    [Priority(0)]
    [TestCaseOrderer(PriorityTestCaseOrderer.TypeName, PriorityTestCaseOrderer.AssemblyName)]
    public class CustomerEndPointTests : IntegrationTestBase
    {
        private static long _customerId;
        private static CustomerForCreation? _customer;

        [SkippableFact(DisplayName = "1. Can create a random customer"), Priority(0)]
        public void CanCreateCustomer()
        {
            var randomCustomer = CreateRandomCustomer();
            var addCustomerResult = _apiClient.Customer.AddCustomer(randomCustomer);
            Assert.True(addCustomerResult.Success);
            Assert.NotNull(addCustomerResult.Data);
            Assert.True(addCustomerResult.Data.Id > 0);
            Assert_CustomerEqual(randomCustomer, addCustomerResult.Data);
            _customerId = addCustomerResult.Data.Id!.Value;
            _customer = randomCustomer;
        }

        [SkippableFact(DisplayName = "2. Can get the created customer"), Priority(1)]
        public void CanGetCustomer()
        {
            Skip.IfNot(_customerId > 0);
            Skip.If(_customer is null);

            var getCustomerResult = _apiClient.Customer.GetCustomer(_customerId);
            Assert.True(getCustomerResult.Success);
            Assert.NotNull(getCustomerResult.Data);
            Assert.True(getCustomerResult.Data.Id > 0);
            Assert_CustomerEqual(_customer, getCustomerResult.Data);
            _customerId = getCustomerResult.Data.Id!.Value;
        }

        private static CustomerForCreation CreateRandomCustomer()
        {
            var random = new Random((int)DateTime.Now.Ticks);
            return new CustomerForCreation
            {
                Name = "Test " + Random(),
                Email = Random() + "@test.de",
                Tel1 = "0" + random.Next(1000000, 9000000),
                Tel2 = "0" + random.Next(1000000, 9000000),
                Type = 0,
                Address = new CustomerAddress
                {
                    AddressType = 1,
                    CustomerId = 0,
                    Company = "Test Company " + Random(),
                    FirstName = "Test FirstName " + Random(),
                    LastName = "Test LastName " + Random(),
                    Name2 = "Test Name2 " + Random(),
                    Street = "Test Street " + Random(),
                    Housenumber = random.Next(1, 999).ToString(),
                    Zip = "44263",
                    City = "Test City " + Random(),
                    State = "Test State " + Random(),
                    CountryCode = "DE",
                    Email = Random() + "@test.invalid",
                    Tel1 = "0" + random.Next(1000000, 9000000),
                    Tel2 = "0" + random.Next(1000000, 9000000),
                    Fax = "0" + random.Next(1000000, 9000000),
                    AddressAddition = "Test AddressAddition " + Random()
                }
            };
        }

        private static void Assert_CustomerEqual(Customer expected, Customer actual)
        {
            Assert.Equal(expected.Type, actual.Type);
            Assert.True(string.Equals(expected.Name, actual.Name, StringComparison.InvariantCultureIgnoreCase));
            Assert.True(string.Equals(expected.Email, actual.Email, StringComparison.InvariantCultureIgnoreCase));
            Assert.True(string.Equals(expected.Tel1, actual.Tel1, StringComparison.InvariantCultureIgnoreCase));
            Assert.True(string.Equals(expected.Tel2, actual.Tel2, StringComparison.InvariantCultureIgnoreCase));
        }

        private static string Random()
        {
            return Guid.NewGuid().ToString()[24..];
        }
    }
}
