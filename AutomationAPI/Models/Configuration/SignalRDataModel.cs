namespace AutomationAPI.Models.Configuration;

public class SignalRDataModel
{
    public const string JsonSectionName = "SignalR";

    public Uri SignalREndpoint { get; set; }
    public string SignalRHubPath { get; set; }
}
