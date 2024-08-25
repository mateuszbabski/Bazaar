using Shared.Abstractions.Modules;

namespace Modules.Orders.Api
{
    internal class OrdersModule : IModule
    {
        public string Name { get; } = "Orders";
    }
}
