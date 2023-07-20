using MediatR;

namespace Modules.Products.Application.Commands.AddProductToBasket
{
    public class AddProductToBasketCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
    }
}
