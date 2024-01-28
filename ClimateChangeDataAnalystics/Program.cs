using ClimateChangeDataAnalystics.UIs;

class Program
{
    static async Task Main()
    {
        var mainUi = new MainUi();

        await mainUi.Run();
    }
}