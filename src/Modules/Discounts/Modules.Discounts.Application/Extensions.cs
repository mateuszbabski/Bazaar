using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Modules.Discounts.Application.Commands.Discounts.CreateDiscount;
using Shared.Application.Queries;
using Modules.Discounts.Application.Services;
using Modules.Discounts.Domain.Entities;

namespace Modules.Discounts.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddDiscountsApplication(this IServiceCollection services)
        {
            services.AddScoped<IValidator<CreateDiscountCommand>, CreateDiscountValidator>();
            //services.AddScoped<IValidator<CreateDiscountCouponCommand>, CreateDiscountCouponValidator>();

            services.AddQueryProcessor<DiscountQueryProcessor, Discount>();
            services.AddQueryProcessor<DiscountCouponQueryProcessor, DiscountCoupon>();

            return services;
        }
    }
}
