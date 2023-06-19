using Modules.Customers.Domain.Entities;
using Shared.Domain.ValueObjects;

namespace Modules.Customers.Application.Dtos
{
    public record CustomerDetailsDto
    {
        public Guid CustomerId { get; init; }
        public string CustomerEmail { get; init; }
        public string CustomerName { get; init; }
        public string CustomerLastName { get; init; }
        public Address CustomerAddress { get; init; }
        public string CustomerTelephoneNumber { get; init; }

        public static CustomerDetailsDto CreateDtoFromObject(Customer customer)
        {
            return new CustomerDetailsDto
            {
                CustomerId = customer.Id,
                CustomerEmail = customer.Email,
                CustomerName = customer.Name,
                CustomerLastName = customer.LastName,
                CustomerAddress = customer.Address,
                CustomerTelephoneNumber = customer.TelephoneNumber
            };
        }
    }
}
