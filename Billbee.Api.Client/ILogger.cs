using System;
using System.Collections.Generic;
using System.Text;

namespace BillBee.API.Client
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
