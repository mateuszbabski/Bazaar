using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Modules.Customers.Application.Commands.SignUpCustomerCommand;

namespace Modules.Customers.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddCustomersApplication(this IServiceCollection services)
        {
            services.AddScoped<IValidator<SignUpCommand>, SignUpValidator>();

            return services;
        }
            
    }
}
