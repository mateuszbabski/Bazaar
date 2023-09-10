using MediatR;
using Modules.Discounts.Application.Dtos;
using Shared.Application.Queries;

namespace Modules.Discounts.Application.Queries.DiscountCoupons.GetDiscountCoupons
{
    public class GetDiscountCouponsQuery : IRequest<PagedList<DiscountCouponDto>>
    {
        #nullable enable
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SortBy { get; set; }
        public string SortDirection { get; set; } = "ASC";
    }
}
