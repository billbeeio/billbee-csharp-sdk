namespace Billbee.Api.Client.Enums
{
    /// <summary>
    /// Defines how server side errors should be handled on client.
    /// </summary>
    public enum ErrorHandlingEnum
    {
        /// <summary>
        /// Throws an exception, if a server side error occurs while processing a request
        /// </summary>
        ThrowException,

        /// <summary>
        /// Returns an object, that contains further error information, if a server error occurs while processing a request
        /// </summary>
        ReturnErrorContent
    }
}
