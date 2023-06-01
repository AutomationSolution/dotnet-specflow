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
}