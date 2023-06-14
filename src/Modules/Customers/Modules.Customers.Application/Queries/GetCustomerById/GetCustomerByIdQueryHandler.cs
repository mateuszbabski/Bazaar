using MediatR;
using Modules.Customers.Application.Dtos;
using Modules.Customers.Domain.Repositories;
using Shared.Application.Exceptions;

namespace Modules.Customers.Application.Queries.GetCustomerById
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDto>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<CustomerDto> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetCustomerById(request.Id)
                ?? throw new NotFoundException("User not found");

            var customerDto = CustomerDto.CreateDtoFromObjet(customer);

            return customerDto;
        }
    }
}
