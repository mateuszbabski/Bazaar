using FluentValidation;
using MediatR;
using Modules.Customers.Domain.Entities;
using Modules.Customers.Domain.Repositories;
using Shared.Abstractions.Auth;
using Shared.Abstractions.UnitOfWork;
using Shared.Application.Auth;
using Shared.Domain.ValueObjects;

namespace Modules.Customers.Application.Commands.SignUpCustomerCommand
{
    internal sealed class SignUpCommandHandler : IRequestHandler<SignUpCommand, AuthenticationResult>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ITokenManager _tokenManager;
        private readonly IHashingService _hashingService;
        private readonly ICustomersUnitOfWork _unitOfWork;

        public SignUpCommandHandler(ICustomerRepository customerRepository,
                                    ITokenManager tokenManager,
                                    IHashingService hashingService,
                                    ICustomersUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _tokenManager = tokenManager;
            _hashingService = hashingService;
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthenticationResult> Handle(SignUpCommand command, CancellationToken cancellationToken)
        {
            var emailCheck = await _customerRepository.GetCustomerByEmail(command.Email);
            if (emailCheck != null)
            {
                throw new Exception("Email in use");
            }

            var validator = new SignUpValidator();
            validator.ValidateAndThrow(command);

            //await CheckIfEmailIsFreeToUse(command.Email);

            var passwordHash = _hashingService.GenerateHashPassword(command.Password);
            var address = Address.CreateAddress(command.Country, command.City, command.Street, command.PostalCode);

            var customer = Customer.Create(command.Email,
                                           passwordHash,
                                           command.Name,
                                           command.LastName,
                                           address,
                                           command.TelephoneNumber);

            await _customerRepository.Add(customer);

            await _unitOfWork.CommitAsync();

            var token = _tokenManager.GenerateToken(customer.Id, customer.Email, customer.Role);

            return new AuthenticationResult(customer.Id, token);
        }
    }
}
