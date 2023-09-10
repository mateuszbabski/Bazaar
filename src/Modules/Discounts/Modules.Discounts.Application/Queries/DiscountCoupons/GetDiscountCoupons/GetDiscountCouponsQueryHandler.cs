using MediatR;
using Modules.Discounts.Application.Dtos;
using Modules.Discounts.Domain.Entities;
using Modules.Discounts.Domain.Repositories;
using Shared.Abstractions.Queries;
using Shared.Abstractions.UserServices;
using Shared.Application.Exceptions;
using Shared.Application.Queries;

namespace Modules.Discounts.Application.Queries.DiscountCoupons.GetDiscountCoupons
{
    public class GetDiscountCouponsQueryHandler : IRequestHandler<GetDiscountCouponsQuery, PagedList<DiscountCouponDto>>
    {
        private readonly ICurrentUserService _userService;
        private readonly IDiscountCouponRepository _discountCouponRepository;
        private readonly IQueryProcessor<DiscountCoupon> _queryProcessor;

        public GetDiscountCouponsQueryHandler(ICurrentUserService userService,
                                              IDiscountCouponRepository discountCouponRepository,
                                              IQueryProcessor<DiscountCoupon> queryProcessor)
        {
            _userService = userService;
            _discountCouponRepository = discountCouponRepository;
            _queryProcessor = queryProcessor;
        }

        public async Task<PagedList<DiscountCouponDto>> Handle(GetDiscountCouponsQuery query, CancellationToken cancellationToken)
        {
            var userId = _userService.UserId;
            var userRole = _userService.UserRole;

            var baseQuery = await GetCouponsAccordingToRole(userId, userRole)
                ?? throw new ForbidException("You are now allowed to proceed this action");

            var sortedQuery = _queryProcessor.SortQuery(baseQuery.AsQueryable(), query.SortBy, query.SortDirection);

            var pagedDiscountCoupons = _queryProcessor.PageQuery(sortedQuery.AsEnumerable(), query.PageNumber, query.PageSize);

            var discountCouponListDto = DiscountCouponDto.CreateDtoFromObjects(pagedDiscountCoupons);

            var pagedDiscountCouponList = new PagedList<DiscountCouponDto>(discountCouponListDto,
                                                                           baseQuery.Count(),
                                                                           query.PageNumber,
                                                                           query.PageSize);

            return pagedDiscountCouponList;
        }

        private async Task<IEnumerable<DiscountCoupon>> GetCouponsAccordingToRole(Guid userId, string userRole) =>        
            userRole switch
            {
                "admin" => await _discountCouponRepository.GetAll(),
                "shop" => await _discountCouponRepository.GetAllByCreator(userId),
                "customer" => await _discountCouponRepository.GetAllTargetedForCustomer(userId),
                _ => throw new ForbidException("You are now allowed to proceed this action"),
            };
            
    }
}
