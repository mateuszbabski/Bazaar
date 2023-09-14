using Modules.Customers.Domain.Entities;
using Modules.Customers.Domain.ValueObjects;
using Shared.Domain.ValueObjects;

namespace Modules.Customers.Domain.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> Add(Customer customer);
        Task<Customer> GetCustomerByEmail(string email);
        Task<Customer> GetCustomerById(CustomerId id);
        Task<Email> GetCustomersEmail(Guid id);
        Task Commit();
    }
}
