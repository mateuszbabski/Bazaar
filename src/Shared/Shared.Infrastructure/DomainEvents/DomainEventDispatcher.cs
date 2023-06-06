using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Shared.Abstractions.DomainEvents;
using Shared.Domain;
using static System.Formats.Asn1.AsnWriter;
using System.Threading;

namespace Shared.Infrastructure.DomainEvents
{
    public sealed class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IDomainEventsAccessor _domainEventsAccessor;

        public DomainEventDispatcher(IServiceProvider serviceProvider,
                                     IDomainEventsAccessor domainEventsAccessor)
        {
            _serviceProvider = serviceProvider;
            _domainEventsAccessor = domainEventsAccessor;
        }

        public Task DispatchAsync(IDomainEvent @event, CancellationToken cancellationToken = default)
            => DispatchAsync(cancellationToken, @event);

        public Task DispatchAsync(IDomainEvent[] events, CancellationToken cancellationToken = default)
            => DispatchAsync(cancellationToken, events);

        public Task DispatchEventAsync()
            => DispatchEventAsync();

        private async Task DispatchEventAsync(CancellationToken cancellationToken = default)
        {
            var domainEvents = _domainEventsAccessor.GetAllDomainEvents();

            if (domainEvents is null || !domainEvents.Any())
                return;

            _domainEventsAccessor.ClearAllDomainEvents();

            using var scope = _serviceProvider.CreateScope();
            foreach (var domainEvent in domainEvents)
            {
                var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(domainEvent.GetType());
                var handlers = scope.ServiceProvider.GetServices(handlerType);

                var tasks = handlers.Select(x => (Task)handlerType
                .GetMethod(nameof(IDomainEventHandler<IDomainEvent>.HandleAsync))
                    ?.Invoke(x, new object[] { domainEvent, cancellationToken }));

                await Task.WhenAll(tasks);
            }    
        }

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
