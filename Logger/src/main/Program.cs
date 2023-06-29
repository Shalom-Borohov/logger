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

            var csvFileLog = new CsvFileLog { FilePath = filePath };
            var encryptedCsvFileLog = new EncryptedCsvFileLog { FilePath = encryptedFilePath };

            encryptedCsvFileLog.WriteEntry(new LogEntry { DateAndTime = DateTime.Now, Message = "Balagan Gadol", Severity = Severity.Critical });
            // csvFileLog.ReadEntries(dateTime).ToList().ForEach(Console.WriteLine);
        }
    }
}
