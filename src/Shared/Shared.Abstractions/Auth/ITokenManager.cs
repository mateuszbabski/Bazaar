using Shared.Domain.ValueObjects;

namespace Shared.Abstractions.Auth
{
    public interface ITokenManager
    {
        string GenerateToken(Guid id, string email, Roles role);
    }
}
