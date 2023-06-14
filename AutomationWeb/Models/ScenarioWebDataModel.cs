using AutomationFramework.Enums.SpecFlow;
using AutomationFramework.Utilities.SpecFlow;
using TechTalk.SpecFlow;

namespace AutomationWeb.Models;

public class ScenarioWebDataModel
{
    private ScenarioType ScenarioType { get; set; }
    private string ScenarioName { get; set; }
    // public string TestRailId { get; set; }   // TODO uncomment once TestRail integration is implemented
    public string TestFeatureName { get; set; }
    public string InformativeScenarioName => $"[{TestFeatureName}] {ScenarioName}";
    
    public ScenarioWebDataModel(ScenarioContext scenarioContext)
    {
        ScenarioType = TagsUtilities.GetScenarioType(scenarioContext);
        ScenarioName = ScenarioContextUtilities.GetScenarioTitle(scenarioContext);
        // TestRailId = TagsUtilities.GetTestRailId(scenarioContext);    // TODO uncomment once TestRail integration is implemented
        TestFeatureName = TagsUtilities.GetTestFeatureName(scenarioContext);
    }
}
