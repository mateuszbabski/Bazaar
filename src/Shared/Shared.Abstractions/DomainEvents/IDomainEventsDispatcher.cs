using Shared.Domain;

namespace Shared.Abstractions.DomainEvents
{
    public interface IDomainEventDispatcher
    {
        Task DispatchDomainEventsAsync(CancellationToken cancellationToken = default);
    }
}
