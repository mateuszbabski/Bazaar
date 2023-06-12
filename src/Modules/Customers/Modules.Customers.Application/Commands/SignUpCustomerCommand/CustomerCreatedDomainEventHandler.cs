using MediatR;
using Modules.Customers.Domain.Events;
using Serilog;
using Shared.Abstractions.Time;

namespace Modules.Customers.Application.Commands.SignUpCustomerCommand
{
    internal sealed class CustomerCreatedDomainEventHandler
        : INotificationHandler<CustomerCreatedDomainEvent>
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public CustomerCreatedDomainEventHandler(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }
        public async Task Handle(CustomerCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            Log.Information("Customer created at: {@date}", _dateTimeProvider.UtcNow);
            // create payment(invoice);
        }
    }
}
