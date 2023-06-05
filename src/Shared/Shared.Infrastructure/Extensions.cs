using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Abstractions.Context;
using Shared.Abstractions.CurrencyConverters;
using Shared.Abstractions.Events;
using Shared.Abstractions.Time;
using Shared.Infrastructure.Context;
using Shared.Infrastructure.CurrencyConverters;
using Shared.Infrastructure.Time;

namespace Shared.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            services.AddHttpClient<ICurrencyConverter, CurrencyConverter>();
            
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDomainEventsDispatcher, IDomainEventsDispatcher>();

            services.AddSqlServerContext<ApplicationDbContext>(configuration);

            return services;
        }        
    }
}
