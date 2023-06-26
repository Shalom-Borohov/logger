using Logger.src.loggers.interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Logger.src.loggers.classes
{
    internal class CsvFileLog : FileLog, ILog
    {
        public void ClearLog() => File.Create(FilePath);

        public IEnumerable<string> ReadEntries(DateTime dateTime) =>
            File.ReadLines(FilePath)
                .Where((string line) => GetDateTimeString(line).Equals(dateTime.ToString()))
                .Select(GetLog);

        private string GetDateTimeString(string line) => line.Split(',')[0];

        private string GetLog(string line) => line.Split(',')[1];

        public void WriteEntry(LogEntry entry) => File.AppendAllText(FilePath, $"{entry.DateAndTime},{entry}{Environment.NewLine}");
    }
}
