using AutomationMobile.Enums.Aquality;
using AutomationMobile.Enums.FrameworkAdditions;
using TechTalk.SpecFlow;

namespace AutomationMobile.Utilities.SpecFlow;

public static class MobileTagsUtilities
{
    public static ApplicationName GetApplicationName(ScenarioContext scenarioContext)
    {
        ApplicationName applicationName;
        var applicationNameTag = scenarioContext.ScenarioInfo.Tags.Single(x => Enum.TryParse(x, out applicationName));
        Enum.TryParse(applicationNameTag, out applicationName); // Duplicate code to remove compiler error
        return applicationName;
    }

    public static DeviceType GetDeviceType(ScenarioContext scenarioContext)
    {
        if (scenarioContext.ScenarioInfo.Tags.Contains("tablet")) return DeviceType.Tablet;

        return DeviceType.Phone;
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
