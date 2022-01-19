using System;

namespace BLL.Exceptions
{
    public class BookException : Exception
    {
        public BookException(string message)
            : base(message) { }
    }
}
