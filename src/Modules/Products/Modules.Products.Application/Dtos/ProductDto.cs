using Modules.Products.Domain.Entities;

namespace Modules.Products.Application.Dtos
{
    public record ProductDto
    {
        public Guid Id { get; init; }
        public string ProductName { get; init; }
        public string ProductCategory { get; init; }
        public decimal Price { get; init; }
        public string Currency { get; init; }

        internal static IEnumerable<ProductDto> CreateDtoFromObject(List<Product> products)
        {
            var productList = new List<ProductDto>();

            foreach (var product in products)
            {
                var productDto = new ProductDto()
                {
                    Id = product.Id,
                    ProductName = product.ProductName,
                    ProductCategory = product.ProductCategory.CategoryName,
                    Price = product.Price.Amount,
                    Currency = product.Price.Currency
                };

                productList.Add(productDto);
            }

            return productList;
        }
    }
}
