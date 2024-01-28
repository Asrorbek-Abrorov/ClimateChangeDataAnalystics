using Newtonsoft.Json;
using Spectre.Console;

namespace ClimateChangeDataAnalystics.UIs;

public class WeekUi
{
    public async Task Run(DateTime date)
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new FigletText(" * Weather info * ")
            .LeftJustified()
            .Color(Color.Purple));

        string locationId = "1512569";
        string apiUrl = $"https://weather-broker-cdn.api.bbci.co.uk/en/forecast/aggregated/{locationId}";

        await AnsiConsole.Status()
            .Spinner(Spinner.Known.Star)
            .Start("[mediumpurple4]Checking...[/]", async ctx =>
            {
                // Simulate some work
                AnsiConsole.MarkupLine("[mediumspringgreen]Connecting to the server...[/]");
                Thread.Sleep(1250);

                // Update the status and spinner
                ctx.Status("[green1 bold italic]Getting information[/]");
                ctx.Spinner(Spinner.Known.Star);
                ctx.SpinnerStyle(Style.Parse("green"));

                // Simulate some work
                AnsiConsole.MarkupLine("[springgreen4]Waiting for the response...[/]");
                Thread.Sleep(2250);

            });

        using var client = new HttpClient();
        var response = await client.GetAsync(apiUrl);
        response.EnsureSuccessStatusCode();

        var forecastJson = await response.Content.ReadAsStringAsync();
        var forecastResponse = JsonConvert.DeserializeObject<ForecastResponse>(forecastJson);

        await Console.Out.WriteLineAsync();
        AnsiConsole.Markup("[italic bold]Tashkent[/]");
        await Console.Out.WriteLineAsync();
        await Console.Out.WriteLineAsync();

        int timeNow = date.Hour + 1;

        foreach (var item in forecastResponse.Forecasts)
        {
            foreach (var forecast in item.Detailed.Reports)
            {
                DateTime forecastDate = DateTime.Parse(forecast.LocalDate);
                if (forecast.Timeslot == $"00:00")
                {
                    Console.WriteLine("Description: " + forecast.EnhancedWeatherDescription);
                    Console.WriteLine("Time: " + forecast.LocalDate + " " + forecast.Timeslot);
                    Console.WriteLine("Weather: " + forecast.TemperatureC + "°C");
                    Console.WriteLine("Feels like: " + forecast.FeelsLikeTemperatureC + "°C");
                    Console.WriteLine($"Wind speed: {forecast.WindSpeedKph} KPH");
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
        }
        await Console.Out.WriteLineAsync();
    }
}
