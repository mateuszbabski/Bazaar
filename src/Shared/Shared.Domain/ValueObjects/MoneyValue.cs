﻿using Shared.Domain.Exceptions;
using Shared.Domain.Rules;

namespace Shared.Domain.ValueObjects
{
    public record MoneyValue
    {
        public decimal Amount { get; }
        public string Currency { get; }

        public MoneyValue(decimal amount, string currency)
        {
            Amount = amount;
            Currency = currency.ToUpper();
        }

        public static MoneyValue Of(decimal amount, string currency)
        {
            if (new SystemMustAcceptsCurrencyRule(currency).IsBroken() || currency.Length != 3)
            {
                throw new InvalidPriceException("Invalid currency.");
            }

            if (amount <= 0)
            {
                throw new InvalidPriceException("Money amount value cannot be zero or negative.");
            }

            return new MoneyValue(amount, currency);
        }

        public static MoneyValue Of(MoneyValue value)
        {
            return new MoneyValue(value.Amount, value.Currency);
        }

        public static MoneyValue operator +(MoneyValue left, MoneyValue right)
        {
            if (new SameCurrencyMoneyOperationRule(left, right).IsBroken())
            {
                throw new Exception("Currency must be equal");
            }

            return new MoneyValue(left.Amount + right.Amount, left.Currency);
        }

        public static MoneyValue operator *(int number, MoneyValue right)
        {
            return new MoneyValue(number * right.Amount, right.Currency);
        }

        public static MoneyValue operator *(decimal number, MoneyValue right)
        {
            return new MoneyValue(number * right.Amount, right.Currency);
        }
    }
}
