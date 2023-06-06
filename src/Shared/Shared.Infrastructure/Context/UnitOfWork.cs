using Microsoft.EntityFrameworkCore;
using Shared.Abstractions.Context;
using Shared.Abstractions.DomainEvents;
using Shared.Abstractions.Events;

namespace Shared.Infrastructure.Context
{
    internal sealed class UnitOfWork : IUnitOfWork
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
