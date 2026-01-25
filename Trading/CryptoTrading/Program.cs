using CryptoTrading;
using CryptoTrading.Extensions;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHttpClient("CryptoApi", client =>
{
    client.BaseAddress = new Uri("https://www.alphavantage.co/query");
    client.Timeout = TimeSpan.FromSeconds(10);
});
builder.Services.AddCryptoTradingServices(builder.Configuration);
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
