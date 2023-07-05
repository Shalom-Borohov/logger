using Logger.Loggers.Interfaces;
using System;
using System.Collections.Generic;
using static Logger.Loggers.Enums.LoggerTypes;

namespace Logger.Loggers.Classes
{
    public class LoggerCreator
    {
        private Dictionary<int, ILog> LoggerByType = new Dictionary<int, ILog>();

        public LoggerCreator()
        {
            LoggerByType.Add((int)LoggerType.CsvFileLog, InitCsvFileLog());
            LoggerByType.Add((int)LoggerType.EncryptedCsvFileLog, InitEncryptedCsvFileLog());
            LoggerByType.Add((int)LoggerType.EventLog, InitEventLog());
        }

        private CsvFileLog InitCsvFileLog() => new CsvFileLog { FilePath = Environment.GetEnvironmentVariable("CSV_FILE_PATH") };

        private EncryptedCsvFileLog InitEncryptedCsvFileLog() =>
            new EncryptedCsvFileLog { FilePath = Environment.GetEnvironmentVariable("ENCRYPTED_CSV_FILE_PATH") };

        private EventLog InitEventLog() => EventLog.GetInstance();

        public ILog Create(LoggerType loggerType) => LoggerByType[(int)loggerType];
    }
}
