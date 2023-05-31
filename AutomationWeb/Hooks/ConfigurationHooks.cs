using AutomationFramework.Configuration;
using AutomationWeb.Configuration;
using BoDi;
using TechTalk.SpecFlow;

namespace AutomationWeb.Hooks;

[Binding]
public class ConfigurationHooks
{
    [BeforeTestRun(Order = 10)]
    public static void BeforeTestRunInitConfiguration(IObjectContainer objectContainer)
    {
        objectContainer.RegisterTypeAs<AutomationWebConfiguration, IAutomationConfiguration>();
    }
}