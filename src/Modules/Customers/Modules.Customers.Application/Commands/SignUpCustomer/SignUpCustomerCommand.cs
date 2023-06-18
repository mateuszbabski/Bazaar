using MediatR;
using Shared.Application.Auth;
using System.ComponentModel.DataAnnotations;

namespace Modules.Customers.Application.Commands.SignUpCustomer
{
    public class SignUpCustomerCommand : IRequest<AuthenticationResult>
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string TelephoneNumber { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
    }
}
