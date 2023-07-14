using Shared.Domain;

namespace Shared.Abstractions.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<int> CommitChangesAsync();
        Task CommitAndDispatchEventsAsync();
        Task CommitAndDispatchDomainEventsAsync<TEntity>(TEntity entity) 
            where TEntity : Entity;
    }
}
