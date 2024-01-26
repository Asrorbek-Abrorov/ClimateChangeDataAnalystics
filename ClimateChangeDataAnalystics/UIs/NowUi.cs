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

        using var client = new HttpClient();
        var response = await client.GetAsync(apiUrl);
        response.EnsureSuccessStatusCode();

        var forecastJson = await response.Content.ReadAsStringAsync();
        var forecastResponse = JsonConvert.DeserializeObject<ForecastResponse>(forecastJson);

        Console.WriteLine("Tashkent");
        await Console.Out.WriteLineAsync();

        string inputTimeslot = date.TimeOfDay.ToString();

        foreach (var item in forecastResponse.Forecasts)
        {
            foreach (var forecast in item.Detailed.Reports)
            {
                DateTime forecastDate = DateTime.Parse(forecast.LocalDate);
                if (forecastDate.Date == date.Date && forecast.Timeslot == inputTimeslot)
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
    }
}
