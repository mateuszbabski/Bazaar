using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Shared.Abstractions.Auth;
using Shared.Abstractions.Context;
using Shared.Abstractions.CurrencyConverters;
using Shared.Abstractions.Events;
using Shared.Abstractions.Time;
using Shared.Infrastructure.Auth;
using Shared.Infrastructure.Context;
using Shared.Infrastructure.CurrencyConverters;
using Shared.Infrastructure.DomainEvents;
using Shared.Infrastructure.Events;
using Shared.Infrastructure.Mediation;
using Shared.Infrastructure.Time;
using System.Text;

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

            services.AddMediation(configuration);
            services.AddEvents();
            services.AddDomainEvents();

            services.AddHttpClient<ICurrencyConverter, CurrencyConverter>();
            
            services.AddSqlServerContext<DbContext>(configuration);

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
                    };
                });

            return services;
        }        
    }
}
