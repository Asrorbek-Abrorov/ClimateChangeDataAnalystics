using Newtonsoft.Json;
using Spectre.Console;

public class NowUi
{
    public async Task Run(DateTime date)
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new FigletText(" * Weather info * ")
            .LeftJustified()
            .Color(Color.Green));

        string locationId = "1512569";
        string apiUrl = $"https://weather-broker-cdn.api.bbci.co.uk/en/forecast/aggregated/{locationId}";

        await AnsiConsole.Status()
                .Start("Thinking...", async ctx =>
                {
                    // Simulate some work
                    AnsiConsole.MarkupLine("Doing some work...");
                    Thread.Sleep(1000);

                    // Update the status and spinner
                    ctx.Status("Thinking some more");
                    ctx.Spinner(Spinner.Known.Star);
                    ctx.SpinnerStyle(Style.Parse("green"));

                    // Simulate some work
                    AnsiConsole.MarkupLine("Doing some more work...");
                    Thread.Sleep(2000);
                });

        using var client = new HttpClient();
        var response = await client.GetAsync(apiUrl);
        response.EnsureSuccessStatusCode();

        var forecastJson = await response.Content.ReadAsStringAsync();
        var forecastResponse = JsonConvert.DeserializeObject<ForecastResponse>(forecastJson);


        Console.WriteLine("Tashkent");
        await Console.Out.WriteLineAsync();

        int timeNow = date.Hour;

        foreach (var item in forecastResponse.Forecasts)
        {
            foreach (var forecast in item.Detailed.Reports)
            {
                DateTime forecastDate = DateTime.Parse(forecast.LocalDate);
                if(timeNow < 10)
                {
                    if (forecastDate.Date == date.Date && forecast.Timeslot == $"0{timeNow}:00")
                    {
                        Console.WriteLine("Description: " + forecast.EnhancedWeatherDescription);
                        Console.WriteLine("Time: " + forecast.LocalDate + " " + forecast.Timeslot);
                        Console.WriteLine("Weather: " + forecast.TemperatureC + "°C");
                        Console.WriteLine("Feels like: " + forecast.FeelsLikeTemperatureC + "°C");
                        Console.WriteLine($"Wind speed: {forecast.WindSpeedKph} KPH");
                        Console.WriteLine();
                    }
                }
                else
                {
                    if (forecastDate.Date == date.Date && forecast.Timeslot == $"{timeNow}:00")
                    {
                        Console.WriteLine("Description: " + forecast.EnhancedWeatherDescription);
                        Console.WriteLine("Time: " + forecast.LocalDate + " " + forecast.Timeslot);
                        Console.WriteLine("Weather: " + forecast.TemperatureC + "°C");
                        Console.WriteLine("Feels like: " + forecast.FeelsLikeTemperatureC + "°C");
                        Console.WriteLine($"Wind speed: {forecast.WindSpeedKph} KPH");
                        Console.WriteLine();
                    }
                }
            }
            Console.WriteLine();
        }
        AnsiConsole.Markup("[red]If nothing was written, there was not any info in the API[/]");
        await Console.Out.WriteLineAsync();
    }
}
