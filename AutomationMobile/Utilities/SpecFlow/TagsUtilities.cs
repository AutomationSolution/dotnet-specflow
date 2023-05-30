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
}