using Logger.src.loggers.interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Logger.src.loggers.classes
{
    internal class EncryptedCsvFileLog : FileLog, ILog
    {
        public void ClearLog() => File.Create(FilePath);

        public IEnumerable<string> ReadEntries(DateTime dateTime)
        {
            string decryptedText = RemoveNoise(GetDecryptedText());

            return decryptedText.Split($"{Environment.NewLine}")
                .Where((string line) => GetDateTimeString(line).Equals(dateTime.ToString()))
                .Select(GetLog);
        }

        private string GetDecryptedText()
        {
            using var fileStream = new FileStream(FilePath, FileMode.Open);
            using var aes = Aes.Create();

            byte[] iv = ReadIVFromFile(aes, fileStream);

            using var cryptoStream = new CryptoStream(fileStream, aes.CreateDecryptor(GetEncryptionKey(), iv), CryptoStreamMode.Read);
            using var decryptReader = new StreamReader(cryptoStream);

            return decryptReader.ReadToEnd();
        }

        private byte[] ReadIVFromFile(Aes aes, FileStream fileStream)
        {
            var iv = new byte[aes.IV.Length];
            int numBytesToRead = aes.IV.Length;

            while (numBytesToRead > 0)
            {
                int n = fileStream.Read(iv);
                if (n == 0) break;

                numBytesToRead -= n;
            }

            return iv;
        }

        private string RemoveNoise(string noisyText) =>
            Regex.Replace(noisyText, @"((.*)(?=\d{2}\/\d{2}\/\d{4} \d{2}:\d{2}:\d{2},))", "").TrimEnd();

        private string GetDateTimeString(string line) => line.Split(',')[0];

        private string GetLog(string line) => line.Split(',')[1];

        public void WriteEntry(LogEntry entry)
        {
            using var fileStream = new FileStream(FilePath, FileMode.Append);
            var encryptionKey = GetEncryptionKey();

            using var aes = Aes.Create();
            aes.Key = encryptionKey;
            fileStream.Write(aes.IV, 0, aes.IV.Length);

            using var cryptoStream = new CryptoStream(fileStream, aes.CreateEncryptor(), CryptoStreamMode.Write);

            using var encryptedWriter = new StreamWriter(cryptoStream);

            encryptedWriter.Write($"{entry.DateAndTime},{entry}{Environment.NewLine}");
        }

        // TODO: Move key to env
        private byte[] GetEncryptionKey()
        {
            byte[] key = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };

            return key;
        }
    }
}
