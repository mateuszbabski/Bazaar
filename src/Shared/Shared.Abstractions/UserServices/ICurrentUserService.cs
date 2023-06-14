using System.Security.Claims;

namespace Shared.Abstractions.UserServices
{
    public interface ICurrentUserService
    {
        ClaimsPrincipal User { get; }
        Guid UserId { get; }
    }
}
