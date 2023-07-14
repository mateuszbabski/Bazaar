using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Abstractions.DomainEvents;
using Shared.Abstractions.UnitOfWork;
using Shared.Application.Exceptions;
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
            try
            {
                await _dbContext.Database.BeginTransactionAsync();
                await _domainEventDispatcher.DispatchDomainEvents(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (TransactionFailedException)
            {
                await _dbContext.Database.RollbackTransactionAsync();
                throw new TransactionFailedException("Transaction failed");
            }
            finally
            {
                await _dbContext.Database.CloseConnectionAsync();
                _dbContext.Dispose();
            }
        }

        public async Task<int> CommitChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
