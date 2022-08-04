using Billbee.Api.Client.Endpoint.Interfaces;
using Billbee.Api.Client.Enums;
using Billbee.Api.Client.Model;
using Billbee.Api.Client.Test.EndPointIntegrationTests.Helpers;

namespace Billbee.Api.Client.Test.EndPointIntegrationTests;

[TestClass]
public class EnumEndPointIntegrationTest
{
    public TestContext TestContext { get; set; }
    
    [TestInitialize]
    public void TestInitialize()
    {
        IntegrationTestHelpers.CheckAccess(TestContext.ManagedType, TestContext.ManagedMethod);   
    }
    
    [TestMethod]
    [RequiresApiAccess]
    public void GetPaymentTypes_IntegrationTest()
    {
        ExecuteEnumSyncTest<PaymentTypeEnum>(x => x.GetPaymentTypes());
    }
    
    [TestMethod]
    [RequiresApiAccess]
    public void GetShippingCarriers_IntegrationTest()
    {
        ExecuteEnumSyncTest<ShippingCarrierEnum>(x => x.GetShippingCarriers());
    }
    
    [TestMethod]
    [RequiresApiAccess]
    public void GetShipmentTypes_IntegrationTest()
    {
        ExecuteEnumSyncTest<ShipmentTypeEnum>(x => x.GetShipmentTypes());
    }
    
    [TestMethod]
    [RequiresApiAccess]
    public void GetOrderStates_IntegrationTest()
    {
        ExecuteEnumSyncTest<OrderStateEnum>(x => x.GetOrderStates());
    }

    private void ExecuteEnumSyncTest<T>(Func<IEnumEndPoint, List<EnumEntry>> endpointFunc) where T: struct, System.Enum
    {
        var apiEnumEntries = endpointFunc(IntegrationTestHelpers.ApiClient.Enums);
            
        // enum entries in api, but not in sdk
        var missingSdkPaymentTypes = _getMissingSdkEnumEntries<T>(apiEnumEntries);
        
        // enum entries in api and sdk, but with wrong number in sdk
        var sdkEnumEntriesWithWrongNumber = _getSdkEnumEntriesWithWrongNumber<T>(apiEnumEntries);

        // enum entries in sdk, but not in api
        var deprecatedSdkPaymentTypes = _getDeprecatedSdkEnumEntries<T>(apiEnumEntries);
        
        Assert.AreEqual(0, missingSdkPaymentTypes.Count);
        Assert.AreEqual(0, sdkEnumEntriesWithWrongNumber.Count);
        Assert.AreEqual(0, deprecatedSdkPaymentTypes.Count());
    }
    
    private List<EnumEntry> _getMissingSdkEnumEntries<T>(List<EnumEntry> apiEnumEntries) where T: struct, System.Enum
    {
        var result =  apiEnumEntries
            .Where(apiEnumEntry => !Enum.TryParse(apiEnumEntry.Name, out T _))
            .OrderBy(x => x.Id)
            .ToList();

        if (result.Count > 0)
        {
            Console.WriteLine("Missing sdk enum entries:");
            foreach (var enumEntry in result)
            {
                Console.WriteLine($"{enumEntry.Name}: {enumEntry.Id}");
            }
        }
        
        return result;
    }

    private Dictionary<T, (int, int)> _getSdkEnumEntriesWithWrongNumber<T>(List<EnumEntry> apiEnumEntries) where T: struct, System.Enum
    {
        var result = apiEnumEntries
            .Where(apiEnumEntry => Enum.TryParse<T>(apiEnumEntry.Name, out T sdkEnumEntry))
            .Select(apiEnumEntry =>
            {
                Enum.TryParse<T>(apiEnumEntry.Name, out T sdkEnumEntry);
                return new KeyValuePair<T, (int, int)>(sdkEnumEntry, (apiEnumEntry.Id, Convert.ToInt32(sdkEnumEntry)));
            })
            .Where(x => x.Value.Item1 != x.Value.Item2)
            .OrderBy(x => x.Value.Item1)
            .ToDictionary(x => x.Key, x => x.Value);

        
        if (result.Count > 0)
        {
            Console.WriteLine("Sdk enum entries with wrong number:");
            foreach (var pair in result)
            {
                Console.WriteLine($"{pair.Key}: expected:{pair.Value.Item1}, actual:{pair.Value.Item2}");
            }
        }
        
        return result;
    }

    private IEnumerable<T> _getDeprecatedSdkEnumEntries<T>(List<EnumEntry> apiEnumEntries) where T: struct, System.Enum
    {
        var result = new List<T>();
        foreach (var sdkEnumEntry in Enum.GetValues<T>())
        {
            var apiEnumEntry = apiEnumEntries.FirstOrDefault(x => x.Id == Convert.ToInt32(sdkEnumEntry));
            if (apiEnumEntry == null)
            {
                result.Add(sdkEnumEntry);
            }
        }

        if (result.Count > 0)
        {
            Console.WriteLine("Deprecated sdk enum entries:");
            foreach (var enumEntry in result)
            {
                Console.WriteLine($"{enumEntry}: {Convert.ToInt32(enumEntry)}");
            }
        }
        
        return result;
    }
}