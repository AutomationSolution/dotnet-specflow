using Newtonsoft.Json;

namespace AutomationWeb.Models.Requests;

public class SampleResponseModel
{
    [JsonProperty("_id")]
    public string Id { get; set; }

    [JsonProperty("_name")]
    public string Name { get; set; }
}