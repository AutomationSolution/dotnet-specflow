using TechTalk.SpecFlow;

namespace AutomationMobile.Utilities.SpecFlow;

public static class ScenarioContextUtilities
{
    public static string GetScenarioTitle(ScenarioContext scenarioContext)
    {
        return scenarioContext.ScenarioInfo.Title;
    }
}