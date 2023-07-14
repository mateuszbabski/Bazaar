using MediatR;
using Serilog;
using Shared.Abstractions.DomainEvents;
using Shared.Domain;

namespace Shared.Infrastructure.DomainEvents
{
    public sealed class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IDomainEventsAccessor _domainEventsAccessor;
        private readonly IMediator _mediator;

        public DomainEventDispatcher(IDomainEventsAccessor domainEventsAccessor,
                                     IMediator mediator)
        {
            _domainEventsAccessor = domainEventsAccessor;
            _mediator = mediator;
        }

        public async Task DispatchDomainEvents<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
            where TEntity : Entity
        {
            var domainEvents = entity.DomainEvents.ToList();

            if (domainEvents is null || !domainEvents.Any())
                return;

            entity.ClearDomainEvents();

            foreach (var domainEvent in domainEvents)
            {
                Log.Information("Domain event: {@event}", domainEvent);
                await _mediator.Publish(domainEvent, cancellationToken);
            }
        }

        public async Task DispatchDomainEventsAsync(CancellationToken cancellationToken = default)
        {
            var domainEvents = _domainEventsAccessor.GetAllDomainEvents();

            if (domainEvents is null || !domainEvents.Any())
                return;

            _domainEventsAccessor.ClearAllDomainEvents();

            Log.Information("Domain event: {@event}", domainEvents);

            foreach (var domainEvent in domainEvents)
            {
                Log.Information("Domain event: {@event}", domainEvent);
                await ((dynamic)_mediator).Publish((dynamic)domainEvent, cancellationToken);
            }        
        }        
    }
}
