using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Shared.Abstractions.DomainEvents;
using Shared.Abstractions.UnitOfWork;
using Shared.Domain;
using Shared.Infrastructure.UnitOfWork;
using System.Reflection;

namespace Shared.Infrastructure.DomainEvents
{
    public static class Extensions
    {
        public static IServiceCollection AddDomainEventsDispatcher(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

            services.Scan(s => s.FromAssemblies(assemblies)
                    .AddClasses(c => c.AssignableTo(typeof(IDomainEventHandler<>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());

            return services;
        }

        public static IServiceCollection AddDomainEventsAccessor<T>(this IServiceCollection services) 
            where T : class, IDomainEventsAccessor
        {
            services.AddScoped<IDomainEventsAccessor, T>();
            services.AddScoped<T>();
            using var serviceProvider = services.BuildServiceProvider();
            serviceProvider.GetRequiredService<DomainEventsTypeRegistry>().Register<T>();

            return services;
        }


    }
}
