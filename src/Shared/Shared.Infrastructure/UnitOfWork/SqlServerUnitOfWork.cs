using Microsoft.EntityFrameworkCore;
using Shared.Abstractions.DomainEvents;
using Shared.Abstractions.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        //public async Task ExecuteAsync(Func<Task> action)
        //{
        //    await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        //    try
        //    {
        //        await action();
        //        await transaction.CommitAsync();
        //    }
        //    catch (Exception)
        //    {
        //        await transaction.RollbackAsync();
        //        throw;
        //    }
        //}
    }
}
