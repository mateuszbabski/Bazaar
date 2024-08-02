using MediatR;
using Modules.Discounts.Application.Contracts;
using Modules.Discounts.Domain.Repositories;
using Shared.Abstractions.UserServices;
using Shared.Application.Exceptions;

namespace Modules.Discounts.Application.Commands.DiscountCoupons.CreateDiscountCoupon
{
    public class CreateDiscountCouponCommandHandler : IRequestHandler<CreateDiscountCouponCommand, Guid>
    {
        private readonly ICurrentUserService _userService;
        private readonly IDiscountCouponRepository _discountCouponRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IDiscountsUnitOfWork _unitOfWork;

        public CreateDiscountCouponCommandHandler(ICurrentUserService userService, 
                                                  IDiscountCouponRepository discountCouponRepository,
                                                  IDiscountRepository discountRepository,
                                                  IDiscountsUnitOfWork unitOfWork)
        {
            _userService = userService;
            _discountCouponRepository = discountCouponRepository;
            _discountRepository = discountRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateDiscountCouponCommand command, CancellationToken cancellationToken)
        {
            var userId = _userService.UserId;
            var discount = await _discountRepository.GetDiscountById(command.DiscountId);

            if (discount == null || discount.CreatedBy != userId) 
            { 
                throw new ForbidException("You are unable to proceed this action");            
            }

            var discountCoupon = discount.CreateNewDiscountCoupon(userId, command.StartsAt, command.ExpirationDate);

            await _discountCouponRepository.Add(discountCoupon);

            discount.AddCouponToDiscount(discountCoupon);

            await _unitOfWork.CommitAndDispatchDomainEventsAsync(discountCoupon);

            return discountCoupon.Id;
        }
    }
}
