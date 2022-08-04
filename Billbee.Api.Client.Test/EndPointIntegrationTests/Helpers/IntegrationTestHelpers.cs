using System.Reflection;

namespace Billbee.Api.Client.Test.EndPointIntegrationTests.Helpers;

public static class IntegrationTestHelpers
{
    private static ApiClient? _apiClient;
    
    public static void CheckAccess(string testContextManagedType, string testContextManagedMethod)
    {
        if (!IntegrationTestSettings.RunIntegrationTests)
        {
            Assert.Inconclusive(
                $"This test is an integration-test. But integration-tests are disable currently (see property {nameof(IntegrationTestSettings)}{nameof(IntegrationTestSettings.RunIntegrationTests)}).");
            return;
        }

        var type = Assembly.GetExecutingAssembly().GetType(testContextManagedType);
        var mi = type!.GetMethod(testContextManagedMethod);
        bool requiresApiAccess = mi!.GetCustomAttributes<RequiresApiAccessAttribute>().Any();
        if (requiresApiAccess && !IntegrationTestSettings.AllowReadWriteAccessToBillbeeApi)
        {
            Assert.Inconclusive(
                $"This test is an integration-test and requires api-access. But api-access is not granted currently (see property {nameof(IntegrationTestSettings)}{nameof(IntegrationTestSettings.AllowReadWriteAccessToBillbeeApi)}).");
        }
    }
    
    public static ApiClient ApiClient 
    {
        get
        {
            if (_apiClient == null)
            {
                var fiDll = new FileInfo(Assembly.GetExecutingAssembly().Location);                
                var di = new DirectoryInfo(Path.Combine(fiDll!.Directory!.FullName, "../../../"));
                var path = Path.Combine(di.FullName, "config.prod");
                if (!File.Exists(path + ".json"))
                {
                    Assert.Fail(
                        $"This test requires api-access, but the required config-file could not be found: '{path}.json'");
                }

                _apiClient = new ApiClient(path, null, IntegrationTestSettings.AllowReadWriteAccessToBillbeeApi);
                Assert.IsTrue(_apiClient.TestConfiguration());
            }
            
            Thread.Sleep(500); // throttle api-calls when executing multiple tests
            
            return _apiClient;    
        }
    }
}