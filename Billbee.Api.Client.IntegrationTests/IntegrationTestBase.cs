using System;

namespace Billbee.Api.Client.IntegrationTests
{
    public abstract class IntegrationTestBase
    {
        protected IApiClient _apiClient;

        public IntegrationTestBase()
        {
            _apiClient = new ApiClient("settings.json");

            if (string.IsNullOrWhiteSpace(_apiClient.Configuration.Username) ||
                string.IsNullOrWhiteSpace(_apiClient.Configuration.Username) ||
                string.IsNullOrWhiteSpace(_apiClient.Configuration.Password) ||
                string.IsNullOrWhiteSpace(_apiClient.Configuration.ApiKey))
            {
                throw new InvalidOperationException("Please adjust the configuration file `settings.json` before test execution.");
            }
        }
    }
}