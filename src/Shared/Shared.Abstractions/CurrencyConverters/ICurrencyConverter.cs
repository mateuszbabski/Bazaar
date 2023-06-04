namespace Shared.Abstractions.CurrencyConverters
{
    public interface ICurrencyConverter
    {
        Task<decimal> GetConversionRate(string from, string to);
        Task<decimal> GetConvertedPrice(decimal amount, string from, string to);
    }
}
