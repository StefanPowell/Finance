using CryptoTrading.Interface;

namespace CryptoTrading;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly ICryptoTradingService _cryptoTradingService;

    public Worker(ILogger<Worker> logger,
                  ICryptoTradingService cryptoTradingService)
    {
        _logger = logger;
        _cryptoTradingService = cryptoTradingService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            await _cryptoTradingService.StartAsync();
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred starting/running Crypto Trading Application.");
        }
        finally
        {
           await _cryptoTradingService.StopAsync();
        }
    }
}
