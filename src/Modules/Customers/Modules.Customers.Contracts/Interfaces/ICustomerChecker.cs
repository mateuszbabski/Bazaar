using Modules.Customers.Domain.ValueObjects;
using Shared.Domain.ValueObjects;

namespace Modules.Customers.Contracts.Interfaces
{
    public interface ICustomerChecker
    {
        Task<Email> GetCustomersEmail(CustomerId id);
    }
}
