using Microsoft.EntityFrameworkCore;
using Shared.Abstractions.Context;
using Shared.Abstractions.Events;

namespace Shared.Infrastructure.Context
{
    internal sealed class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;
        private readonly IDomainEventsDispatcher _domainEventsDispatcher;

        public UnitOfWork(DbContext dbContext, IDomainEventsDispatcher domainEventsDispatcher)
        {
            _dbContext = dbContext;
            _domainEventsDispatcher = domainEventsDispatcher;
        }

        public async Task<int> CommitAsync()
        {
            await _domainEventsDispatcher.DispatchEventsAsync();
            return await _dbContext.SaveChangesAsync();
        }
    }
}
