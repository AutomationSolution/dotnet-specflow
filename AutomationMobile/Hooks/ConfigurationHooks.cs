using AutomationMobile.Configuration;
using AutomationMobile.Models;
using AutomationMobile.Utilities.SpecFlow;
using BoDi;
using TechTalk.SpecFlow;

namespace AutomationMobile.Hooks;

[Binding]
public class ConfigurationHooks
{
    [BeforeTestRun(Order = 10)]
    public static void BeforeTestRunInitConfiguration(IObjectContainer objectContainer)
    {
        // AutomationMobileConfiguration.InitTestRunConfiguration();
    }

    [BeforeScenario(Order = 10)]
    public static void BeforeScenarioInitConfiguration(ScenarioContext scenarioContext)
    {
        var applicationName = TagsUtilities.GetApplicationName(scenarioContext);

        AutomationMobileConfiguration.DeviceConfigModel = new DeviceConfigModel(applicationName);
    }
}