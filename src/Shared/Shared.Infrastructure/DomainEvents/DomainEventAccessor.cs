using Microsoft.EntityFrameworkCore;
using Shared.Abstractions.DomainEvents;
using Shared.Domain;

namespace Shared.Infrastructure.DomainEvents
{
    public class DomainEventsAccessor : IDomainEventsAccessor
    {
        private readonly DbContext _dbContext;

        public DomainEventsAccessor(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IReadOnlyCollection<IDomainEvent> GetAllDomainEvents()
        {
            var domainEntities = this._dbContext.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).ToList();

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
