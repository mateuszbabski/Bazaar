using MediatR;

namespace Modules.Products.Application.Commands.AddProduct
{
    public class AddProductCommand : IRequest<Guid>
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductCategory { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "PLN";
        public decimal WeightPerUnit { get; set; }
        public string Unit { get; set; } = "Piece";
    }
}
