using CryptoTrading.Persistence.Repository;
using CryptoTrading.Persistence.Repository.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoTrading.Persistence.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection CryptoTradingPersistentLayer(this IServiceCollection services, IConfiguration config)
    {
        return services.AddTransient<IDataRepository, DataRepository>();
    }
}
