using Shared.Domain;
using Shared.Domain.ValueObjects;

namespace Modules.Discounts.Domain.Rules
{
    public class DiscountHasToHaveCurrencyIfItsNotPercentageDiscountRule : IBusinessRule
    {
        private readonly bool _isPercentageDiscount;
        private readonly Currency _currency;

        public DiscountHasToHaveCurrencyIfItsNotPercentageDiscountRule(bool isPercentageDiscount, Currency currency)
        {
            _isPercentageDiscount = isPercentageDiscount;
            _currency = currency;
        }

        public string Message => "Discount has to have currency";

        public bool IsBroken() => _isPercentageDiscount == false && _currency == null;
    }
}
