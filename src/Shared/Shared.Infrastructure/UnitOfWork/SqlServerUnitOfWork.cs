﻿using Microsoft.EntityFrameworkCore;
using Serilog;
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

        public async Task CommitAndDispatchDomainEventsAsync<TEntity>(TEntity entity) where TEntity : Entity
        {
            await _domainEventDispatcher.DispatchDomainEvents(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> CommitChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
