using MediatR;
using Modules.Shops.Domain.Repositories;
using Shared.Abstractions.Auth;
using Shared.Application.Auth;
using Shared.Application.Exceptions;

namespace Modules.Shops.Application.Commands.SignInShop
{
    public class SignInShopCommandHandler : IRequestHandler<SignInShopCommand, AuthenticationResult>
    {
        private readonly IShopRepository _shopRepository;
        private readonly ITokenManager _tokenManager;
        private readonly IHashingService _hashingService;

        public SignInShopCommandHandler(IShopRepository shopRepository,
                                        ITokenManager tokenManager,
                                        IHashingService hashingService)
        {
            _shopRepository = shopRepository;
            _tokenManager = tokenManager;
            _hashingService = hashingService;
        }
        public async Task<AuthenticationResult> Handle(SignInShopCommand command, CancellationToken cancellationToken)
        {
            var shop = await _shopRepository.GetShopByEmail(command.Email)
                ?? throw new BadRequestException("Invalid email or password");

            if (!_hashingService.ValidatePassword(command.Password, shop.PasswordHash))
            {
                throw new BadRequestException("Invalid email or password");
            }

            var token = _tokenManager.GenerateToken(shop.Id, shop.Email, shop.Role);

            return new AuthenticationResult(shop.Id, token);
        }
    }
}
