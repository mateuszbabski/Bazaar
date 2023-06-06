using Shared.Domain;

namespace Shared.Abstractions.DomainEvents
{
    public interface IDomainEventsAccessor
    {
        IReadOnlyCollection<IDomainEvent> GetAllDomainEvents();

        void ClearAllDomainEvents();
    }
}
