using AutomationWeb.Configuration;
using TechTalk.SpecFlow;

namespace AutomationWeb.Hooks;

[Binding]
public class DataHooks
{
    [BeforeScenario(Order = 50)]
    public static void BeforeScenarioSetScenarioMetaData()
    {
        AutomationWebConfiguration.ScenarioMetaData.ScenarioGuid = Guid.NewGuid();
        AutomationWebConfiguration.ScenarioMetaData.StartTime = DateTime.Now;
    }
}