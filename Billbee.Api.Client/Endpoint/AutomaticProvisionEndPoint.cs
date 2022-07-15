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

        public ApiResult<CreateUserResult> CreateAccount(Account createAccountContainer)
        {
            return _restClient.Post<ApiResult<CreateUserResult>>("/automaticprovision/createaccount", createAccountContainer);
        }

        public ApiResult<TermsResult> TermsInfo()
        {
            return _restClient.Get<ApiResult<TermsResult>>("/automaticprovision/termsinfo");
        }
    }
}
