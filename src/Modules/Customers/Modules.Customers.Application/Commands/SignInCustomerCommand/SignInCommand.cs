using MediatR;
using Shared.Application.Auth;
using System.ComponentModel.DataAnnotations;

namespace Modules.Customers.Application.Commands.SignInCustomerCommand
{
    public class SignInCommand : IRequest<AuthenticationResult>
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
