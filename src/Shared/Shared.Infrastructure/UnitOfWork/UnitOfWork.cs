using Microsoft.EntityFrameworkCore;
using Shared.Abstractions.DomainEvents;
using Shared.Abstractions.UnitOfWork;

namespace Shared.Infrastructure.UnitOfWork
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;
        private readonly IDomainEventDispatcher _domainEventDispatcher;

        public UnitOfWork(DbContext dbContext, IDomainEventDispatcher domainEventDispatcher)
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
