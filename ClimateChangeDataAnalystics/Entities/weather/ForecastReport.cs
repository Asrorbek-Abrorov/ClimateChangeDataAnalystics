using Newtonsoft.Json;

public class ForecastReport
{
    [JsonProperty("enhancedWeatherDescription")]
    public string EnhancedWeatherDescription { get; set; }

    [JsonProperty("extendedWeatherType")]
    public int ExtendedWeatherType { get; set; }

    [JsonProperty("feelsLikeTemperatureC")]
    public int FeelsLikeTemperatureC { get; set; }

    [JsonProperty("feelsLikeTemperatureF")]
    public int FeelsLikeTemperatureF { get; set; }

    [JsonProperty("gustSpeedKph")]
    public int GustSpeedKph { get; set; }

    [JsonProperty("gustSpeedMph")]
    public int GustSpeedMph { get; set; }

    [JsonProperty("humidity")]
    public int Humidity { get; set; }

    [JsonProperty("localDate")]
    public string LocalDate { get; set; }

    [JsonProperty("precipitationProbabilityInPercent")]
    public int PrecipitationProbabilityInPercent { get; set; }

    [JsonProperty("precipitationProbabilityText")]
    public string PrecipitationProbabilityText { get; set; }

    [JsonProperty("pressure")]
    public int Pressure { get; set; }

    [JsonProperty("temperatureC")]
    public int TemperatureC { get; set; }

    [JsonProperty("temperatureF")]
    public int TemperatureF { get; set; }

    [JsonProperty("timeslot")]
    public string Timeslot { get; set; }

    [JsonProperty("timeslotLength")]
    public int TimeslotLength { get; set; }

    [JsonProperty("visibility")]
    public string Visibility { get; set; }

    [JsonProperty("weatherType")]
    public int WeatherType { get; set; }

    [JsonProperty("weatherTypeText")]
    public string WeatherTypeText { get; set; }

    [JsonProperty("windDescription")]
    public string WindDescription { get; set; }

    [JsonProperty("windDirection")]
    public string WindDirection { get; set; }

    [JsonProperty("windDirectionAbbreviation")]
    public string WindDirectionAbbreviation { get; set; }

    [JsonProperty("windDirectionFull")]
    public string WindDirectionFull { get; set; }

    [JsonProperty("windSpeedKph")]
    public int WindSpeedKph { get; set; }

    [JsonProperty("windSpeedMph")]
    public int WindSpeedMph { get; set; }
}