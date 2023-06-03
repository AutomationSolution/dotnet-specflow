namespace AutomationWeb.Models.Configuration;

public sealed class EnvironmentModel
{
    public string Endpoint { get; set; }
    public string EnvironmentType { get; set; }

    public string GetEndpoint()
    {
        return EnvironmentType.Equals(string.Empty) ? string.Format(Endpoint, string.Empty) : string.Join(',', Endpoint, EnvironmentType);
    }
}
