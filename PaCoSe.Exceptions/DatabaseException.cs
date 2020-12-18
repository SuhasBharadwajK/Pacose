using System;

namespace PaCoSe.Exceptions
{
    public class DatabaseException : BaseSerializableException
    {
        public DatabaseException(string message = "An error has occured while performing the database operation") : base(message)
        {
        }

        public DatabaseException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
