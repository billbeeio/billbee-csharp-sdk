using Billbee.Api.Client.Model;

namespace Billbee.Api.Client.Endpoint.Interfaces
{
    /// <summary>
    /// EndPoint to access functions for auto provisioning
    /// </summary>
    public interface IAutomaticProvisionEndPoint
    {
        /// <summary>
        /// Creates a new user account in billbee
        /// </summary>
        /// <param name="createAccountContainer">The definition of the account, that shoule be created</param>
        /// <returns>The password, user-id and one time loging url.</returns>
        ApiResult<CreateUserResult> CreateAccount(Account createAccountContainer);

        /// <summary>
        /// Calls the terms and coditions of use for billbee
        /// </summary>
        /// <returns>The urls of all needed information.</returns>
        ApiResult<TermsResult> TermsInfo();
    }
}