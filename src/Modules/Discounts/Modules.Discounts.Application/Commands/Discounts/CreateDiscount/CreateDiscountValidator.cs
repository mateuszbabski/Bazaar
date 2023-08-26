using FluentValidation;

namespace Modules.Discounts.Application.Commands.Discounts.CreateDiscount
{
    public class CreateDiscountValidator : AbstractValidator<CreateDiscountCommand>
    {
        public CreateDiscountValidator()
        {
            RuleFor(c => c.DiscountValue).NotEmpty()
                                         .WithMessage("Product name cannot be empty");

            RuleFor(c => c.IsPercentageDiscount).NotEmpty()
                                                .WithMessage("Description cannot be empty");

            RuleFor(c => c.DiscountType).NotEmpty()
                                        .WithMessage("Category cannot be empty");
        }
    }
}
