using Modules.Shops.Infrastructure.Context;
using Shared.Infrastructure.DomainEvents;

namespace Modules.Shops.Infrastructure.DomainEvents
{
    internal class ShopsDomainEventsAccessor : DomainEventsAccessor<ShopsDbContext>
    {
        public ShopsDomainEventsAccessor(ShopsDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
