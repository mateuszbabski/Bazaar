using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Modules.Shops.Application.Commands.SignUpShop;
using Modules.Shops.Application.Services;
using Modules.Shops.Domain.Entities;
using Shared.Application.Queries;

namespace Modules.Shops.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddShopsApplication(this IServiceCollection services)
        {
            services.AddScoped<IValidator<SignUpShopCommand>, SignUpShopValidator>();

            services.AddQueryProcessor<ShopQueryProcessor, Shop>();

            return services;
        }
    }
}
