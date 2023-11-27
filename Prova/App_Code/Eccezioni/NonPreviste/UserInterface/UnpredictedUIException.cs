using System;
using System.Runtime.Serialization;

namespace GameCompletionManager
{
    public class UnpredictedUIException : UnpredictedException
    {
        public UnpredictedUIException()
        {
        }

        public UnpredictedUIException(string message) : base(message)
        {
        }

        public UnpredictedUIException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnpredictedUIException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}