using FluentValidation;

namespace Modules.Shippings.Application.Commands.ShippingMethods.AddShippingMethod
{
    public class AddShippingMethodCommandValidator : AbstractValidator<AddShippingMethodCommand>
    {
        public AddShippingMethodCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty()
                                       .WithMessage("Name cannot be empty");

            RuleFor(c => c.Amount).NotEmpty()
                                  .WithMessage("Amount field cannot be empty");

            RuleFor(c => c.Currency).NotEmpty()
                                    .WithMessage("Currency field cannot be empty");

            RuleFor(c => c.DurationInDays).NotEmpty()
                                          .WithMessage("Duration field cannot be empty");
        }
    }
}
