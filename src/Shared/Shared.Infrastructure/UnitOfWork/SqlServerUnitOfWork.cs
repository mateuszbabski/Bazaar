using Microsoft.EntityFrameworkCore;
using Shared.Abstractions.DomainEvents;
using Shared.Abstractions.UnitOfWork;
using Shared.Domain;

namespace Shared.Infrastructure.UnitOfWork
{
    public abstract class SqlServerUnitOfWork<T> : IUnitOfWork where T : DbContext
    {
        private readonly T _dbContext;
        private readonly IDomainEventDispatcher _domainEventDispatcher;

        protected SqlServerUnitOfWork(T dbContext, IDomainEventDispatcher domainEventDispatcher)
        {
            _dbContext = dbContext;
            _domainEventDispatcher = domainEventDispatcher;
        }

        public async Task CommitAndDispatchEventsAsync()
        {
            await _domainEventDispatcher.DispatchDomainEventsAsync();
            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> CommitChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
