using System;
using System.Runtime.Serialization;

namespace GameCompletionManager
{
    /// <summary>
    /// Descrizione di riepilogo per DatabaseException
    /// </summary>
    public class UnpredictedDatabaseException : UnpredictedException
    {
        public UnpredictedDatabaseException()
        {
        }

        public UnpredictedDatabaseException(string message) : base(message)
        {
        }

        public UnpredictedDatabaseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnpredictedDatabaseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}