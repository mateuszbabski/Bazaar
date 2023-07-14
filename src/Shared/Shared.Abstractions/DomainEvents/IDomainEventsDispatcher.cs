using Shared.Domain;

namespace Shared.Abstractions.DomainEvents
{
    public interface IDomainEventDispatcher
    {
        Task DispatchDomainEvents<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
            where TEntity : Entity;
    }
}
