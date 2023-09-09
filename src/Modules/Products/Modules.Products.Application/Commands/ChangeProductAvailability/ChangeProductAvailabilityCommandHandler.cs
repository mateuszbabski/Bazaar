using MediatR;
using Modules.Products.Application.Contracts;
using Modules.Products.Domain.Repositories;
using Shared.Abstractions.UserServices;
using Shared.Application.Exceptions;

namespace Modules.Products.Application.Commands.ChangeProductAvailability
{
    public class ChangeProductAvailabilityCommandHandler : IRequestHandler<ChangeProductAvailabilityCommand, Guid>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IProductRepository _productRepository;
        private readonly IProductsUnitOfWork _unitOfWork;

        public ChangeProductAvailabilityCommandHandler(ICurrentUserService currentUserService,
                                                       IProductRepository productRepository,
                                                       IProductsUnitOfWork unitOfWork)
        {
            _currentUserService = currentUserService;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Guid> Handle(ChangeProductAvailabilityCommand command, CancellationToken cancellationToken)
        {
            var shopId = _currentUserService.UserId;

            var product = await _productRepository.GetById(command.Id);

            if (product == null || product.ShopId != shopId) 
            {
                throw new NotFoundException("Product not found");
            }

            product.ChangeAvailability();

            await _unitOfWork.CommitAndDispatchDomainEventsAsync(product);

            return product.Id;
        }
    }
}
