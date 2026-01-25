using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTrading.Models.Requests;

public class CurrencyExchangeRate
{
    public string function { get; set; }
    public string fromCurrency { get; set; }
    public string toCurrency { get; set; }
    public string apiKey { get; set; }
}
