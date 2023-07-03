using Logger.src.loggers.classes;
using System;
using System.Linq;
using static Logger.src.loggers.enums.SeverityLevel;

namespace Logger
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "logs.csv";
            string encryptedFilePath = "encryptedLogs.csv";
            var dateTime = new DateTime(2023, 6, 26, 15, 29, 57);
            var dateTimeForEncrypted = new DateTime(2023, 07, 02, 15, 53, 02);

            var csvFileLog = new CsvFileLog { FilePath = filePath };
            var encryptedCsvFileLog = new EncryptedCsvFileLog { FilePath = encryptedFilePath };
            var eventLog = EventLog.GetInstance();

            eventLog.WriteEntry(
                new LogEntry { DateAndTime = DateTime.Now, Message = "Wallak everything is A-OK", Severity = Severity.Information });

            eventLog.ClearLog();

            //encryptedCsvFileLog.WriteEntry(new LogEntry { DateAndTime = DateTime.Now, Message = "Blagan Gadol meod meod meod", Severity = Severity.Critical });
            //encryptedCsvFileLog.ReadEntries(dateTimeForEncrypted).ToList().ForEach(Console.WriteLine);


        }
    }
}
