namespace AutomationWeb.Models.Configuration;

public class SecretsWebModel
{
    public const string JsonSectionName = "Secrets";

    public string BackOfficeUsername { get; set; }
    public string BackOfficePassword { get; set; }
    public string BrowserStackUser { get; set; }
    public string BrowserStackKey { get; set; }
}
