using System;

namespace Transportadora.Api.Exceptions
{
    public class TransportadoraException : Exception
    {
        public int HttpStatus { get; set; }
        public override string Message { get; }

        public TransportadoraException(int httpStatus, string message)
        {
            HttpStatus = httpStatus;
            Message = message;
        }
    }
}