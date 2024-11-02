using System;
using System.Runtime.Serialization;

namespace GameCompletionManager
{
    /// <summary>
    /// Descrizione di riepilogo per SystemException
    /// </summary>
    public class SystemException : PredictedException
    {
        public SystemException()
        {
        }

        public SystemException(string message) : base(message)
        {
        }

        public SystemException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SystemException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}