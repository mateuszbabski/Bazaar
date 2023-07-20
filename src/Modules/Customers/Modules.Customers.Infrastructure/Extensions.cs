using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Customers.Application.Contracts;
using Modules.Customers.Domain.Repositories;
using Modules.Customers.Infrastructure.Context;
using Modules.Customers.Infrastructure.Repository;
using Shared.Abstractions.UnitOfWork;
using Shared.Infrastructure.UnitOfWork;

namespace Modules.Customers.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddCustomersInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CustomersDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<ICustomersUnitOfWork, CustomersUnitOfWork>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            return services;
        }
    }
}
