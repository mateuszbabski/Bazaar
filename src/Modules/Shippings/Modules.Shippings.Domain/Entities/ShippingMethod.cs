using Modules.Shippings.Domain.Events;
using Modules.Shippings.Domain.Exceptions;
using Modules.Shippings.Domain.ValueObjects;
using Shared.Domain;
using Shared.Domain.ValueObjects;

namespace Modules.Shippings.Domain.Entities
{
    public class ShippingMethod : Entity
    {
        public ShippingMethodId Id { get; private set; }
        public ShippingMethodName Name { get; private set; }
        public MoneyValue BasePrice { get; private set; }
        public int DurationTimeInDays { get; private set; } = 1;
        public bool IsAvailable { get; private set; } = true;

        private ShippingMethod()
        {
            
        }

        internal ShippingMethod(ShippingMethodName name, MoneyValue basePrice, int durationTime)
        {
            Id = new ShippingMethodId(Guid.NewGuid());
            Name = name;
            BasePrice = basePrice;
            DurationTimeInDays = durationTime;
            IsAvailable = true;
        }

        public static ShippingMethod CreateNewShippingMethod(string name, MoneyValue basePrice, int durationTime)
        {
            if (durationTime <= 0)
            {
                throw new InvalidDurationTimeException();
            }

            var shippingMethod = new ShippingMethod(name, basePrice, durationTime);

            shippingMethod.AddDomainEvent(new NewShippingMethodCreatedDomainEvent(shippingMethod.Id));

            return shippingMethod;
        }

        public void UpdateDetails(string newName,
                                  decimal amount,
                                  string currency,
                                  int newDurationTime)
        {
            SetName(newName);
            SetPrice(amount, currency);
            SetDurationTime(newDurationTime);
        }

        private void SetName(string name)
        {
            if (!string.IsNullOrEmpty(name))
                this.Name = new ShippingMethodName(name);
        }

        private void SetPrice(decimal amount, string currency)
        {
            if (amount <= 0)
            {
                amount = this.BasePrice.Amount;
            }

            if (string.IsNullOrEmpty(currency))
            {
                currency = this.BasePrice.Currency;
            }

            var newPrice = MoneyValue.Of(amount, currency);
            this.BasePrice = newPrice;
        }

        private void SetDurationTime(int durationTime)
        {
            if (durationTime > 0)
                this.DurationTimeInDays = durationTime;
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

        private void Remove()
        {
            this.IsAvailable = false;
        }

        private void Restore()
        {
            this.IsAvailable = true;
        }
    }
}
