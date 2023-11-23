using System;
using System.Runtime.Serialization;

namespace GameCompletionManager
{
    /// <summary>
    /// Descrizione di riepilogo per UnpredictedException
    /// </summary>
    public class UnpredictedException : Exception
    {
        public UnpredictedException()
        {
            //
            // TODO: aggiungere qui la logica del costruttore
            //
        }

        public UnpredictedException(string message) : base(message)
        {
        }

        public UnpredictedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnpredictedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}