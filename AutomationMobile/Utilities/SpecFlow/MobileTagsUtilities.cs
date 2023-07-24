using AutomationMobile.Enums.Aquality;
using AutomationMobile.Enums.FrameworkAdditions;
using TechTalk.SpecFlow;

namespace AutomationMobile.Utilities.SpecFlow;

public static class MobileTagsUtilities
{
    public static ApplicationName GetApplicationName(ScenarioContext scenarioContext)
    {
        var applicationName = ApplicationName.iOSBusiness;  // Default value will not be used due to LINQ code functionality
        var applicationNameTag = scenarioContext.ScenarioInfo.Tags.Single(x => Enum.TryParse(x, out applicationName));
        return applicationName;
    }

    public static DeviceType GetDeviceType(ScenarioContext scenarioContext)
    {
        return scenarioContext.ScenarioInfo.Tags.Contains("tablet") ? DeviceType.Tablet : DeviceType.Phone;
    }
    
    public static bool GetInstallFromAppCenterFlag(ScenarioContext scenarioContext)
    {
        return scenarioContext.ScenarioInfo.Tags.Contains("InstallFromAppCenter");
    }

    public static bool GetBiometricAuthFlag(ScenarioContext scenarioContext)
    {
        return scenarioContext.ScenarioInfo.Tags.Contains("BiometricAuthEnabled");
    }
}
