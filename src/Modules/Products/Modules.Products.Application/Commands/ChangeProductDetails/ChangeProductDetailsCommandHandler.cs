using MediatR;
using Modules.Products.Application.Contracts;
using Modules.Products.Domain.Repositories;
using Shared.Abstractions.UserServices;
using Shared.Application.Exceptions;

namespace Modules.Products.Application.Commands.ChangeProductDetails
{
    public class ChangeProductDetailsCommandHandler : IRequestHandler<ChangeProductDetailsCommand, Guid>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IProductRepository _productRepository;
        private readonly IProductsUnitOfWork _unitOfWork;

        public ChangeProductDetailsCommandHandler(ICurrentUserService currentUserService,
                                                  IProductRepository productRepository,
                                                  IProductsUnitOfWork unitOfWork)
        {
            _currentUserService = currentUserService;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(ChangeProductDetailsCommand command, CancellationToken cancellationToken)
        {
            var shopId = _currentUserService.UserId;

            var product = await _productRepository.GetById(command.Id);

            if (product == null || product.ShopId != shopId)
            {
                throw new NotFoundException("Product not found");
            }

            product.ChangeProductDetails(command.ProductName,
                                         command.ProductDescription,
                                         command.ProductCategory,
                                         command.Unit);

            await _unitOfWork.CommitAndDispatchDomainEventsAsync(product);

            return product.Id;
        }
    }
}
