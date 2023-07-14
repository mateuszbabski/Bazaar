using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Modules.Customers.Domain.Entities;
using Modules.Customers.Domain.Repositories;
using Serilog;
using Shared.Abstractions.Auth;
using Shared.Abstractions.UnitOfWork;
using Shared.Application.Auth;
using Shared.Application.Exceptions;
using Shared.Domain.ValueObjects;
using System.Reflection;

namespace Modules.Customers.Application.Commands.SignUpCustomer
{
    internal sealed class SignUpCustomerCommandHandler : IRequestHandler<SignUpCustomerCommand, AuthenticationResult>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ITokenManager _tokenManager;
        private readonly IHashingService _hashingService;
        private readonly IUnitOfWork _unitOfWork;        

        public SignUpCustomerCommandHandler(ICustomerRepository customerRepository,
                                            ITokenManager tokenManager,
                                            IHashingService hashingService,
                                            IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _tokenManager = tokenManager;
            _hashingService = hashingService;
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthenticationResult> Handle(SignUpCustomerCommand command, CancellationToken cancellationToken)
        {
            var emailCheck = await _customerRepository.GetCustomerByEmail(command.Email);
            if (emailCheck != null)
            {
                throw new BadRequestException("Email in use");
            }

            var validator = new SignUpCustomerValidator();
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
            
            Log.Information("events count: {@ev}", customer.DomainEvents.Count);
            await _unitOfWork.CommitAndDispatchDomainEventsAsync(customer);          

            var token = _tokenManager.GenerateToken(customer.Id, customer.Email, customer.Role);

            return new AuthenticationResult(customer.Id, token);
        }
    }
}
