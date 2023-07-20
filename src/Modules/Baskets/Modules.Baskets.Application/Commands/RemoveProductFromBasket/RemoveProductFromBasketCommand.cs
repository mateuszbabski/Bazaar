using MediatR;

namespace Modules.Baskets.Application.Commands.RemoveProductFromBasket
{
    public class RemoveProductFromBasketCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
