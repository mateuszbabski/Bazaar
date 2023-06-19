using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Modules.Shops.Application.Commands.SignUpShop;

namespace Modules.Shops.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddShopsApplication(this IServiceCollection services)
        {
            services.AddScoped<IValidator<SignUpShopCommand>, SignUpShopValidator>();
            return services;
        }
    }
}
