﻿using System.Collections.ObjectModel;

namespace Shared.Domain.ValueObjects
{
    public class Currency
    {
        private readonly List<string> _currencies;
        public ReadOnlyCollection<string> CurrencyList
        {
            get
            {
                return _currencies.AsReadOnly();
            }
        }
        public Currency()
        {
            _currencies = new List<string>
            {
                "PLN",
                "EUR",
                "USD"
            };
        }
    }
}
