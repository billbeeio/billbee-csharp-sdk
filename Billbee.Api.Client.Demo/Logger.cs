using System;

namespace Billbee.Api.Client.Demo
{
    /// <summary>
    /// Very simple logger, for writing data from the api to the console.
    /// </summary>
    public class Logger : ILogger
    {
        /// <summary>
        /// Generates a log message to the console.
        /// </summary>
        /// <param name="message">The message, that describes the content of the log entry</param>
        /// <param name="severity">The severy, in which context the log message was created</param>
        public void LogMsg(string message, LogSeverity severity)
        {
            Console.WriteLine($"{DateTime.Now} {severity}: {message}");
        }
    }
}
