using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Shared.Abstractions.Auth;
using Shared.Abstractions.CurrencyConverters;
using Shared.Abstractions.Time;
using Shared.Abstractions.UnitOfWork;
using Shared.Infrastructure.Auth;
using Shared.Infrastructure.CurrencyConverters;
using Shared.Infrastructure.DomainEvents;
using Shared.Infrastructure.Events;
using Shared.Infrastructure.Time;
using Shared.Infrastructure.UnitOfWork;
using System.Reflection;
using System.Text;

namespace Shared.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services,
                                                                 IConfiguration configuration,
                                                                 params Assembly[] assemblies)
        {
            var jwtSettings = new JwtSettings();
            configuration.GetSection("JwtSettings").Bind(jwtSettings);
            services.AddSingleton(jwtSettings);

            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddSingleton<ITokenManager, TokenManager>();
            services.AddSingleton<IHashingService, HashingService>();

            services.AddMediatR(configuration =>
                configuration.RegisterServicesFromAssemblies(assemblies));

            
            //services.AddCommands();
            //services.AddQueries();
            services.AddEvents(assemblies);
            services.AddDomainEvents(assemblies);
            //services.TryAddSingleton<IDispatcher, Dispatcher>();

            //services.AddDbContext<DbContext>(options =>
            //{
            //    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            //});

            services.AddUnitOfWork();

            services.AddSingleton(new UnitOfWorkTypeRegistry());

            services.AddHttpClient<ICurrencyConverter, CurrencyConverter>();

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
