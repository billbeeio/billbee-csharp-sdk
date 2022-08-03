namespace Billbee.Api.Client.Test.EndPointIntegrationTests.Helpers;

public static class IntegrationTestSettings
{
    public static bool RunIntegrationTests = false;
    
    // !!! be careful: setting the following property to 'true' will potentially add/change/delete parts your billbee data !!!
    public static bool AllowReadWriteAccessToBillbeeApi = false;
}