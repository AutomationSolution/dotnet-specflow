using TechTalk.SpecFlow;

namespace AutomationFramework.Utilities.SpecFlow;

public static class ScenarioContextUtilities
{
    public static string GetScenarioTitle(ScenarioContext scenarioContext)
    {
        return scenarioContext.ScenarioInfo.Title;
    }
}
