using Shared.Abstractions.Modules;

namespace Modules.Customers.Api
{
    internal class CustomersModule : IModule
    {
        public string Name { get; } = "Customers";
    }
}
