using System;

namespace PaCoSe.Exceptions
{
    public class DuplicateEntryException : BaseSerializableException
    {
        public DuplicateEntryException() : base()
        {
        }

        public DuplicateEntryException(string message) : base(message)
        {
        }

        public DuplicateEntryException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
