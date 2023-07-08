using Modules.Products.Domain.Entities;

namespace Modules.Products.Application.Dtos
{
    public record ProductDetailsDto
    {
        public Guid Id { get; init; }
        public string ProductName { get;  init; }
        public string ProductDescription { get; init; }
        public string ProductCategory { get; init; }
        public decimal ProductPrice { get; init; }
        public string ProductPriceCurrency { get; init; }
        public decimal WeightPerUnit { get; init; }
        public string Unit { get; init; }
        public Guid ShopId { get; init; }
        public bool IsAvailable { get; init; }

        internal static ProductDetailsDto CreateDtoFromObject(Product product)
        {
            return new ProductDetailsDto()
            {
                Id = product.Id,
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                ProductCategory = product.ProductCategory.CategoryName,
                ProductPrice = product.Price.Amount,
                ProductPriceCurrency = product.Price.Currency,
                WeightPerUnit = product.WeightPerUnit,
                Unit = product.Unit,
                ShopId = product.ShopId,
                IsAvailable = product.IsAvailable,
            };
        }
    }
}
