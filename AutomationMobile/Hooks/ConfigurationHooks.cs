using AutomationFramework.Configuration;
using AutomationMobile.Configuration;
using BoDi;
using TechTalk.SpecFlow;

namespace AutomationMobile.Hooks;

[Binding]
public class ConfigurationHooks
{
    [BeforeTestRun(Order = 10)]
    public static void BeforeTestRunInitConfiguration(IObjectContainer objectContainer)
    {
        objectContainer.RegisterTypeAs<AutomationMobileConfiguration, IAutomationConfiguration>();
    }
}