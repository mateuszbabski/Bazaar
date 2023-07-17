using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Modules.Products.Application.Commands.AddProduct;
using Modules.Products.Application.Services;
using Modules.Products.Domain.Entities;
using Shared.Application.Queries;

namespace Modules.Products.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddProductsApplication(this IServiceCollection services)
        {
            services.AddScoped<IValidator<AddProductCommand>, AddProductCommandValidator>();

            services.AddQueryProcessor<ProductQueryProcessor, Product>();

            return services;
        }
    }
}
