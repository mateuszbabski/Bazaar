using Modules.Products.Domain.Events;
using Modules.Products.Domain.ValueObjects;
using Shared.Domain;
using Shared.Domain.ValueObjects;

namespace Modules.Products.Domain.Entities
{
    public class Product : Entity, IAggregateRoot
    {
        public ProductId Id { get; private set; }
        public ProductName ProductName { get; private set; }
        public ProductDescription ProductDescription { get; private set; }
        public ProductCategory ProductCategory { get; private set; }
        public MoneyValue Price { get; private set; }
        public Weight WeightPerUnit { get; private set; }
        public ProductUnit Unit { get; private set; }
        public ProductShopId ShopId { get; private set; }
        public bool IsAvailable { get; private set; } = true;

        private Product() { }
        internal Product(ProductName productName,
                         ProductDescription productDescription,
                         ProductCategory productCategory,
                         Weight weightPerUnit,
                         MoneyValue price,
                         ProductUnit unit,
                         ProductShopId shopId)
        {
            Id = new ProductId(Guid.NewGuid());
            ProductName = productName;
            ProductDescription = productDescription;
            ProductCategory = productCategory;
            WeightPerUnit = weightPerUnit;
            Price = price;
            Unit = unit;
            ShopId = shopId;
            IsAvailable = true;
        }
        internal static Product CreateProduct(ProductName productName,
                                              ProductDescription productDescription,
                                              ProductCategory productCategory,
                                              Weight weightPerUnit,
                                              MoneyValue price,
                                              ProductUnit unit,
                                              ProductShopId shopId)
        {
            var product = new Product(productName,
                                      productDescription,
                                      productCategory,
                                      weightPerUnit,
                                      price,
                                      unit,
                                      shopId);

            product.AddDomainEvent(new ProductAddedToShopDomainEvent(product));

            return product;
        }

        internal void ChangeDetails(string productName, string productDescription, string unit)
        {
            SetName(productName);
            SetDescription(productDescription);
            SetUnit(unit);

            this.AddDomainEvent(new ProductDetailsChangedDomainEvent(this));
        }

        internal void ChangeAvailability()
        {
            if (IsAvailable == true)
            {
                Remove();
            }
            else
            {
                Restore();
            }
        }

        internal void SetPrice(MoneyValue price)
        {
            Price = price;
            this.AddDomainEvent(new ProductPriceChangedDomainEvent(this));
        }

        internal void SetUnit(string unit)
        {
            if (!string.IsNullOrEmpty(unit))
                Unit = new ProductUnit(unit);
        }

        internal void SetName(string productName)
        {
            if (!string.IsNullOrEmpty(productName))
                ProductName = new ProductName(productName);
        }

        internal void SetDescription(string productDescription)
        {
            if (!string.IsNullOrEmpty(productDescription))
                ProductDescription = new ProductDescription(productDescription);
        }

        public MoneyValue GetPrice()
        {
            return Price;
        }

        internal void Remove()
        {
            IsAvailable = false;
            this.AddDomainEvent(new ProductRemovedFromShopDomainEvent(this));
        }

        internal void Restore()
        {
            IsAvailable = true;
            this.AddDomainEvent(new ProductRestoredDomainEvent(this));
        }
    }
}
