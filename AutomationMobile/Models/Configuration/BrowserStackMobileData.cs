using AutomationFramework.Configuration;
using AutomationMobile.Configuration;

namespace AutomationMobile.Models.Configuration;

public class BrowserStackMobileData
{
    public string BrowserStackAppAddress => AutomationMobileConfiguration.BrowserStackMobileSettingsModel.BrowserStackAppAddresses
        .GetType()
        .GetProperty(AutomationMobileConfiguration.DeviceConfigModel.ApplicationName.ToString())
        .GetValue(AutomationMobileConfiguration.BrowserStackMobileSettingsModel.BrowserStackAppAddresses, null) as string;

    public string BrowserStackShareableId =>
        $"{AutomationMobileConfiguration.SecretsMobileModel.BrowserStackUser}/{AutomationMobileConfiguration.DeviceConfigModel.ApplicationName}";

    public string BrowserStackBuild =>
        string.Join(' ', AutomationMobileConfiguration.BrowserStackMobileSettingsModel.BrowserStackBuildPrefix,
            AutomationFrameworkConfiguration.TestRunConfig.TestRunStartTime.ToString(AutomationFrameworkConfiguration.TestRunConfig
                .DefaultCulture));
}