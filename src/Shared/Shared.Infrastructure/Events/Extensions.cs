using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Shared.Abstractions.Events;
using System.Reflection;

namespace Shared.Infrastructure.Events
{
    public static class Extensions
    {
        public static IServiceCollection AddEvents(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddScoped<IEventDispatcher, EventDispatcher>();

            var mediatorHandlerTypes = services
                .Where(descriptor => descriptor.ServiceType.IsGenericType &&
                                     descriptor.ServiceType.GetGenericTypeDefinition() == typeof(INotificationHandler<>))
                .Select(descriptor => descriptor.ImplementationType);

            services.Scan(s => s.FromAssemblies(assemblies)
                    .AddClasses(c => c.AssignableTo(typeof(IEventHandler<>))
                    .Where(type => !mediatorHandlerTypes.Contains(type)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());

            return services;
        }
    }
}
