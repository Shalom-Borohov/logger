using System;
using static Logger.src.loggers.enums.SeverityLevel;

namespace Logger.src.loggers.classes
{
    internal class LogEntry
    {
        public Severity Severity { get; set; }
        public DateTime DateAndTime { get; set; }
        public string Message { get; set; }

        public override string ToString() => $"{DateAndTime}; {Severity}; {Message}";
    }
}
