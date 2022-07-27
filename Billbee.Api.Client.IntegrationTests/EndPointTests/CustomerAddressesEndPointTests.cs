using Billbee.Api.Client.Model;
using System;
using System.Linq;
using Xunit;

namespace Billbee.Api.Client.IntegrationTests.EndPointTests
{
    [Priority(1)]
    [TestCaseOrderer(PriorityTestCaseOrderer.TypeName, PriorityTestCaseOrderer.AssemblyName)]
    public class CustomerAddressesEndPointTests : IntegrationTestBase
    {
        private static long _randomCustomerId;
        private static long _customerAddressId;
        private static CustomerAddress? _updatedCustomerAddress;

        [SkippableFact(DisplayName = "1. Can get a random customer"), Priority(0)]
        public void CanGetRandomCustomer()
        {
            var getRandomCustomerResult = _apiClient.Customer.GetCustomerList(0, 1);
            Assert.True(getRandomCustomerResult.Success);
            Assert.NotEmpty(getRandomCustomerResult.Data);
            var randomCustomerId = getRandomCustomerResult.Data.First().Id!.Value;
            Assert.True(randomCustomerId > 0);

            _randomCustomerId = randomCustomerId;
        }

        [SkippableFact(DisplayName = "2. Can create a customer address"), Priority(1)]
        public void CanCreateCustomerAddress()
        {
            Skip.IfNot(_randomCustomerId > 0);

            var createCustomerAddress = CreateRandomCustomerAddress(_randomCustomerId);

            var addCustomerAddressResult = _apiClient.CustomerAddresses.AddCustomerAddresses(createCustomerAddress);
            Assert.True(addCustomerAddressResult.Success);
            Assert.NotNull(addCustomerAddressResult.Data);
            Assert.True(addCustomerAddressResult.Data.Id > 0);
            Assert_CustomerAddressEqual(createCustomerAddress, addCustomerAddressResult.Data);
            _customerAddressId = addCustomerAddressResult.Data.Id!.Value;
        }

        [SkippableFact(DisplayName = "3. Can update the customer address"), Priority(2)]
        public void CanUpdateCustomerAddress()
        {
            Skip.IfNot(_customerAddressId > 0);

            var updateCustomerAddress = CreateRandomCustomerAddress(_randomCustomerId);
            updateCustomerAddress.Id = _customerAddressId;
            var updateCustomerAddressResult = _apiClient.CustomerAddresses.UpdateCustomerAddress(updateCustomerAddress);
            Assert.True(updateCustomerAddressResult.Success);
            Assert.NotNull(updateCustomerAddressResult.Data);
            Assert_CustomerAddressEqual(updateCustomerAddress, updateCustomerAddressResult.Data);

            _updatedCustomerAddress = updateCustomerAddress;
        }

        [SkippableFact(DisplayName = "4. Can get the updated customer address"), Priority(3)]
        public void CanGetUpdatedCustomerAddress()
        {
            Skip.IfNot(_customerAddressId > 0);
            Skip.If(_updatedCustomerAddress is null);

            var getCustomerAddressResult = _apiClient.CustomerAddresses.GetCustomerAddress(_customerAddressId);
            Assert.True(getCustomerAddressResult.Success);
            Assert.NotNull(getCustomerAddressResult.Data);
            Assert.Equal(_customerAddressId, getCustomerAddressResult.Data.Id);

            Assert_CustomerAddressEqual(_updatedCustomerAddress, getCustomerAddressResult.Data);
        }

        [SkippableFact(DisplayName = "5. Can get the updared customer addresses from all addresses"), Priority(4)]
        public void CanGetCustomerAddresses()
        {
            Skip.IfNot(_customerAddressId > 0);
            Skip.If(_updatedCustomerAddress is null);

            var totalPages = 2;
            for (int page = 1; page <= totalPages; page++)
            {
                var getCustomerAddressesResult = _apiClient.CustomerAddresses.GetCustomerAddresses(page, 20);
                Assert.True(getCustomerAddressesResult.Success);
                Assert.NotNull(getCustomerAddressesResult.Data);
                Assert.NotEmpty(getCustomerAddressesResult.Data);

                var address = getCustomerAddressesResult.Data.FirstOrDefault(x => x.Id == _customerAddressId);
                if (address is not null)
                {
                    Assert_CustomerAddressEqual(_updatedCustomerAddress, address);
                    return;
                }

                totalPages = getCustomerAddressesResult.Paging.TotalPages;
            }

            throw new InvalidOperationException("Could not find created customer address in all addresses");
        }

        internal static CustomerAddress CreateRandomCustomerAddress(long randomCustomerId)
        {
            var random = new Random((int)DateTime.Now.Ticks);
            return new CustomerAddress
            {
                AddressType = 1,
                CustomerId = randomCustomerId,
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
            };
        }

        private static void Assert_CustomerAddressEqual(CustomerAddress expected, CustomerAddress actual)
        {
            Assert.Equal(expected.AddressType, actual.AddressType);
            Assert.Equal(expected.CustomerId, actual.CustomerId);
            Assert.True(string.Equals(expected.Company, actual.Company, StringComparison.InvariantCultureIgnoreCase));
            Assert.True(string.Equals(expected.FirstName, actual.FirstName, StringComparison.InvariantCultureIgnoreCase));
            Assert.True(string.Equals(expected.LastName, actual.LastName, StringComparison.InvariantCultureIgnoreCase));
            Assert.True(string.Equals(expected.Name2, actual.Name2, StringComparison.InvariantCultureIgnoreCase));
            Assert.True(string.Equals(expected.Street, actual.Street, StringComparison.InvariantCultureIgnoreCase));
            Assert.True(string.Equals(expected.Housenumber, actual.Housenumber, StringComparison.InvariantCultureIgnoreCase));
            Assert.True(string.Equals(expected.Zip, actual.Zip, StringComparison.InvariantCultureIgnoreCase));
            Assert.True(string.Equals(expected.City, actual.City, StringComparison.InvariantCultureIgnoreCase));
            Assert.True(string.Equals(expected.State, actual.State, StringComparison.InvariantCultureIgnoreCase));
            Assert.True(string.Equals(expected.CountryCode, actual.CountryCode, StringComparison.InvariantCultureIgnoreCase));
            //Assert.True(string.Equals(expected.Email, actual.Email, StringComparison.InvariantCultureIgnoreCase));
            //Assert.True(string.Equals(expected.Tel1, actual.Tel1, StringComparison.InvariantCultureIgnoreCase));
            //Assert.True(string.Equals(expected.Tel2, actual.Tel2, StringComparison.InvariantCultureIgnoreCase));
            //Assert.True(string.Equals(expected.Fax, actual.Fax, StringComparison.InvariantCultureIgnoreCase));
            Assert.True(string.Equals(expected.AddressAddition, actual.AddressAddition, StringComparison.InvariantCultureIgnoreCase));
        }

        private static string Random()
        {
            return Guid.NewGuid().ToString()[24..];
        }
    }
}
