using Shared.Application.Auth;

namespace Shared.Abstractions.Auth
{
    public interface ICustomerService
    {
        Task<AuthenticationResult> LoginCustomer(LoginRequest request);
        Task<AuthenticationResult> RegisterCustomer(RegisterCustomerRequest request);
    }
}
