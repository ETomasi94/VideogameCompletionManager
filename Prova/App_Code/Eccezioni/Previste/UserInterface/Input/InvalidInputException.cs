using System;
using System.Runtime.Serialization;

namespace GameCompletionManager
{
    public class InvalidInputException : UserInterfaceException
    {
        public InvalidInputException()
        {
        }

        public InvalidInputException(string message) : base(message)
        {
        }

        public InvalidInputException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidInputException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

}