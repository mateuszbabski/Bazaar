using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Shared.Abstractions.Mediation.Commands;

namespace Shared.Infrastructure.Mediation.Commands
{
    public static class Extensions
    {
        public static IServiceCollection AddCommands(this IServiceCollection services)
        {
            services.TryAddSingleton<ICommandDispatcher, CommandDispatcher>();

            services.Scan(selector =>
            {
                selector.FromCallingAssembly()
                        .AddClasses(filter =>
                        {
                            filter.AssignableTo(typeof(ICommandHandler<,>));
                        })
                        .AsImplementedInterfaces()
                        .WithSingletonLifetime();
            });

            return services;
        }
    }
}
