using AutomationWeb.Configuration;
using BoDi;
using TechTalk.SpecFlow;

namespace AutomationWeb.Hooks;

[Binding]
public class BaseHooks
{
    [BeforeTestRun(Order = 10)]
    public static void BeforeTestRunInitConfiguration(IObjectContainer objectContainer)
    {
        AutomationWebConfiguration.InitTestRunConfiguration();
    }

    [BeforeScenario(Order = 10)]
    public static void BeforeScenarioInitConfiguration()
    {
        AutomationWebConfiguration.InitTestThreadConfiguration();
    }
}