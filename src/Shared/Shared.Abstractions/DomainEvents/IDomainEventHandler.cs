using MediatR;
using Shared.Domain;

namespace Shared.Abstractions.DomainEvents
{
    public interface IDomainEventHandler<in TEvent> : INotificationHandler<TEvent> 
        where TEvent : class, IDomainEvent
    {
    }
}
