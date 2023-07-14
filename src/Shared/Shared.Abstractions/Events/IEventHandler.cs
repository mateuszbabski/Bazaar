using MediatR;

namespace Shared.Abstractions.Events
{
    public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> 
        where TEvent : class, IEvent
    {        
    }
}
