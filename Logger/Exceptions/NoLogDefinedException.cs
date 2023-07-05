using System;
using System.Runtime.Serialization;

namespace Logger.Exceptions
{
    public class NoLogDefinedException : Exception
    {
        public NoLogDefinedException() { }

        public NoLogDefinedException(string message) : base(message) { }

        public NoLogDefinedException(string message, Exception innerException) : base(message, innerException) { }

        protected NoLogDefinedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
