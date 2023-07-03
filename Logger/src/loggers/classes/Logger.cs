using Logger.src.loggers.interfaces;
using System;
using System.Collections.Generic;

namespace Logger.src.loggers.classes
{
    internal class Logger : ILog
    {
        public ILog LoggerInstance { get; set; }

        public void ClearLog() => LoggerInstance.ClearLog();

        public IEnumerable<string> ReadEntries(DateTime dateTime) => LoggerInstance.ReadEntries(dateTime);

        public void WriteEntry(LogEntry entry) => LoggerInstance.WriteEntry(entry);
    }
}
