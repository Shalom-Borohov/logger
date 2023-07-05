using Logger.Loggers.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Logger.Loggers.Classes
{
    public class CsvFileLog : FileLog
    {
        public override IEnumerable<string> ReadEntries(DateTime dateTime) =>
            File.ReadLines(FilePath)
                .Where((string line) => GetDateTimeString(line).Equals(dateTime.ToString()))
                .Select(GetLog);

        public override void WriteEntry(LogEntry entry) =>
            File.AppendAllText(FilePath, $"{entry.DateAndTime},{entry}{Environment.NewLine}");

        protected string GetDateTimeString(string line) => line.Split(',')[0];

        protected string GetLog(string line) => line.Split(',')[1];

    }
}
