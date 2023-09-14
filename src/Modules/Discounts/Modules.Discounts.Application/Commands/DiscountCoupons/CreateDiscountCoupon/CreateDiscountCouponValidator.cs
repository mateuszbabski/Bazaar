using FluentValidation;

namespace Modules.Discounts.Application.Commands.DiscountCoupons.CreateDiscountCoupon
{
    public class CreateDiscountCouponValidator : AbstractValidator<CreateDiscountCouponCommand>
    {
        public CreateDiscountCouponValidator()
        {
            RuleFor(c => c.DiscountId).NotEmpty()
                                         .WithMessage("Discount Id cannot be empty");
        }
    }
}
