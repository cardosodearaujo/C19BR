using System;

namespace Everaldo.Cardoso.Araujo.C19BR.Framework.Exceptions
{
    public class InternalApiException : Exception
    {
        public InternalApiException() : base()
        {

        }

        public InternalApiException(string message) : base(message)
        {

        }

        public InternalApiException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
