using System;

namespace PaCoSe.Exceptions
{
    [Serializable]
    public class BaseSerializableException : BaseException
    {
        public BaseSerializableException() : base()
        {
        }

        public BaseSerializableException(string message) : base(message)
        {
        }

        public BaseSerializableException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
