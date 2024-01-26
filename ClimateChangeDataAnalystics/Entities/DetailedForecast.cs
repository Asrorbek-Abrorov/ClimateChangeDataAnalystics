using Newtonsoft.Json;

public class DetailedForecast
{
    [JsonProperty("reports")]
    public ForecastReport[] Reports { get; set; }
}
