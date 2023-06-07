using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Shared.Abstractions.DomainEvents;
using System.Reflection;

namespace Shared.Infrastructure.DomainEvents
{
    public static class Extensions
    {
        public static IServiceCollection AddDomainEvents(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
            services.AddScoped<IDomainEventsAccessor, DomainEventsAccessor>();

            //services.Scan(s => s.FromCallingAssembly()
            services.Scan(s => s.FromAssemblies(assemblies)
                    .AddClasses(c => c.AssignableTo(typeof(IDomainEventHandler<>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());

            return services;
        }
    }
}
