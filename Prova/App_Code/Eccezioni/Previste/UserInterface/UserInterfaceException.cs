using System;
using System.Runtime.Serialization;

namespace GameCompletionManager
{
    public class UserInterfaceException : PredictedException
    {
        public UserInterfaceException()
        {
        }

        public UserInterfaceException(string message) : base(message)
        {
        }

        public UserInterfaceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserInterfaceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}