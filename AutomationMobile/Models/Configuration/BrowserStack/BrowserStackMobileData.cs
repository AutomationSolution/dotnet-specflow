using AutomationFramework.Configuration;
using AutomationMobile.Configuration;

namespace AutomationMobile.Models.Configuration.BrowserStack;

public class BrowserStackMobileData
{
    public string BrowserStackAppAddress => AutomationMobileConfiguration.BrowserStackModel.Settings.BrowserStackAppAddresses
        .GetType()
        .GetProperty(AutomationMobileConfiguration.DeviceConfigModel.ApplicationName.ToString())
        .GetValue(AutomationMobileConfiguration.BrowserStackModel.Settings.BrowserStackAppAddresses, null) as string;

    public string BrowserStackShareableId =>
        $"{AutomationMobileConfiguration.SecretsMobileModel.BrowserStackUser}/{AutomationMobileConfiguration.DeviceConfigModel.ApplicationName}";

    public string BrowserStackBuild =>
        string.Join(' ', AutomationMobileConfiguration.BrowserStackModel.Settings.BrowserStackBuildPrefix,
            AutomationFrameworkConfiguration.TestRunConfig.TestRunStartTime.ToString(AutomationFrameworkConfiguration.TestRunConfig
                .DefaultCulture));
}
