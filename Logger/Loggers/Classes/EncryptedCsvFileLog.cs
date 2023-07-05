using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Logger.Loggers.Classes
{
    public class EncryptedCsvFileLog : CsvFileLog
    {
        public override IEnumerable<string> ReadEntries(DateTime dateTime)
        {
            var decryptedText = RemoveNoise(GetDecryptedText());

            return decryptedText.Split($"{Environment.NewLine}")
                .Where((string line) => GetDateTimeString(line).Equals(dateTime.ToString()))
                .Select(GetLog);
        }

        public override void WriteEntry(LogEntry entry)
        {
            using var fileStream = new FileStream(FilePath, FileMode.Append);
            byte[] encryptionKey = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("ENCRYPTION_KEY"));

            using var aes = Aes.Create();
            aes.Key = encryptionKey;
            fileStream.Write(aes.IV, 0, aes.IV.Length);

            using var cryptoStream = new CryptoStream(fileStream, aes.CreateEncryptor(), CryptoStreamMode.Write);

            using var encryptedWriter = new StreamWriter(cryptoStream);

            encryptedWriter.Write($"{entry.DateAndTime},{entry}{Environment.NewLine}");
        }

        private string GetDecryptedText()
        {
            using var fileStream = new FileStream(FilePath, FileMode.Open);
            using var aes = Aes.Create();

            byte[] iv = ReadIVFromFile(aes, fileStream);
            byte[] key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("ENCRYPTION_KEY"));

            using var cryptoStream = new CryptoStream(fileStream, aes.CreateDecryptor(key, iv), CryptoStreamMode.Read);
            using var decryptReader = new StreamReader(cryptoStream);

            return decryptReader.ReadToEnd();
        }

        private byte[] ReadIVFromFile(Aes aes, FileStream fileStream)
        {
            var iv = new byte[aes.IV.Length];
            var numBytesToRead = aes.IV.Length;

            while (numBytesToRead > 0)
            {
                int n = fileStream.Read(iv);
                if (n == 0) break;

                numBytesToRead -= n;
            }

            return iv;
        }

        private string RemoveNoise(string noisyText) =>
            Regex.Replace(noisyText, EverythingBeforeDateTimeOrLinesWithoutDateRegex(), "").TrimEnd();

        private string EverythingBeforeDateTimeOrLinesWithoutDateRegex() =>
            @"((.*)(?=\d{2}\/\d{2}\/\d{4} \d{2}:\d{2}:\d{2},)|(^((?!\d{2}\/\d{2}\/\d{4}).)*$))";
    }
}
