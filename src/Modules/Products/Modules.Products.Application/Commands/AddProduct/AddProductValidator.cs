using FluentValidation;

namespace Modules.Products.Application.Commands.AddProduct
{
    public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
    {
        public AddProductCommandValidator()
        {
            RuleFor(c => c.ProductName).NotEmpty()
                                       .WithMessage("Product name cannot be empty");

            RuleFor(c => c.ProductDescription).NotEmpty()
                                              .WithMessage("Description cannot be empty");

            RuleFor(c => c.ProductCategory).NotEmpty()
                                           .WithMessage("Category cannot be empty");

            RuleFor(c => c.Amount).NotEmpty()
                                  .WithMessage("Amount field cannot be empty");

            RuleFor(c => c.Currency).NotEmpty()
                                    .WithMessage("Currency field cannot be empty");

            RuleFor(c => c.WeightPerUnit).NotEmpty()
                                         .WithMessage("Weight field cannot be empty");

            RuleFor(c => c.Unit).NotEmpty()
                                .WithMessage("Unit field cannot be empty");
        }
    }
}
