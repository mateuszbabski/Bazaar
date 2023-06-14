using Microsoft.Extensions.DependencyInjection;
using Shared.Abstractions.UserServices;

namespace Shared.Infrastructure.UserServices
{
    public static class Extensions
    {
        public static IServiceCollection AddCurrentUserProvider(this IServiceCollection services)
        {
            services.AddSingleton<ICurrentUserService, CurrentUserService>();

            return services;
        }
    }
}
