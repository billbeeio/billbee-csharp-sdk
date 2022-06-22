using Billbee.Api.Client.Interfaces.Endpoint;
using Billbee.Api.Client.Model;

namespace Billbee.Api.Client.EndPoint
{
    /// <inheritdoc cref="Billbee.Api.Client.Interfaces.Endpoint.IAutomaticProvisionEndPoint" />
    public class AutomaticProvisionEndPoint : RestClientBaseClass, IAutomaticProvisionEndPoint
    {
        internal AutomaticProvisionEndPoint(ApiConfiguration config, ILogger logger = null) : base(logger, config)
        {
        }

        /// <inheritdoc />
        public ApiResult<CreateUserResult> CreateAccount(Account createAccountContainer)
        {
            return post<ApiResult<CreateUserResult>>("/automaticprovision/createaccount", createAccountContainer);
        }

        /// <inheritdoc />
        public ApiResult<TermsResult> TermsInfo()
        {
            return requestResource<ApiResult<TermsResult>>("/automaticprovision/termsinfo");
        }
    }
}