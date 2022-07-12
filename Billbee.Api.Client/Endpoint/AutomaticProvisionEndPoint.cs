using Billbee.Api.Client.Endpoint.Interfaces;
using Billbee.Api.Client.Model;

namespace Billbee.Api.Client.EndPoint
{
    /// <inheritdoc cref="Billbee.Api.Client.Endpoint.Interfaces.IAutomaticProvisionEndPoint" />
    public class AutomaticProvisionEndPoint : RestClientBaseClass, IAutomaticProvisionEndPoint
    {
        internal AutomaticProvisionEndPoint(ApiConfiguration config, ILogger logger = null) : base(logger, config)
        {
        }

        public ApiResult<CreateUserResult> CreateAccount(Account createAccountContainer)
        {
            return post<ApiResult<CreateUserResult>>("/automaticprovision/createaccount", createAccountContainer);
        }

        public ApiResult<TermsResult> TermsInfo()
        {
            return requestResource<ApiResult<TermsResult>>("/automaticprovision/termsinfo");
        }
    }
}
