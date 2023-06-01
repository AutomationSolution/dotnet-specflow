using AutomationMobile.Enums.Aquality;
using AutomationMobile.Enums.FrameworkAdditions;
using TechTalk.SpecFlow;

namespace AutomationMobile.Utilities.SpecFlow;

public static class TagsUtilities
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

    public static string GetTestRailId(ScenarioContext scenarioContext)
    {
        return scenarioContext.ScenarioInfo.Tags.Single(tag => tag.Contains("TestRailId_"))
            .Replace("TestRailId_C", string.Empty);
    }

    public static string GetTestFeatureName(ScenarioContext scenarioContext)
    {
        string teamName;
        try
        {
            teamName = scenarioContext.ScenarioInfo.Tags.Single(tag => tag.Contains("Feature"));
        }
        catch (InvalidOperationException)
        {
            return string.Empty;
        }

        return teamName;
    }

    public static bool GetInstallFromAppCenterFlag(ScenarioContext scenarioContext)
    {
        return scenarioContext.ScenarioInfo.Tags.Contains("InstallFromAppCenter");
    }

    public static bool GetBiometricAuthFlag(ScenarioContext scenarioContext)
    {
        return scenarioContext.ScenarioInfo.Tags.Contains("BiometricAuthEnabled");
    }

    public static ScenarioType GetScenarioType(ScenarioContext scenarioContext)
    {
        foreach (ScenarioType scenarioType in Enum.GetValues(typeof(ScenarioType)))
        {
            try
            {
                var x = scenarioContext.ScenarioInfo.Tags.Single(x => x.Equals(scenarioType.ToString()));
                return scenarioType;
            }
            catch (InvalidOperationException)
            {
                // Continue to next scenario type
            }
        }

        throw new InvalidOperationException(
            $"Unable to parse scenario type. Make sure you've specified it in scenario tags. Reference: {nameof(ScenarioType)}");
    }
}
