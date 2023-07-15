using Modules.Customers.Domain.Entities;
using Shared.Domain;

namespace Modules.Customers.Domain.Events
{
    public sealed record CustomerCreatedDomainEvent(Customer Customer) : IDomainEvent
    {
    }
}
