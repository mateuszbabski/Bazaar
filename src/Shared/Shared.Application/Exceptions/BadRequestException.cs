using System.Net;

namespace Shared.Application.Exceptions
{
    public class BadRequestException : CustomException
    {
        public BadRequestException(string message)
            : base(message, null, HttpStatusCode.BadRequest)
        {
        }
    }
}
