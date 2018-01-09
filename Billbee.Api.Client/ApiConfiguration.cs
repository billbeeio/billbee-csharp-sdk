using Billbee.Api.Client.Enums;

namespace Billbee.Api.Client
{
    /// <summary>
    /// Configuration parameters for the Billbee API client.
    /// </summary>
    public class ApiConfiguration
    {
        /// <summary>
        /// Username of the main user of your account.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// API password, could be set in Settings->Billbee API->General Settings
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The Api key for your application. Can be requested from the Billbee support.
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// The base url of the Billbee API. Typically, this sticks unchanged.
        /// </summary>
        public string BaseUrl { get; set; } = "https://app01.billbee.de/api/v1";

        /// <summary>
        /// How should the api treat server side errors
        /// </summary>
        public ErrorHandlingEnum errorHandlingBehaviour { get; set; } = ErrorHandlingEnum.throwException;
    }
}
