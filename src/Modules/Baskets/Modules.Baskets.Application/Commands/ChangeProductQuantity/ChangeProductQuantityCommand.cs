using MediatR;

namespace Modules.Baskets.Application.Commands.ChangeProductQuantity
{
    public class ChangeProductQuantityCommand : IRequest<Unit>
    {
        public Guid BasketItemId { get; set; }
        public int Quantity { get; set; }
    }
}
