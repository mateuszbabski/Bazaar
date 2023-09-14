using FluentValidation;

namespace Modules.Discounts.Application.Commands.Discounts.CreateDiscount
{
    public class CreateDiscountValidator : AbstractValidator<CreateDiscountCommand>
    {
        public CreateDiscountValidator()
        {
            RuleFor(c => c.DiscountValue).NotEmpty()
                                         .WithMessage("Discount Value cannot be empty");

            RuleFor(c => c.DiscountType).NotEmpty()
                                        .WithMessage("Discount Type cannot be empty");
        }
    }
}
