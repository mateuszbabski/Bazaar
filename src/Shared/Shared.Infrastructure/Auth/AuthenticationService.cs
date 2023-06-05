using Modules.Customers.Domain.Entities;
using Modules.Customers.Domain.Repositories;
using Shared.Abstractions.Auth;
using Shared.Application.Auth;
using Shared.Domain.ValueObjects;

namespace Shared.Infrastructure.Auth
{
    public class AuthenticationService : ICustomerService
    {
        private readonly ITokenManager _tokenManager;
        private readonly ICustomerRepository _customerRepository;
        private readonly IHashingService _hashingService;

        public AuthenticationService(ITokenManager tokenManager,
                                     ICustomerRepository customerRepository,
                                     IHashingService hashingService)
        {
            _tokenManager = tokenManager;
            _customerRepository = customerRepository;
            _hashingService = hashingService;
        }

        public async Task<AuthenticationResult> RegisterCustomer(RegisterCustomerRequest request)
        {
            var validator = new RegisterCustomerRequestValidator();
            validator.ValidateAndThrow(request);

            await CheckIfEmailIsFreeToUse(request.Email);

            var passwordHash = _hashingService.GenerateHashPassword(request.Password);
            var address = Address.CreateAddress(request.Country, request.City, request.Street, request.PostalCode);

            var customer = Customer.Create(request.Email,
                                           passwordHash,
                                           request.Name,
                                           request.LastName,
                                           address,
                                           request.TelephoneNumber);

            await _customerRepository.Add(customer);

            var token = _tokenManager.GenerateToken(customer.Id, customer.Email, customer.Role);

            return new AuthenticationResult(customer.Id, token);
        }

        public async Task<AuthenticationResult> LoginCustomer(LoginRequest request)
        {
            var customer = await _customerRepository.GetCustomerByEmail(request.Email) ?? throw new Exception("Email not found");

            if (!_hashingService.ValidatePassword(request.Password, customer.PasswordHash))
            {
                throw new Exception("Invalid password");
            }

            var token = _tokenManager.GenerateToken(customer.Id, customer.Email, customer.Role);

            return new AuthenticationResult(customer.Id, token);
        }

        //private async Task<bool> CheckIfEmailIsFreeToUse(string email)
        //{
        //    var customer = await _customerRepository.GetCustomerByEmail(email);
        //    //var shop = await _shopRepository.GetShopByEmail(email);

        //    if (customer != null || shop != null)
        //        throw new Exception("Email cannot be used");
        //    return true;
        //}
    }
}
