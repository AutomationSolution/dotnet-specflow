namespace AutomationMobile.Models.Configuration;

public class SecretsMobileModel
{
    public const string JsonSectionName = "Secrets";

    public string BrowserStackUser { get; set; }
    public string BrowserStackKey { get; set; }
}
