using Aquality.Appium.Mobile.Applications;
using Aquality.Appium.Mobile.Configurations;
using Aquality.Selenium.Core.Configurations;
using AutomationMobile.Configuration;

namespace AutomationMobile.Utilities.Aquality;

public class BrowserStackApplicationProfile : IApplicationProfile
{
    private readonly ISettingsFile settingsFile;

    public BrowserStackApplicationProfile(ISettingsFile settingsFile)
    {
        this.settingsFile = settingsFile;
    }

    public IDriverSettings DriverSettings => new CustomDriverSettings(settingsFile);

    public bool IsRemote => settingsFile.GetValue<bool>(".isRemote");

    public PlatformName PlatformName => AutomationMobileConfiguration.DeviceConfigModel.PlatformName;

    public Uri RemoteConnectionUrl => AutomationMobileConfiguration.BrowserStackModel.Settings.BrowserStackHubUrl;

    public string ScreensLocation => settingsFile.GetValue<string>(".screensLocation");
}
