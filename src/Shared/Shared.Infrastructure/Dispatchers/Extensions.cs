//using Microsoft.Extensions.DependencyInjection;
//using Shared.Abstractions.Dispatchers;
//using Shared.Abstractions.Events;
//using Shared.Abstractions.Mediation.Commands;
//using Shared.Abstractions.Mediation.Queries;

//namespace Shared.Infrastructure.Dispatchers
//{
//    public static class Extensions
//    {
//        public static IServiceCollection AddDispatchers(this IServiceCollection services)
//        {
//            services.AddScoped<IDispatcher, Dispatcher>();

//            services.Scan(s => s.FromCallingAssembly()
//                    .AddClasses(c => 
//                    {
//                        c.AssignableTo(typeof(ICommandHandler<,>));
//                        c.AssignableTo(typeof(ICommandHandler<>));
//                        c.AssignableTo(typeof(IQueryHandler<,>));
//                        c.AssignableTo(typeof(IEventHandler<>));
//                    })
//                    .AsImplementedInterfaces()
//                    .WithScopedLifetime());

//            return services;
//        }
//    }
//}
