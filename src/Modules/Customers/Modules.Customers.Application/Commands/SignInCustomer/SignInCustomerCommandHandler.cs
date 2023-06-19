using MediatR;
using Modules.Customers.Domain.Repositories;
using Shared.Abstractions.Auth;
using Shared.Application.Auth;
using Shared.Application.Exceptions;

namespace Modules.Customers.Application.Commands.SignInCustomer
{
    internal sealed class SignInCustomerCommandHandler : IRequestHandler<SignInCustomerCommand, AuthenticationResult>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ITokenManager _tokenManager;
        private readonly IHashingService _hashingService;

        public SignInCustomerCommandHandler(ICustomerRepository customerRepository,
                                            ITokenManager tokenManager,
                                            IHashingService hashingService)
        {
            _customerRepository = customerRepository;
            _tokenManager = tokenManager;
            _hashingService = hashingService;
        }

        public async Task<AuthenticationResult> Handle(SignInCustomerCommand command, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetCustomerByEmail(command.Email) 
                ?? throw new BadRequestException("Invalid email or password");

            if (!_hashingService.ValidatePassword(command.Password, customer.PasswordHash))
            {
                throw new BadRequestException("Invalid email or password");
            }

            var token = _tokenManager.GenerateToken(customer.Id, customer.Email, customer.Role);

            return new AuthenticationResult(customer.Id, token);
        }
    }
}
