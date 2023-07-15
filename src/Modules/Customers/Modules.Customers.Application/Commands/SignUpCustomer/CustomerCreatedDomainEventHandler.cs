using MediatR;
using Modules.Customers.Domain.Events;
using Serilog;
using Shared.Abstractions.DomainEvents;
using Shared.Abstractions.Time;

namespace Modules.Customers.Application.Commands.SignUpCustomer
{
    internal sealed class CustomerCreatedDomainEventHandler
        : IDomainEventHandler<CustomerCreatedDomainEvent>
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public CustomerCreatedDomainEventHandler(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }
        public async Task Handle(CustomerCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            Log.Information("Customer created at: {@date}", _dateTimeProvider.UtcNow);
            Log.Information("Customer created at: {@date}", _dateTimeProvider.LocalTimeNow);

            // send welcome email
            await Task.CompletedTask;
        }
    }
}
