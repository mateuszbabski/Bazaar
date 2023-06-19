using MediatR;
using Shared.Application.Auth;
using System.ComponentModel.DataAnnotations;

namespace Modules.Shops.Application.Commands.SignInShop
{
    public class SignInShopCommand : IRequest<AuthenticationResult>
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
