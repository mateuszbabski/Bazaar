using Microsoft.Extensions.DependencyInjection;
using Shared.Abstractions.Mediation.Commands;

namespace Shared.Infrastructure.Mediation.Commands
{
    public static class Extensions
    {
        public static IServiceCollection AddCommands(this IServiceCollection services)
        {
            services.AddSingleton<ICommandDispatcher, CommandDispatcher>();

            // get from another assembly interface
            services.Scan(s => s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies().ToList())
                .AddClasses(c => c.AssignableToAny(typeof(ICommandHandler<>), typeof(ICommandHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            return services;
        }
    }
}
