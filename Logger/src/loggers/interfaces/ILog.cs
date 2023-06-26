using Logger.src.loggers.classes;
using System;
using System.Collections.Generic;

namespace Logger.src.loggers.interfaces
{
    internal interface ILog
    {
        void WriteEntry(LogEntry entry);
        IEnumerable<string> ReadEntries(DateTime dateTime);
        void ClearLog();
    }
}
