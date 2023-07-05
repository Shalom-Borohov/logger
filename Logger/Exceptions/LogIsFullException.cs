using System;
using System.Runtime.Serialization;

namespace Logger.Exceptions
{
    public class LogIsFullException : Exception
    {
        public LogIsFullException() { }

        public LogIsFullException(string message) : base(message) { }

        public LogIsFullException(string message, Exception innerException) : base(message, innerException) { }

        protected LogIsFullException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
