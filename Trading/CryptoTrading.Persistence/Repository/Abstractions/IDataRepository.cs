using CryptoTrading.Models.Response;

namespace CryptoTrading.Persistence.Repository.Abstractions;

public interface IDataRepository
{
    Task<IEnumerable<string>> GetCryptoWatchList();
    Task UpdateCryptoPrice(RealtimeCurrencyExchangeRate realtimeCurrencyExchangeRate);
}
