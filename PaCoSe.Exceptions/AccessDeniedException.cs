using System;

namespace PaCoSe.Exceptions
{
    public class AccessDeniedException : BaseSerializableException
    {
        public AccessDeniedException() : base()
        {
        }

        public AccessDeniedException(string message) : base(message)
        {
        }

        public AccessDeniedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
