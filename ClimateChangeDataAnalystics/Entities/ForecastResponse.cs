using Newtonsoft.Json;

public class ForecastResponse
{
    [JsonProperty("forecasts")]
    public ForecastData[] Forecasts { get; set; }
}
