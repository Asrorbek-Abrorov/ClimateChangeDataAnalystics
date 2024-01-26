using System;
using System.Net.Http;
using System.Threading.Tasks;
using ClimateChangeDataAnalystics.UIs;
using Newtonsoft.Json;

class Program
{
    static async Task Main()
    {
        var mainUi = new MainUi();
        await mainUi.Run();
    }
}