using FluentValidation;
using MediatR;
using Modules.Shops.Domain.Entities;
using Modules.Shops.Domain.Repositories;
using Shared.Abstractions.Auth;
using Shared.Abstractions.UnitOfWork;
using Shared.Application.Auth;
using Shared.Application.Exceptions;
using Shared.Domain.ValueObjects;

namespace Modules.Shops.Application.Commands.SignUpShop
{
    public class SignUpShopCommandHandler : IRequestHandler<SignUpShopCommand, AuthenticationResult>
    {
        private readonly IShopRepository _shopRepository;
        private readonly ITokenManager _tokenManager;
        private readonly IHashingService _hashingService;
        private readonly IUnitOfWork _unitOfWork;

        public SignUpShopCommandHandler(IShopRepository shopRepository,
                                        ITokenManager tokenManager,
                                        IHashingService hashingService,
                                        IUnitOfWork unitOfWork)
        {
            _shopRepository = shopRepository;
            _tokenManager = tokenManager;
            _hashingService = hashingService;
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthenticationResult> Handle(SignUpShopCommand command, CancellationToken cancellationToken)
        {
            var emailCheck = await _shopRepository.GetShopByEmail(command.Email);
            if (emailCheck != null)
            {
                throw new BadRequestException("Email in use");
            }

            var validator = new SignUpShopValidator();
            validator.ValidateAndThrow(command);

            //await CheckIfEmailIsFreeToUse(command.Email);

            var passwordHash = _hashingService.GenerateHashPassword(command.Password);
            var address = Address.CreateAddress(command.Country, command.City, command.Street, command.PostalCode);
            var shop = Shop.Create(command.Email,
                                   passwordHash,
                                   command.OwnerName,
                                   command.OwnerLastName,
                                   command.ShopName,
                                   address,
                                   command.TaxNumber,
                                   command.ContactNumber);

            await _shopRepository.Add(shop);

            await _unitOfWork.CommitAndDispatchDomainEventsAsync(shop);
            //await _unitOfWork.CommitAndDispatchEventsAsync();

            var token = _tokenManager.GenerateToken(shop.Id, shop.Email, shop.Role);

            return new AuthenticationResult(shop.Id, token);
        }
    }
}
