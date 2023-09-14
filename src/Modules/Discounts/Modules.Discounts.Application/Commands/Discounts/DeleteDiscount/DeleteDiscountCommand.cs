using MediatR;

namespace Modules.Discounts.Application.Commands.Discounts.DeleteDiscount
{
    public class DeleteDiscountCommand : IRequest<Unit>
    {
        public Guid DiscountId { get; set; }
    }
}
