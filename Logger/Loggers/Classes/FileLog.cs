using Logger.Loggers.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;

namespace Logger.Loggers.Classes
{
    public abstract class FileLog : ILog
    {
        public string FilePath { get; set; }

        public void ClearLog() => File.WriteAllText(FilePath, "");

        public abstract IEnumerable<string> ReadEntries(DateTime dateTime);

        public abstract void WriteEntry(LogEntry entry);
    }
}
