using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTrading.Models.Requests;

public class DigitalCurrencyDaily
{
    public string function {  get; set; }
    public string symbol { get; set; }
    public string market { get; set; }
    public string apikey { get; set; }
}
