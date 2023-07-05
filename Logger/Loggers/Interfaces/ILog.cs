using Logger.Loggers.Classes;
using System;
using System.Collections.Generic;
using static Logger.Loggers.Enums.SeverityLevel;

namespace Logger.Loggers.Interfaces
{
    public interface ILog
    {
        public void WriteEntry(LogEntry entry);
        public IEnumerable<string> ReadEntries(DateTime dateTime);
        public void ClearLog();
        public void WriteInfoEntry(string message) => WriteInfoEntry(message, DateTime.Now);

        public void WriteErrorEntry(string message) => WriteErrorEntry(message, DateTime.Now);

        public void WriteInfoEntry(string message, DateTime dateTime) =>
            WriteEntry(new LogEntry { DateAndTime = dateTime, Message = message, Severity = Severity.Information });

        public void WriteErrorEntry(string message, DateTime dateTime) =>
            WriteEntry(new LogEntry { DateAndTime = dateTime, Message = message, Severity = Severity.Information });
    }
}
