using Newtonsoft.Json;

namespace AutomationAPI.Models;

public class WeatherForecastHttp
{
    [JsonProperty("date", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
    public DateTimeOffset Date { get; set; }

    [JsonProperty("temperatureC", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
    public int TemperatureC { get; set; }

    [JsonProperty("summary", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
    public string Summary { get; set; }

    [JsonProperty("temperatureF", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
    public int TemperatureF { get; set; }
}
