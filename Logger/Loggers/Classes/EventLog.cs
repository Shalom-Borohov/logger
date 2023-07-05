using Logger.Loggers.Interfaces;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using static Logger.Loggers.Enums.SeverityLevel;
using static Logger.Loggers.Enums.TaskCategory;

namespace Logger.Loggers.Classes
{
    public class EventLog : ILog
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

        private System.Diagnostics.EventLog SystemEventLog;

        private EventLog()
        {
            InitSourceIfNotExists();
        }

        private void InitSourceIfNotExists()
        {
            if (!System.Diagnostics.EventLog.SourceExists("My Log"))
            {
                System.Diagnostics.EventLog.CreateEventSource("Application", "My Log");
            }

            SystemEventLog = new System.Diagnostics.EventLog { Source = "My Log" };
        }

        private static EventLog Instance;

        public static EventLog GetInstance()
        {
            Instance ??= new EventLog();

            return Instance;
        }

        public void ClearLog() => SystemEventLog.Clear();

        public IEnumerable<string> ReadEntries(DateTime dateTime)
        {
            var entries = "";

            foreach (EventLogEntry entry in SystemEventLog.Entries)
            {
                if (entry.TimeGenerated.ToString().Equals(dateTime.ToString()))
                {
                    entries = string.Concat(entries, $"{entry.Message},");
                }
            }

            return string.Concat($"{dateTime}{Environment.NewLine}", entries).Split(",");
        }

        public void WriteEntry(LogEntry entry) =>
            SystemEventLog.WriteEntry(entry.Message, EntryTypeBySeverity[entry.Severity], EventId++, (short)TaskCategoryType.Writing);
    }
}
