using Modules.Customers.Domain.Entities;

namespace Modules.Customers.Application.Dtos
{
    public record CustomerDto
    {
        public Guid CustomerId { get; init; }
        public string CustomerEmail { get; init; }

        public static CustomerDto CreateDtoFromObject(Customer customer)
        {
            return new CustomerDto
            {
                CustomerId = customer.Id, 
                CustomerEmail = customer.Email
            };
        }
    }
}
