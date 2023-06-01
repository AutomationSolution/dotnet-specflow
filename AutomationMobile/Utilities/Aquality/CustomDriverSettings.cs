using Aquality.Appium.Mobile.Configurations;
using Aquality.Selenium.Core.Configurations;
using AutomationMobile.Configuration;
using OpenQA.Selenium.Appium;

namespace AutomationMobile.Utilities.Aquality;

public class CustomDriverSettings : CapabilityBasedSettings, IDriverSettings
{
    private const string ApplicationPathKey = "applicationPath";
    private const string AppCapabilityKey = "app";

    private readonly ISettingsFile settingsFile;

    private AppiumOptions appiumOptions;

    public CustomDriverSettings(ISettingsFile settingsFile)
    {
        this.settingsFile = settingsFile;
    }

    private string ApplicationPathJPath => $".driverSettings.{AutomationMobileConfiguration.DeviceConfigModel.PlatformName.ToString().ToLower()}.{ApplicationPathKey}";

    private bool HasApplicationPath => settingsFile.IsValuePresent(ApplicationPathJPath);

    private IReadOnlyDictionary<string, object> DeviceCapabilities
    {
        get
        {
            var deviceKey = AutomationMobileConfiguration.DeviceConfigModel.DeviceName.ToString();
            var deviceSettings = new DeviceSettings(deviceKey);
            return deviceSettings.Capabilities;
        }
    }

    public AppiumOptions AppiumOptions
    {
        get
        {
            if (appiumOptions is null)
            {
                appiumOptions = AutomationMobileConfiguration.AppiumOptions;
                if (HasApplicationPath && ApplicationPath != null)
                    SetCapability(appiumOptions, new KeyValuePair<string, object>(AppCapabilityKey, ApplicationPath));
                DeviceCapabilities.ToList().ForEach(capability => SetCapability(appiumOptions, capability));
            }

            return appiumOptions;
        }
    }

    public string ApplicationPath
    {
        get
        {
            var appValue = settingsFile.GetValue<string>(ApplicationPathJPath);
            return appValue?.StartsWith(".") == true ? Path.GetFullPath(appValue) : appValue;
        }
    }
}