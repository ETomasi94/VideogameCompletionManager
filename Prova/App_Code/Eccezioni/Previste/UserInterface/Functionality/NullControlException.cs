using System;
using System.Runtime.Serialization;

namespace GameCompletionManager
{
    public class NullControlException : UserInterfaceFuncException
    {
        public NullControlException()
        {
        }

        public NullControlException(string message) : base(message)
        {
        }

        public NullControlException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NullControlException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}