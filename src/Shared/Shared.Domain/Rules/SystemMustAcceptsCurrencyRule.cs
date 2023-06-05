using Shared.Domain.ValueObjects;

namespace Shared.Domain.Rules
{
    public class SystemMustAcceptsCurrencyRule : Currency, IBusinessRule
    {
        private readonly string _currency;

        public SystemMustAcceptsCurrencyRule(string currency)
        {
            _currency = currency;
        }

        public string Message => "Money value must have valid currency";
        public bool IsBroken() => !CurrencyList.Contains(_currency, StringComparer.OrdinalIgnoreCase);
    }
}
