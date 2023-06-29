using Logger.src.loggers.interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

namespace Logger.src.loggers.classes
{
    internal class EncryptedCsvFileLog : FileLog, ILog
    {
        public void ClearLog() => File.Create(FilePath);

        public IEnumerable<string> ReadEntries(DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public void WriteEntry(LogEntry entry)
        {
            using var fileStream = new FileStream(FilePath, FileMode.Append);
            var encryptionKey = CreateEncryptionKey();

            using var aes = Aes.Create();
            aes.Key = encryptionKey;
            fileStream.Write(aes.IV, 0, aes.IV.Length);

            using var cryptoStream = new CryptoStream(fileStream, aes.CreateEncryptor(), CryptoStreamMode.Write);

            using var encryptedWriter = new StreamWriter(cryptoStream);

            encryptedWriter.WriteLine($"{entry.DateAndTime},{entry}{Environment.NewLine}");

        }

        // TODO: Move key to env
        private byte[] CreateEncryptionKey()
        {
            byte[] key = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };

            return key;
        }
    }
}
