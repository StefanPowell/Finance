using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTrading.Models.Response;

public class RealtimeCurrencyExchangeRate
{
    public string fromCurrecnyCode {  get; set; }
    public string fromCurrencyName {  get; set; }
    public string toCurrecnyCode { get; set; }
    public string toCurrencyName { get; set; }
    public double exchangeRate { get; set; }
    public DateTime lastRefreshed { get; set; }
    public string timeZone { get; set; }
    public double bidPrice { get; set; }
    public double askPrice { get; set; }
}
