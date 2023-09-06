using MediatR;
using Modules.Discounts.Application.Contracts;
using Modules.Discounts.Domain.Repositories;
using Shared.Abstractions.UserServices;
using Shared.Application.Exceptions;

namespace Modules.Discounts.Application.Commands.DiscountCoupons.DisableDiscountCoupon
{
    public class DisableDiscountCouponCommandHandler : IRequestHandler<DisableDiscountCouponCommand, Unit>
    {
        private readonly ICurrentUserService _userService;
        private readonly IDiscountCouponRepository _discountCouponRepository;
        private readonly IDiscountsUnitOfWork _unitOfWork;

        public DisableDiscountCouponCommandHandler(ICurrentUserService userService,
                                                   IDiscountCouponRepository discountCouponRepository,
                                                   IDiscountsUnitOfWork unitOfWork)
        {
            _userService = userService;
            _discountCouponRepository = discountCouponRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DisableDiscountCouponCommand command, CancellationToken cancellationToken)
        {
            var userId = _userService.UserId;
            var discountCoupon = await _discountCouponRepository.GetDiscountCouponById(command.DiscountCouponId);

            if (discountCoupon == null || discountCoupon.Discount.CreatedBy != userId) 
            {
                throw new ForbidException("You are unable to proceed this action");
            }

            discountCoupon.DisableCoupon();
            await _unitOfWork.CommitChangesAsync();

            return Unit.Value;            
        }
    }
}
