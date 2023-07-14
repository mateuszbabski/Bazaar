using System.Net;

namespace Shared.Application.Exceptions
{
    public class TransactionFailedException : CustomException
    {
        public TransactionFailedException(string message)
            : base(message, null, HttpStatusCode.BadRequest)
        {
            
        }
    }
}
