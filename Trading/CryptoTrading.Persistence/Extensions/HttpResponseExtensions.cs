using CryptoTrading.Models.Response;
using System.Text.Json;

namespace CryptoTrading.Extensions;

public static class HttpResponseExtensions
{
    public static async Task<RealtimeCurrencyExchangeRate> ToRealtimeCurrencyExchangeRate(
        this HttpResponseMessage response
        )
    {
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement
                .GetProperty("Realtime Currency Exchange Rate");

            return new RealtimeCurrencyExchangeRate
            {
                fromCurrecnyCode = root.GetProperty("1. From_Currency Code").GetString()!,
                fromCurrencyName = root.GetProperty("2. From_Currency Name").GetString()!,
                toCurrecnyCode = root.GetProperty("3. To_Currency Code").GetString()!,
                toCurrencyName = root.GetProperty("4. To_Currency Name").GetString()!,
                exchangeRate = double.Parse(root.GetProperty("5. Exchange Rate").GetString()!),
                lastRefreshed = DateTime.Parse(root.GetProperty("6. Last Refreshed").GetString()!),
                timeZone = root.GetProperty("7. Time Zone").GetString()!,
                bidPrice = double.Parse(root.GetProperty("8. Bid Price").GetString()!),
                askPrice = double.Parse(root.GetProperty("9. Ask Price").GetString()!)
            };
    }

}
