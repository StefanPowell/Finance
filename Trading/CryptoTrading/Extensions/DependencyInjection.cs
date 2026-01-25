using CryptoTrading.Interface;
using CryptoTrading.Services;

namespace CryptoTrading.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddCryptoTradingServices(this IServiceCollection services, IConfiguration config)
    {
        return services.AddSingleton<ICryptoTradingService, CryptoTradingService>();
    }
}
