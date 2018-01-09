using Billbee.Api.Client.Model;
using BillBee.API.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billbee.Api.Client.Endpoint
{
    /// <summary>
    /// Endpoint to access functions for auto provisioning
    /// </summary>
    public class AutomaticProvisionEndPoint : RestClientBaseClass
    {
        internal AutomaticProvisionEndPoint(ApiConfiguration config, ILogger logger = null) : base(logger, config)
        {

        }

        /// <summary>
        /// Creates a new user account in billbee
        /// </summary>
        /// <param name="createAccountContainer">The definition of the account, that shoule be created</param>
        /// <returns>The password, user-id and one time loging url.</returns>
        public ApiResult<CreateUserResult> CreateAccount(Account createAccountContainer)
        {
            return post<ApiResult<CreateUserResult>>("/automaticprovision/createaccount", createAccountContainer);
        }

        /// <summary>
        /// Calls the terms and coditions of use for billbee
        /// </summary>
        /// <returns>The urls of all needed information.</returns>
        public ApiResult<TermsResult> TermsInfo()
        {
            return requestResource<ApiResult<TermsResult>>("/automaticprovision/termsinfo");
        }
    }
}
