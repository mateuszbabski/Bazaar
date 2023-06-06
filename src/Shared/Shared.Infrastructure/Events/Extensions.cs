using Microsoft.Extensions.DependencyInjection;
using Shared.Abstractions.DomainEvents;
using Shared.Abstractions.Events;
using System.Reflection;

namespace Shared.Infrastructure.Events
{
    public static class Extensions
    {
        public static IServiceCollection AddEvents(this IServiceCollection services)
        {
            services.AddSingleton<IEventDispatcher, EventDispatcher>();

            services.Scan(s => s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                .AddClasses(c => c.AssignableTo(typeof(IEventHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            return services;
        }
    }
}
