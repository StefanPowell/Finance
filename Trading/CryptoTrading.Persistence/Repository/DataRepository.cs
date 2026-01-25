using CryptoTrading.Models.Response;
using CryptoTrading.Persistence.Repository.Abstractions;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CryptoTrading.Persistence.Repository;

public class DataRepository : IDataRepository
{
    private readonly string _connectionString;

    public DataRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public SqlConnection CreateConnection()
    {
        SqlConnection connection = new(_connectionString);
        connection.Open();
        return connection;
    }

    public async Task<IEnumerable<string>> GetCryptoWatchList()
    {
        using (var sql = CreateConnection())
        {

            IEnumerable<string> allCryptoInWatchlist = await sql.QueryAsync<string>(
                "dbo.sp_Crypto_Get_AllCryptoCurrenciesInWatchlist",
                null,
                commandType: CommandType.StoredProcedure
            );

            return allCryptoInWatchlist;
        }
    }

    public async Task UpdateCryptoPrice(RealtimeCurrencyExchangeRate realtimeCurrencyExchangeRate)
    {
        using (var sql = CreateConnection())
        {

            var parameters = new
            {
                @FromCurrencyCode = realtimeCurrencyExchangeRate.fromCurrecnyCode,
                @ToCurrecnyCode = realtimeCurrencyExchangeRate.toCurrecnyCode,
                @ExchangeRate = realtimeCurrencyExchangeRate.exchangeRate,
                @LastRefreshed = realtimeCurrencyExchangeRate.lastRefreshed,   
                @BidPrice = realtimeCurrencyExchangeRate.bidPrice,
                @AskPrice = realtimeCurrencyExchangeRate.askPrice
            };

            await sql.ExecuteAsync(
                "dbo.sp_Crypto_Update_CryptoCurrencyPriceInWatchlist",
                parameters,
                commandType: CommandType.StoredProcedure
            );

        }
    }
}
