namespace Billbee.Api.Client
{
    public enum LogSeverity
    {
        Info,
        Warning,
        Error
    }

    public interface ILogger
    {
        void LogMsg(string message, LogSeverity severity);
    }
}
