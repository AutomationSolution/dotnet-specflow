using AutomationFramework.Enums.SpecFlow;
using TechTalk.SpecFlow;

namespace AutomationFramework.Utilities.SpecFlow;

public static class TagsUtilities
{
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

    public static ScenarioType GetScenarioType(ScenarioContext scenarioContext)
    {
        foreach (ScenarioType scenarioType in Enum.GetValues(typeof(ScenarioType)))
        {
            try
            {
                var _ = scenarioContext.ScenarioInfo.Tags.Single(x => x.Equals(scenarioType.ToString()));
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
