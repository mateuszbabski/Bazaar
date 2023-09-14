using Shared.Abstractions.Modules;

namespace Modules.Discounts.Api
{
    internal class DiscountsModule : IModule
    {
        public string Name { get; } = "Discounts";
    }
}
