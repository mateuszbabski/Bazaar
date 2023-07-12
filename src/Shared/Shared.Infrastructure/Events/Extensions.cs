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
            services.TryAddSingleton<IEventDispatcher, EventDispatcher>();

            services.Scan(s => s.FromAssemblies(assemblies)
                    //.AddClasses(c => c.AssignableTo(typeof(IEventHandler<>)))
                    .AddClasses(c => c.AssignableTo(typeof(INotificationHandler<>)))
                    .AsImplementedInterfaces()
                    .WithSingletonLifetime());

            return services;
        }
    }
}
