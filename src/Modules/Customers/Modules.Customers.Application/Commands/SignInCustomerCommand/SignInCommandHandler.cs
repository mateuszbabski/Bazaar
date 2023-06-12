﻿using MediatR;
using Modules.Customers.Domain.Repositories;
using Shared.Abstractions.Auth;
using Shared.Application.Auth;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Modules.Customers.Application.Commands.SignInCustomerCommand
{
    internal sealed class SignInCommandHandler : IRequestHandler<SignInCommand, AuthenticationResult>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ITokenManager _tokenManager;
        private readonly IHashingService _hashingService;

        public SignInCommandHandler(ICustomerRepository customerRepository,
                                    ITokenManager tokenManager,
                                    IHashingService hashingService)
        {
            _customerRepository = customerRepository;
            _tokenManager = tokenManager;
            _hashingService = hashingService;
        }

        public async Task<AuthenticationResult> Handle(SignInCommand command, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetCustomerByEmail(command.Email) ?? throw new Exception("Email not found");

            if (!_hashingService.ValidatePassword(command.Password, customer.PasswordHash))
            {
                throw new Exception("Invalid password");
            }

            var token = _tokenManager.GenerateToken(customer.Id, customer.Email, customer.Role);

            return new AuthenticationResult(customer.Id, token);
        }
    }
}
