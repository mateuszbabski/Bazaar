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

        public static Product CreateProduct(ProductName productName,
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

        public void ChangeProductDetails(string productName,
                                         string productDescription,
                                         string productCategory,
                                         string unit)
        {
            SetName(productName);
            SetDescription(productDescription);
            SetUnit(unit);
            SetCategory(productCategory);

            this.AddDomainEvent(new ProductDetailsChangedDomainEvent(this));
        }

        public void ChangeAvailability()
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

        public void ChangeProductPrice(decimal amount, string currency)
        {
            if(String.IsNullOrEmpty(currency))
            {
                currency = this.Price.Currency;
            }

            var newPrice = MoneyValue.Of(amount, currency);
            SetPrice(newPrice);
            this.AddDomainEvent(new ProductPriceChangedDomainEvent(this.Id, this.Price));
        }

        private void SetPrice(MoneyValue price)
        {
            Price = price;            
        }

        public void ChangeProductWeight(decimal weight)
        {
            if(!weight.Equals(null) && !string.IsNullOrEmpty(weight.ToString()))
            {
                SetWeight(weight);
            }
            
            this.AddDomainEvent(new ProductWeightChangedDomainEvent(this));
        }

        private void SetWeight(decimal weight)
        {
            if (!string.IsNullOrEmpty(weight.ToString()))
                WeightPerUnit = new Weight(weight);
        }

        private void SetUnit(string unit)
        {
            if (!string.IsNullOrEmpty(unit))
                Unit = new ProductUnit(unit);
        }

        private void SetName(string productName)
        {
            if (!string.IsNullOrEmpty(productName))
                ProductName = new ProductName(productName);
        }

        private void SetDescription(string productDescription)
        {
            if (!string.IsNullOrEmpty(productDescription))
                ProductDescription = new ProductDescription(productDescription);
        }

        private void SetCategory(string productCategory)
        {
            if (!string.IsNullOrEmpty(productCategory))
                ProductCategory = ProductCategory.Create(productCategory);
        }

        public MoneyValue GetPrice()
        {
            return Price;
        }

        private void Remove()
        {
            IsAvailable = false;
            this.AddDomainEvent(new ProductRemovedFromShopDomainEvent(this));
        }

        private void Restore()
        {
            IsAvailable = true;
            this.AddDomainEvent(new ProductRestoredDomainEvent(this));
        }
    }
}
