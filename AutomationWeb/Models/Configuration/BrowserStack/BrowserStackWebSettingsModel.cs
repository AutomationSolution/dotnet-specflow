namespace AutomationWeb.Models.Configuration.BrowserStack;

public class BrowserStackWebSettingsModel
{
    public const string JsonSectionName = "BrowserStackSettings";

    public string BrowserStackOS { get; set; }
    public string BrowserStackOSVersion { get; set; }
    public Uri BrowserStackHubUrl { get; set; }
    public bool BrowserStackLocalTesting { get; set; }
    public bool BrowserStackLocalTestingAcceptSslCerts { get; set; }
    public string BrowserStackProject { get; set; }
    public string BrowserStackBuildPrefix { get; set; }
    public bool BrowserStackGetAppFromConfig { get; set; }
    public string BrowserStackCloudUrl { get; set; }
    public string BrowserStackCloudUploadPath { get; set; }
    public string BrowserStackCloudUploadedAppsPath { get; set; }
    public string BrowserStackDefaultLocation { get; set; }
    public bool BrowserStackEnableNetworkLogs { get; set; }
    public string BrowserStackIdleTimeoutSeconds { get; set; }
    public bool BrowserStackVideoRecording { get; set; }
    public bool BrowserStackInteractiveDebugging { get; set; }
}
