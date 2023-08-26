using MediatR;
using Modules.Discounts.Domain.ValueObjects;

namespace Modules.Discounts.Application.Commands.Discounts.CreateDiscount
{
    public class CreateDiscountCommand : IRequest<Guid>
    {
        #nullable enable
        public decimal DiscountValue { get; set; }
        public bool IsPercentageDiscount { get; set; } = true;
        public string? Currency { get; set; } = string.Empty;
        public DiscountType DiscountType { get; set; }
        public Guid? DiscountTargetId { get; set; } = Guid.Empty;

    }
}
