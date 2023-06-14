using AutomationFramework.Configuration;
using AutomationWeb.Configuration;

namespace AutomationWeb.Models.Configuration.BrowserStack;

public class BrowserStackWebData
{
    public string BrowserStackBuild =>
        string.Join(' ', AutomationWebConfiguration.BrowserStackModel.Settings.BrowserStackBuildPrefix,
            AutomationFrameworkConfiguration.TestRunConfig.TestRunStartTime.ToString(AutomationFrameworkConfiguration.TestRunConfig
                .DefaultCulture));
}
