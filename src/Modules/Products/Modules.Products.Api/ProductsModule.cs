using Shared.Abstractions.Modules;

namespace Modules.Products.Api
{
    internal class ProductsModule : IModule
    {
        public string Name { get; } = "Products";
    }
}
