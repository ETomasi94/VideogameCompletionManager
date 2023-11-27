using System;
using System.Runtime.Serialization;

namespace GameCompletionManager
{
    /// <summary>
    /// Descrizione di riepilogo per SystemException
    /// </summary>
    public class UnpredictedSystemException : UnpredictedException
    {
        public UnpredictedSystemException()
        {
        }

        public UnpredictedSystemException(string message) : base(message)
        {
        }

        public UnpredictedSystemException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnpredictedSystemException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}