using Modules.Discounts.Domain.ValueObjects;

namespace Modules.Discounts.Domain.Entities
{
    public class DiscountTarget
    {
        public DiscountType DiscountType { get; private set; }
        public Guid? TargetId { get; private set; } = Guid.Empty;        
    }
}
