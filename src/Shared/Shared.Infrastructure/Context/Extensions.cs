using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Infrastructure.Context
{
    public static class Extensions
    {
        public static IServiceCollection AddSqlServerContext<T>(this IServiceCollection services, IConfiguration configuration)
            where T : DbContext
        {
            services.AddDbContext<T>(x => x.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
