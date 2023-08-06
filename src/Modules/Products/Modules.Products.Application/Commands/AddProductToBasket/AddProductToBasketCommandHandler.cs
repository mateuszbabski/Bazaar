using MediatR;
using Modules.Products.Contracts.Events;
using Modules.Products.Domain.Repositories;
using Shared.Abstractions.Events;
using Shared.Abstractions.UserServices;
using Shared.Application.Exceptions;

namespace Modules.Products.Application.Commands.AddProductToBasket
{
    public class AddProductToBasketCommandHandler : IRequestHandler<AddProductToBasketCommand, Unit>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICurrentUserService _userService;
        private readonly IEventDispatcher _eventDispatcher;

        public AddProductToBasketCommandHandler(IProductRepository productRepository,
                                                ICurrentUserService userService,
                                                IEventDispatcher eventDispatcher)
        {
            _productRepository = productRepository;
            _userService = userService;
            _eventDispatcher = eventDispatcher;
        }

        public async Task<Unit> Handle(AddProductToBasketCommand request, CancellationToken cancellationToken)
        {
            var customerId = _userService.UserId;
            var product = await _productRepository.GetById(request.Id)
                ?? throw new NotFoundException("Product not found.");

            await _eventDispatcher.PublishAsync(new ProductAddedToBasketEvent(product.Id,
                                                                              product.ShopId,
                                                                              customerId,
                                                                              product.Price,
                                                                              product.WeightPerUnit,
                                                                              request.Quantity),
                                                cancellationToken);

            return Unit.Value;
        }
    }
}
