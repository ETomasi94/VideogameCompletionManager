using System;
using System.Runtime.Serialization;

namespace GameCompletionManager
{
    /// <summary>
    /// Descrizione di riepilogo per Class1
    /// </summary>
    public class PredictedException : Exception
    {
        public PredictedException()
        {
        }

        public PredictedException(string message) : base(message)
        {
        }

        public PredictedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PredictedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}