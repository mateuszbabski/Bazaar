using Shared.Domain;

namespace Shared.Abstractions.DomainEvents
{
    public interface IDomainEventDispatcher
    {
        Task DispatchAsync(IDomainEvent @event, CancellationToken cancellationToken = default);
        Task DispatchAsync(IDomainEvent[] events, CancellationToken cancellationToken = default);
        Task DispatchEventAsync();

        Task DispatchDomainEventsAsync(Entity entity, CancellationToken cancellationToken = default);
    }

    public interface IDomainEventDispatcher<T> : IDomainEventDispatcher
    {
        //Task DispatchDomainEventsAsync(T entity, CancellationToken cancellationToken = default);
    }
}
