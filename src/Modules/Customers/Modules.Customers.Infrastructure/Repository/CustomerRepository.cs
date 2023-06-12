using Microsoft.EntityFrameworkCore;
using Modules.Customers.Domain.Entities;
using Modules.Customers.Domain.Repositories;
using Modules.Customers.Domain.ValueObjects;
using Modules.Customers.Infrastructure.Context;

namespace Modules.Customers.Infrastructure.Repository
{
    internal sealed class CustomerRepository : ICustomerRepository
    {
        private readonly CustomersDbContext _dbContext;

        public CustomerRepository(CustomersDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Customer> Add(Customer customer)
        {
            await _dbContext.Customers.AddAsync(customer);

            return customer;
        }

        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Customer> GetCustomerByEmail(string email)
        {
            return await _dbContext.Customers.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<Customer> GetCustomerById(CustomerId id)
        {
            return await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}

