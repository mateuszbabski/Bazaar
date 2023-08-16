using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Modules.Shippings.Application.Commands.ShippingMethods.AddShippingMethod;
using Modules.Shippings.Application.Services;
using Modules.Shippings.Domain.Entities;
using Shared.Application.Queries;

namespace Modules.Shippings.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddShippingsApplication(this IServiceCollection services)
        {
            // TODO: Shippings application layer
            // TODO: Shippings api layer controllers
            // TODO: Shippings application layer
            // TODO: Shippings orchestrate CheckoutBasket event to find and get shipping method and create an order
            // TODO: Shippings create shipping from order

            services.AddScoped<IValidator<AddShippingMethodCommand>, AddShippingMethodCommandValidator>();

            //services.AddQueryProcessor<ShippingQueryProcessor, Shipping>();
            services.AddQueryProcessor<ShippingMethodQueryProcessor, ShippingMethod>();

            return services;
        }
    }
}
