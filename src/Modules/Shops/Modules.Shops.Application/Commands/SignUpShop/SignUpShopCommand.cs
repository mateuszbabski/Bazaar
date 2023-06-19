using MediatR;
using Shared.Application.Auth;
using System.ComponentModel.DataAnnotations;

namespace Modules.Shops.Application.Commands.SignUpShop
{
    public class SignUpShopCommand : IRequest<AuthenticationResult>
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        public string OwnerName { get; set; }
        public string OwnerLastName { get; set; }
        public string ShopName { get; set; }
        public string TaxNumber { get; set; }
        public string ContactNumber { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
    }
}
