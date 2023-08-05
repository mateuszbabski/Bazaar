using Microsoft.Extensions.DependencyInjection;
using Modules.Shippings.Application.Contracts;

namespace Modules.Shippings.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddShippingsApplication(this IServiceCollection services)
        {
            // TODO: Shippings application layer
            // TODO: Shippings dbcontext create and migrate
            // TODO: Shippings api layer controllers
            // TODO: Shippings application layer
            // TODO: Shippings domain and application layer tests
            // TODO: Shippings orchestrate CheckoutBasket event to find and get shipping method and create an order
            // TODO: Shippings create shipping from order
            return services;
        }
    }
}
