using Modules.Customers.Domain.Repositories;
using Modules.Discounts.Contracts.Events;
using Shared.Abstractions.Events;

namespace Modules.Customers.Application.Events.EventHandlers
{
    public class DiscountCouponAddedForCustomerEventHandler : IEventHandler<DiscountCouponAddedForCustomerEvent>
    {
        private readonly ICustomerRepository _customerRepository;

        public DiscountCouponAddedForCustomerEventHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task Handle(DiscountCouponAddedForCustomerEvent notification, CancellationToken cancellationToken)
        {
            var email = await _customerRepository.GetCustomersEmail(notification.TargetId.Value);
            // send email to user
            throw new NotImplementedException();
        }
    }
}
