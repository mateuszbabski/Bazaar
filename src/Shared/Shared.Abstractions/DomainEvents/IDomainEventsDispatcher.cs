using Shared.Domain;

namespace Shared.Abstractions.DomainEvents
{
    public interface IDomainEventDispatcher
    {
        Task DispatchDomainEventsAsync(CancellationToken cancellationToken = default);
        Task DispatchDomainEvents<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
            where TEntity : Entity;
    }
}
