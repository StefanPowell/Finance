using CryptoTrading.Extensions;
using CryptoTrading.Interface;
using CryptoTrading.Models.Response;
using CryptoTrading.Persistence.Repository.Abstractions;
using Grpc.Net.Client;
using System.Timers;
using Trading;

namespace CryptoTrading.Services;

public class CryptoTradingService : ICryptoTradingService
{
    private readonly ILogger<CryptoTradingService> _logger;
    private readonly Crypto.CryptoClient _client;
    private readonly System.Timers.Timer _trackQueriesTimer;
    private readonly IDataRepository _dataRepository;
    private readonly IHttpClientFactory _httpClientFactory;

    public CryptoTradingService(ILogger<CryptoTradingService> logger,
        IHttpClientFactory httpClientFactory,
        ICryptoDataService cryptoDataService,
        IDataRepository dataRepository)
    {
        _logger = logger;
        _dataRepository = dataRepository;
        _httpClientFactory = httpClientFactory;

        _trackQueriesTimer = new(1000)
        {
            AutoReset = true
        };

        // Enable HTTP/2 without TLS (DEV ONLY)
        AppContext.SetSwitch(
            "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

        var channel = GrpcChannel.ForAddress("http://localhost:5191");
        _client = new Crypto.CryptoClient(channel);

        _trackQueriesTimer.Elapsed += UpdateCryptoList;
    }

    public async Task StartAsync()
    {
        try
        {
           
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send trade");
        }
    }

    private void UpdateCryptoList(object? sender, ElapsedEventArgs e)
    {
        try
        {
            QueryAllCryptoInWatchList().ConfigureAwait(false).GetAwaiter().GetResult();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }
    }

    public Task StopAsync()
    {
        return Task.CompletedTask;
    }

    public async Task QueryAllCryptoInWatchList()
    {
        IEnumerable<string> allCryptoSymbols = await _dataRepository.GetCryptoWatchList();
        HttpClient client = _httpClientFactory.CreateClient("CryptoApi");

        foreach (string symbol in allCryptoSymbols) 
        {

            string query =
            $"query?function=DIGITAL_CURRENCY_DAILY" +
            $"&symbol={symbol}" +
            $"&market=USD" +
            $"&apikey=8C9CDKSN2OCD9QZY";

            Task<RealtimeCurrencyExchangeRate> response = (await client.GetAsync(query)).ToRealtimeCurrencyExchangeRate();
            await _dataRepository.UpdateCryptoPrice(response.Result);
        }
    }
}
