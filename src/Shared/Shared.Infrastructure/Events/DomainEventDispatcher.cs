﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Shared.Abstractions.Events;
using Shared.Domain;

namespace Shared.Infrastructure.Events
{
    internal sealed class DomainEventsDispatcher : IDomainEventsDispatcher
    {
        private readonly DbContext _dbContext;
        private readonly IMediator _mediator;

        public DomainEventsDispatcher(DbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task DispatchEventsAsync()
        {
            var domainEntities = _dbContext.ChangeTracker.Entries<Entity>()
                .Where(x => x.Entity.DomainEvents.Any())
                .Select(x => x.Entity)
                .ToList();

            var domainEvents = domainEntities.SelectMany(x => x.DomainEvents).ToList();

            if (domainEvents is null)
                return;

            domainEntities.ForEach(x => x.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
            {
                Log.Warning(domainEvent.ToString());
                await _mediator.Publish(domainEvent);
            }
        }
    }
}