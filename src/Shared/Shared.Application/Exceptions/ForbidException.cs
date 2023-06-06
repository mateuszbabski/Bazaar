using System.Net;

namespace Shared.Application.Exceptions
{
    public class ForbidException : CustomException
    {
        public ForbidException(string message)
            : base(message, null, HttpStatusCode.Forbidden)
        {
        }
    }
}
