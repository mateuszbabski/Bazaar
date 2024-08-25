using Modules.Customers.Domain.Entities;

namespace Modules.Customers.Contracts
{
    public interface ICustomerChecker
    {
        Task<Customer> GetCustomerByIdToProcess(Guid customerId);
    }
}
