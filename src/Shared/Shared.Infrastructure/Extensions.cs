using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Abstractions.Auth;
using Shared.Abstractions.Context;
using Shared.Abstractions.CurrencyConverters;
using Shared.Abstractions.Events;
using Shared.Abstractions.Time;
using Shared.Infrastructure.Auth;
using Shared.Infrastructure.Context;
using Shared.Infrastructure.CurrencyConverters;
using Shared.Infrastructure.Time;

namespace Shared.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = new JwtSettings();
            configuration.GetSection("JwtSettings").Bind(jwtSettings);
            services.AddSingleton(jwtSettings);

            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddSingleton<ITokenManager, TokenManager>();
            services.AddSingleton<IHashingService, HashingService>();

            services.AddHttpClient<ICurrencyConverter, CurrencyConverter>();
            
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDomainEventsDispatcher, IDomainEventsDispatcher>();

            services.AddSqlServerContext<ApplicationDbContext>(configuration);

            return services;
        }        
    }
}
