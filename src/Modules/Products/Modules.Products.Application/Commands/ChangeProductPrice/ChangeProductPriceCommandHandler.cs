using MediatR;
using Modules.Products.Domain.Repositories;
using Shared.Abstractions.UnitOfWork;
using Shared.Abstractions.UserServices;
using Shared.Application.Exceptions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Modules.Products.Application.Commands.ChangeProductPrice
{
    public class ChangeProductPriceCommandHandler : IRequestHandler<ChangeProductPriceCommand, Guid>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ChangeProductPriceCommandHandler(ICurrentUserService currentUserService,
                                                IProductRepository productRepository,
                                                IUnitOfWork unitOfWork)
        {
            _currentUserService = currentUserService;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Guid> Handle(ChangeProductPriceCommand command, CancellationToken cancellationToken)
        {
            var shopId = _currentUserService.UserId;

            var product = await _productRepository.GetById(command.Id);

            if (product == null || product.ShopId.Value != shopId)
            {
                throw new NotFoundException("Product not found");
            }

            product.ChangeProductPrice(command.Amount, command.Currency);

            await _unitOfWork.CommitAndDispatchDomainEventsAsync(product);

            return product.Id;
        }
    }
}
