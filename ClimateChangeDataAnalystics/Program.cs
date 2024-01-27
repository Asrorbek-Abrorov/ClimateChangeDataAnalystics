using System;
using System.Net.Http;
using System.Threading.Tasks;
using ClimateChangeDataAnalystics.UIs;
using Colorful;
using Newtonsoft.Json;
using Spectre.Console;
using Console = Colorful.Console;

class Program
{
    static async Task Main()
    {
        var mainUi = new MainUi();

        await mainUi.Run();
    }
}