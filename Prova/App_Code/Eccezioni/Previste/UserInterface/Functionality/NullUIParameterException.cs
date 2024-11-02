using System;
using System.Runtime.Serialization;

namespace GameCompletionManager
{
    public class NullUIParameterException : UserInterfaceFuncException
    {
        public NullUIParameterException()
        {
        }

        public NullUIParameterException(string message) : base(message)
        {
        }

        public NullUIParameterException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NullUIParameterException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}