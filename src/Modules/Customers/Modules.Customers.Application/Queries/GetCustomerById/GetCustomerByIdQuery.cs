using MediatR;
using Modules.Customers.Application.Dtos;

namespace Modules.Customers.Application.Queries.GetCustomerById
{
    public class GetCustomerByIdQuery : IRequest<CustomerDto>
    {
        public Guid Id { get; set; }
    }
}
