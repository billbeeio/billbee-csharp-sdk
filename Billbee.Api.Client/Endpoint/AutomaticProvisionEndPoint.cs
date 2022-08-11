using System.Net.Http;
using Billbee.Api.Client.Endpoint.Interfaces;
using Billbee.Api.Client.Model;

namespace Billbee.Api.Client.EndPoint
{
    /// <inheritdoc cref="Billbee.Api.Client.Endpoint.Interfaces.IAutomaticProvisionEndPoint" />
    public class AutomaticProvisionEndPoint : IAutomaticProvisionEndPoint
    {
        private readonly IBillbeeRestClient _restClient;

        internal AutomaticProvisionEndPoint(IBillbeeRestClient restClient)
        {
            _restClient = restClient;
        }

        [ApiMapping("/api/v1/automaticprovision/createaccount", HttpOperation.Post)]
        public ApiResult<CreateUserResult> CreateAccount(Account createAccountContainer)
        {
            return _restClient.Post<ApiResult<CreateUserResult>>("/automaticprovision/createaccount", createAccountContainer);
        }

        [ApiMapping("/api/v1/automaticprovision/termsinfo", HttpOperation.Get)]
        public ApiResult<TermsResult> TermsInfo()
        {
            return _restClient.Get<ApiResult<TermsResult>>("/automaticprovision/termsinfo");
        }
    }
}
