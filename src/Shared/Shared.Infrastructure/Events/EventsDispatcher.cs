using MediatR;
using Serilog;
using Shared.Abstractions.Events;
using Shared.Domain;

namespace Shared.Infrastructure.Events
{
    internal sealed class EventDispatcher : IEventDispatcher
    {
        private readonly IMediator _mediator;

        public EventDispatcher(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
            where TEvent : class, IEvent
        {
            Log.Information("Integration event: {@event}", @event);
            await _mediator.Publish(@event, cancellationToken);
            //using var scope = _serviceProvider.CreateScope();
            //var handlers = scope.ServiceProvider.GetServices<IEventHandler<TEvent>>();
            //var tasks = handlers.Select(x => x.Handle(@event, cancellationToken));
            //await Task.WhenAll(tasks);
        }
    }
}
