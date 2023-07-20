using Shared.Abstractions.CurrencyConverters;
using System.Net.Http;
using System.Net.Http.Json;

namespace Shared.Infrastructure.CurrencyConverters
{
    internal sealed class CurrencyConverter : ICurrencyConverter
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CurrencyConverter(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<decimal> GetConversionRate(string from, string to)
        {
            if (from == to) return 1;

            var httpClient = _httpClientFactory.CreateClient();

            var uriString =
                string.Format($"https://api.frankfurter.app/latest?amount=1&from={@from}&to={@to}", from, to);

            var response = await httpClient.GetAsync(uriString);
            response.EnsureSuccessStatusCode();

            var serializedResponse = await response.Content.ReadFromJsonAsync<ConversionResult>();

            return serializedResponse.Rates.Values.First();
        }

        public async Task<decimal> GetConvertedPrice(decimal amount, string from, string to)
        {
            if (from == to) return amount;

            var httpClient = _httpClientFactory.CreateClient();

            var uriString =
                string.Format($"https://api.frankfurter.app/latest?amount={@amount}&from={@from}&to={@to}", amount, from, to);

            var response = await httpClient.GetAsync(uriString);
            response.EnsureSuccessStatusCode();

            var serializedResponse = await response.Content.ReadFromJsonAsync<ConversionResult>();

            return serializedResponse.Rates.Values.First();
        }
    }
}
