using MediatR;
using Modules.Discounts.Application.Dtos;

namespace Modules.Discounts.Application.Queries.Discounts.GetDiscountById
{
    public class GetDiscountByIdQuery : IRequest<DiscountDto>
    {
        public Guid Id { get; set; }
    }
}
