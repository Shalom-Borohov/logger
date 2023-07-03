using Logger.src.loggers.interfaces;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using static Logger.src.loggers.enums.SeverityLevel;
using static Logger.src.loggers.enums.TaskCategory;

namespace Logger.src.loggers.classes
{
    internal class EventLog : ILog
    {
        public Dictionary<Severity, EventLogEntryType> EntryTypeBySeverity = new Dictionary<Severity, EventLogEntryType>
            {
                { Severity.Information, EventLogEntryType.Information },
                { Severity.Warning, EventLogEntryType.Warning },
                { Severity.Error, EventLogEntryType.Error },
                { Severity.Critical, EventLogEntryType.FailureAudit },
                { Severity.Verbose, EventLogEntryType.SuccessAudit }
            };

        private int EventId = 0;

        private System.Diagnostics.EventLog SystemEventLog = new System.Diagnostics.EventLog { Source = "Application" };

        private EventLog() { }

        private static EventLog Instance;

        public static EventLog GetInstance()
        {
            Instance ??= new EventLog();

            return Instance;
        }

        public void ClearLog() => SystemEventLog.Clear();

        public IEnumerable<string> ReadEntries(DateTime dateTime)
        {
            string entries = "";

            foreach (EventLogEntry entry in SystemEventLog.Entries)
            {
                if (entry.TimeGenerated.Equals(dateTime))
                {
                    entries = string.Concat(entries, $"{entry.Message}{Environment.NewLine}");
                }
            }

            return entries.Split($"{Environment.NewLine}");
        }

        public void WriteEntry(LogEntry entry) =>
            SystemEventLog.WriteEntry(entry.Message, EntryTypeBySeverity[entry.Severity], EventId++, (short)TaskCategoryType.Writing);
    }
}
