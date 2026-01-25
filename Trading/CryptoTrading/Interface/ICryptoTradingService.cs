using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTrading.Interface;

public interface ICryptoTradingService
{
    Task StartAsync();
    Task StopAsync();
}
