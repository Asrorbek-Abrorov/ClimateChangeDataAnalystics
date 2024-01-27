using ClimateChangeDataAnalystics;
using Newtonsoft.Json;
using Spectre.Console;

public class NowUi
{
    public async Task Run(DateTime date)
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new FigletText(" * Weather info * ")
            .LeftJustified()
            .Color(Color.Yellow));

        string locationId = "1512569";
        string apiUrl = $"https://weather-broker-cdn.api.bbci.co.uk/en/forecast/aggregated/{locationId}";

        string lat = "41.2646";
        string lon = "69.2163";
        string apiKey = "b1ba0c3c55c4aa75b181d87851ea8321";

        string airPollutionUrl = $"http://api.openweathermap.org/data/2.5/air_pollution?lat={lat}&lon={lon}&appid={apiKey}";

        using var aqiClient = new HttpClient();
        var aqiResponse = await aqiClient.GetAsync(airPollutionUrl);
        aqiResponse.EnsureSuccessStatusCode();
        var aqiContent = await aqiResponse.Content.ReadAsStringAsync();

        WeatherData weatherData = JsonConvert.DeserializeObject<WeatherData>(aqiContent);

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
        Console.WriteLine();
        Console.WriteLine();

        int timeNow = date.Hour + 1;

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
                        AnsiConsole.WriteLine($"Humidity: {forecast.Humidity}%");
                        AnsiConsole.WriteLine($"Pressure: {forecast.Pressure}mp");
                        Console.WriteLine($"Precipation probability: {forecast.PrecipitationProbabilityInPercent}%");
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
                        AnsiConsole.WriteLine($"Humidity: {forecast.Humidity}%");
                        AnsiConsole.WriteLine($"Pressure: {forecast.Pressure}mp");
                        Console.WriteLine($"Precipation probability: {forecast.PrecipitationProbabilityInPercent}% {forecast.PrecipitationProbabilityText}");
                        Console.WriteLine($"Wind speed: {forecast.WindSpeedKph} KPH");
                        Console.WriteLine($"Wind direction: {forecast.WindDirectionFull} ");
                        Console.WriteLine($"Gust speed: {forecast.GustSpeedKph} KPH");
                        Console.WriteLine($"Visibility: {forecast.Visibility}");
                        Console.WriteLine();
                    }
                }
            }
        }
        int aqi = weatherData.list[0].main.aqi;
        double co = weatherData.list[0].components.co;
        double no = weatherData.list[0].components.no;
        double no2 = weatherData.list[0].components.no2;
        double o3 = weatherData.list[0].components.o3;
        double so2 = weatherData.list[0].components.so2;
        double pm2_5 = weatherData.list[0].components.pm2_5;
        double pm10 = weatherData.list[0].components.pm10;
        double nh3 = weatherData.list[0].components.nh3;



        Console.WriteLine($"Coordinates: {lon}, {lat}");
        switch (aqi)
        {
            case 1:
                Console.WriteLine($"AQI: {aqi} {AQI.Good}");
                break;
            case 2:
                Console.WriteLine($"AQI: {aqi} {AQI.fair}");
                break;
            case 3:
                Console.WriteLine($"AQI: {aqi} {AQI.Moderate}");
                break;
            case 4:
                Console.WriteLine($"AQI: {aqi} {AQI.Poor}");
                break;
            case 5:
                Console.WriteLine($"AQI: {aqi} {AQI.VeryPoor}");
                break;
            default:
                AnsiConsole.MarkupLine($"AQI: [red1]{aqi}[/] [red rapidblink italic]Not recommended to be outside[/]");
                break;
        }
        Console.WriteLine($"    CO: {co}");
        Console.WriteLine($"    NO: {no}");
        Console.WriteLine($"    NO2: {no2}");
        Console.WriteLine($"    O3: {o3}");
        Console.WriteLine($"    SO2: {so2}");
        Console.WriteLine($"    PM2.5: {pm2_5}");
        Console.WriteLine($"    PM10: {pm10}");
        Console.WriteLine($"    NH3: {nh3}");

        await Console.Out.WriteLineAsync();
    }
}
