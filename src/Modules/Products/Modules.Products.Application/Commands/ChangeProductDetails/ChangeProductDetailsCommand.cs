using MediatR;

namespace Modules.Products.Application.Commands.ChangeProductDetails
{
    public class ChangeProductDetailsCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductCategory { get; set; }
        public string Unit { get; set; }
    }
}
