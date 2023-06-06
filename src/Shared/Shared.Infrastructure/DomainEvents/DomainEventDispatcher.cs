using Microsoft.Extensions.DependencyInjection;
using Shared.Abstractions.DomainEvents;

namespace Shared.Infrastructure.DomainEvents
{
    public sealed class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public DomainEventDispatcher(IServiceProvider serviceProvider)
            => _serviceProvider = serviceProvider;

        public Task DispatchAsync(IDomainEvent @event, CancellationToken cancellationToken = default)
            => DispatchAsync(cancellationToken, @event);

        public Task DispatchAsync(IDomainEvent[] events, CancellationToken cancellationToken = default)
            => DispatchAsync(cancellationToken, events);

        private async Task DispatchAsync(CancellationToken cancellationToken, params IDomainEvent[] events)
        {
            if (events is null || !events.Any())
            {
                return;
            }

            using var scope = _serviceProvider.CreateScope();
            foreach (var @event in events)
            {
                var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(@event.GetType());
                var handlers = scope.ServiceProvider.GetServices(handlerType);

                var tasks = handlers.Select(x => (Task)handlerType
                    .GetMethod(nameof(IDomainEventHandler<IDomainEvent>.HandleAsync))
                    ?.Invoke(x, new object[] { @event, cancellationToken }));

                await Task.WhenAll(tasks);
            }
        }
    }
}
