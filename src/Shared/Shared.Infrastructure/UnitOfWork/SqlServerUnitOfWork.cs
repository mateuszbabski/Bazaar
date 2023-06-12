using Microsoft.EntityFrameworkCore;
using Shared.Abstractions.DomainEvents;
using Shared.Abstractions.UnitOfWork;

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

        public async Task<int> CommitAsync()
        {
            await _domainEventDispatcher.DispatchEventAsync();
            return await _dbContext.SaveChangesAsync();
        }
    }
}
