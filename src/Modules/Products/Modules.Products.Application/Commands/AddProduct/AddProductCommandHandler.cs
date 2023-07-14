using MediatR;
using Modules.Products.Domain.Entities;
using Modules.Products.Domain.Repositories;
using Modules.Products.Domain.ValueObjects;
using Shared.Abstractions.UnitOfWork;
using Shared.Abstractions.UserServices;
using Shared.Application.Exceptions;
using Shared.Domain.ValueObjects;

namespace Modules.Products.Application.Commands.AddProduct
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Guid>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddProductCommandHandler(ICurrentUserService currentUserService,
                                        IProductRepository productRepository,
                                        IUnitOfWork unitOfWork)
        {
            _currentUserService = currentUserService;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(AddProductCommand command, CancellationToken cancellationToken)
        {
            var shopId = _currentUserService.UserId;

            if(shopId == Guid.Empty)
            {
                throw new ForbidException("Unauthorized.");
            }

            var price = MoneyValue.Of(command.Amount, command.Currency);
            var productCategory = ProductCategory.Create(command.ProductCategory);

            var product = Product.CreateProduct(command.ProductName,
                                                command.ProductDescription,
                                                productCategory,
                                                command.WeightPerUnit,
                                                price,
                                                command.Unit,
                                                shopId);

            await _productRepository.Add(product);

            await _unitOfWork.CommitAndDispatchDomainEventsAsync(product);

            return product.Id;
        }
    }
}
