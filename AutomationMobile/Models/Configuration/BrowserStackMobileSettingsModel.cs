namespace AutomationMobile.Models.Configuration;

public class BrowserStackMobileSettingsModel
{
    public Uri BrowserStackHubUrl { get; set; }
    public bool BrowserStackRealMobile { get; set; }
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
    public BrowserStackAppAddress BrowserStackAppAddresses { get; set; }
}

public class BrowserStackAppAddress
{
    public string iOSBusiness { get; set; }
    public string iOSCustomer { get; set; }
    public string AndroidBusiness { get; set; }
    public string AndroidCustomer { get; set; }
}
