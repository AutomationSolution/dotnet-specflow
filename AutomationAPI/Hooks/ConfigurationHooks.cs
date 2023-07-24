using AutomationAPI.Configuration;
using AutomationFramework.Configuration;
using BoDi;
using TechTalk.SpecFlow;

namespace AutomationAPI.Hooks;

[Binding]
public class ConfigurationHooks
{
    [BeforeTestRun(Order = 10)]
    public static void BeforeTestRunInitConfiguration(IObjectContainer objectContainer)
    {
        objectContainer.RegisterTypeAs<AutomationApiConfiguration, IAutomationConfiguration>();
    }
}
