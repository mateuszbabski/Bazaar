using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Customers.Application;
using Modules.Customers.Infrastructure;
using Shared.Abstractions.Modules;

namespace Modules.Customers.Api
{
    internal class CustomersModule : IModule
    {
        public string Name { get; } = "Customers";
    }
}
