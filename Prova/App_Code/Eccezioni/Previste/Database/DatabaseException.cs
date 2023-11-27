using System;
using System.Runtime.Serialization;

namespace GameCompletionManager
{
    /// <summary>
    /// Descrizione di riepilogo per DatabaseException
    /// </summary>
    public class DatabaseException : PredictedException
    {
        public DatabaseException()
        {
        }

        public DatabaseException(string message) : base(message)
        {
        }

        public DatabaseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DatabaseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}