using Modules.Customers.Domain.Entities;
using Shared.Domain;

namespace Modules.Customers.Domain.Events
{
    public record CustomerCreatedDomainEvent(Customer Customer) : IDomainEvent
    {
    }
}
