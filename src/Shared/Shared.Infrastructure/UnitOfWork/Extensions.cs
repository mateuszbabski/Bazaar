using Microsoft.Extensions.DependencyInjection;
using Shared.Abstractions.UnitOfWork;

namespace Shared.Infrastructure.UnitOfWork
{
    public static class Extensions
    {
        public static IServiceCollection AddUnitOfWork<T>(this IServiceCollection services) where T : class, IUnitOfWork
        {
            services.AddScoped<IUnitOfWork, T>();
            services.AddScoped<T>();
            using var serviceProvider = services.BuildServiceProvider();
            serviceProvider.GetRequiredService<UnitOfWorkTypeRegistry>().Register<T>();

            return services;
        }
    }
}
