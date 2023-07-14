using Microsoft.EntityFrameworkCore;
using Serilog;
using Shared.Abstractions.DomainEvents;
using Shared.Domain;

namespace Shared.Infrastructure.DomainEvents
{
    public class DomainEventsAccessor<T> : IDomainEventsAccessor where T : DbContext
    {
        private readonly T _dbContext;

        public DomainEventsAccessor(T dbContext)
        {
            _dbContext = dbContext;
        }

        public IReadOnlyCollection<IDomainEvent> GetAllDomainEvents()
        {
            Log.Information("dbContext: {@db}", _dbContext.GetType().Name);

            var domainEntities = this._dbContext.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any())
                .ToList();

            Log.Information("{@eventEntity}", domainEntities);

            return domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();
        }

        public void ClearAllDomainEvents()
        {
            var domainEntities = this._dbContext.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).ToList();

            domainEntities
                .ForEach(entity => entity.Entity.ClearDomainEvents());
        }
    }
}
