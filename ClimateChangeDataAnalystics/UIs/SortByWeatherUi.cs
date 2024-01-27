using Newtonsoft.Json;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimateChangeDataAnalystics.UIs;

public class SortByWeatherUi
{
    public async Task Run(DateTime date)
    {
        var keepRunning = true;
        while (keepRunning)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(new FigletText(" * Main Ui * ")
                .LeftJustified()
                .Color(Color.Cyan1));

            AnsiConsole.WriteLine();

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

            var weatherTypes = new List<string>();

            using var client = new HttpClient();
            var response = await client.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            var forecastJson = await response.Content.ReadAsStringAsync();
            var forecastResponse = JsonConvert.DeserializeObject<ForecastResponse>(forecastJson);

            foreach (var item in forecastResponse.Forecasts)
            {
                foreach (var forecast in item.Detailed.Reports)
                {
                    if (!weatherTypes.Contains(forecast.WeatherTypeText))
                    {
                        weatherTypes.Add(forecast.WeatherTypeText);
                    }
                }
            }
            weatherTypes.Add("Back");
;           var ui = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("[orange1 rapidblink italic bold link]* Weathers *[/]!")
                .PageSize(12)
                .MoreChoicesText("[grey rapidblink](Move up and down to reveal more)[/]")
                .AddChoices(weatherTypes)
                
                .HighlightStyle("red bold italic"));

            switch (ui)
            {
                case "Back":
                    return;
            }

            foreach (var item in forecastResponse.Forecasts)
            {
                foreach (var forecast in item.Detailed.Reports)
                {
                    if (ui == forecast.WeatherTypeText)
                    {
                        Console.WriteLine("Weather type: " + forecast.WeatherTypeText);
                        Console.WriteLine("Description: " + forecast.EnhancedWeatherDescription);
                        Console.WriteLine("Time: " + forecast.LocalDate + " " + forecast.Timeslot);
                        Console.WriteLine("Weather: " + forecast.TemperatureC + "°C");
                        Console.WriteLine("Feels like: " + forecast.FeelsLikeTemperatureC + "°C");
                        AnsiConsole.WriteLine($"Humidity: {forecast.Humidity}%");
                        AnsiConsole.WriteLine($"Pressure: {forecast.Pressure}mp");
                        Console.WriteLine($"Precipation probability: {forecast.PrecipitationProbabilityInPercent}%");
                        Console.WriteLine($"Wind speed: {forecast.WindSpeedKph} KPH");
                        Console.WriteLine();
                    }
                }
            }

            await Console.Out.WriteLineAsync();
            AnsiConsole.Markup("[slowblink green1]Enter[/] any [link yellow1]key[/] to [italic red]continue[/]!");
            Console.ReadKey();
        }
    }
}
