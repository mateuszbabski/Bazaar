using MediatR;
using Modules.Discounts.Application.Contracts;
using Modules.Discounts.Domain.Entities;
using Modules.Discounts.Domain.Repositories;
using Modules.Discounts.Domain.ValueObjects;
using Modules.Products.Contracts.Interfaces;
using Shared.Abstractions.UserServices;
using Shared.Application.Exceptions;

namespace Modules.Discounts.Application.Commands.Discounts.CreateDiscount
{
    public class CreateDiscountCommandHandler : IRequestHandler<CreateDiscountCommand, Guid>
    {
        private readonly ICurrentUserService _userService;
        private readonly IDiscountRepository _discountRepository;
        private readonly IDiscountsUnitOfWork _unitOfWork;
        private readonly IProductChecker _productChecker;

        public CreateDiscountCommandHandler(ICurrentUserService userService,
                                            IDiscountRepository discountRepository,
                                            IDiscountsUnitOfWork unitOfWork, 
                                            IProductChecker productChecker)
        {
            _userService = userService;
            _discountRepository = discountRepository;
            _unitOfWork = unitOfWork;
            _productChecker = productChecker;
        }

        public async Task<Guid> Handle(CreateDiscountCommand command, CancellationToken cancellationToken)
        {
            var userId = _userService.UserId;
            var userRole = _userService.UserRole;

            if (userId == Guid.Empty || userRole == "customer") 
            {
                throw new ForbidException("You dont have an access to proceed this action");
            }

            if (userRole == "shop"
                && command.DiscountType.ToString() == "AssignedToProduct"
                && command.DiscountTargetId != Guid.Empty)
            {                
                if (!await _productChecker.IsProductExisting(userId, command.DiscountTargetId))
                    throw new ForbidException("You dont have an access to proceed this action or you provided wrong data");
            }
            // TODO: move rules checker to discounttarget domain
            BusinessRulesChecker(userId, userRole, command.DiscountType, command.DiscountTargetId);

            var discountTarget = DiscountTarget.CreateDiscountTarget(command.DiscountType,
                                                                     command.DiscountTargetId);

            var discount = CreateDiscount(userId,
                                          command.IsPercentageDiscount,
                                          command.DiscountValue,
                                          command?.Currency,
                                          discountTarget);
            
            await _discountRepository.Add(discount);
            await _unitOfWork.CommitAndDispatchDomainEventsAsync(discount);
            return discount.Id;
        }

        private static void BusinessRulesChecker(Guid userId,
                                                 string userRole,
                                                 DiscountType discountType,
                                                 Guid? discountTargetId)
        {
            if (userRole == "admin" && (discountType.ToString() == "AssignedToProduct"
                || discountType.ToString() == "AssignedToVendors"
                || discountType.ToString() == "AssignedToCustomer"))
            {
                throw new ForbidException("You dont have an access to proceed this action");
            }

            if (userRole == "shop" && (discountType.ToString() == "AssignedToAllProducts"
                || discountType.ToString() == "AssignedToShipping"
                || discountType.ToString() == "AssignedToOrderTotal"))
            {
                throw new ForbidException("You dont have an access to proceed this action");
            }

            if (userRole == "shop" && discountType.ToString() == "AssignedToVendors" && userId != discountTargetId)
            {
                throw new ForbidException("You dont have an access to proceed this action");
            }

            if (userRole == "shop"
                && discountType.ToString() == "AssignedToProduct"
                && discountTargetId == Guid.Empty)
            {
                throw new ForbidException("You dont have an access to proceed this action");
            }
        }

        private static Discount CreateDiscount(Guid creator,
                                               bool isPercentageDiscount,
                                               decimal discountValue,
                                               string? currency,
                                               DiscountTarget discountTarget)
        {
            if (isPercentageDiscount)
            {
                return Discount.CreatePercentageDiscount(creator,
                                                         discountValue,
                                                         discountTarget);
            }

            return Discount.CreateValueDiscount(creator,
                                                discountValue,
                                                currency!,
                                                discountTarget);          
        }
    }
}
