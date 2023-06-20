namespace AutomationMobile.Models.Configuration;

public class SecretsMobileModel
{
    public const string JsonSectionName = "SecretsMobileModel";

    public string BrowserStackUser { get; set; }
    public string BrowserStackKey { get; set; }
}
