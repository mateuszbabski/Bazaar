using MediatR;
using Modules.Shops.Domain.Events;
using Serilog;
using Shared.Abstractions.DomainEvents;
using Shared.Abstractions.Time;

namespace Modules.Shops.Application.Commands.SignUpShop
{
    internal sealed class ShopCreatedDomainEventHandler
        : IDomainEventHandler<ShopCreatedDomainEvent>
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public ShopCreatedDomainEventHandler(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }
        public async Task Handle(ShopCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            Log.Information("Shop created at: {@date}", _dateTimeProvider.UtcNow);

            // send welcome email
        }
    }
}
