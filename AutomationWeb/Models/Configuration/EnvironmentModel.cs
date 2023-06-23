namespace AutomationWeb.Models.Configuration;

public sealed class EnvironmentModel
{
    public const string JsonSectionName = "Environment";

    public string Endpoint { get; set; }
    public string EnvironmentType { get; set; }

    public string GetEndpoint() => string.Format(Endpoint, EnvironmentType);
}
