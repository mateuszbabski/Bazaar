using MediatR;

namespace Modules.Products.Application.Commands.ChangeProductPrice
{
    public class ChangeProductPriceCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}
