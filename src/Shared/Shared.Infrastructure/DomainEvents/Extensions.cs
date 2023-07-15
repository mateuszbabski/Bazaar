using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shared.Abstractions.DomainEvents;
using System.Reflection;

namespace Shared.Infrastructure.DomainEvents
{
    public static class Extensions
    {
        public static IServiceCollection AddDomainEventsDispatcher(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

            var mediatorHandlerTypes = services
                .Where(descriptor => descriptor.ServiceType.IsGenericType &&
                                     descriptor.ServiceType.GetGenericTypeDefinition() == typeof(INotificationHandler<>))
                .Select(descriptor => descriptor.ImplementationType);

            services.Scan(s => s.FromAssemblies(assemblies)
                    .AddClasses(c => c.AssignableTo(typeof(IDomainEventHandler<>))
                    .Where(type => !mediatorHandlerTypes.Contains(type)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());

            return services;
        }
    }
}
