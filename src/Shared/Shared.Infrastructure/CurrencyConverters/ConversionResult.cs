namespace Shared.Infrastructure.CurrencyConverters
{
    internal record ConversionResult
    {
        public decimal Amount { get; init; }
        public string Base { get; init; }
        public DateTime Date { get; init; }
        public Dictionary<string, decimal> Rates { get; init; }
    }
}
