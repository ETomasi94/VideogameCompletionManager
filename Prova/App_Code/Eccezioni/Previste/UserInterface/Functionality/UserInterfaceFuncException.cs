using System;
using System.Runtime.Serialization;

namespace GameCompletionManager
{
    public class UserInterfaceFuncException : UserInterfaceException
    {
        public UserInterfaceFuncException()
        {
        }

        public UserInterfaceFuncException(string message) : base(message)
        {
        }

        public UserInterfaceFuncException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserInterfaceFuncException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}