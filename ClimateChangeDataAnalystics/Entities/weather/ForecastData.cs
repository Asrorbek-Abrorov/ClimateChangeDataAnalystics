using Newtonsoft.Json;

public class ForecastData
{
    [JsonProperty("detailed")]
    public DetailedForecast Detailed { get; set; }
}
