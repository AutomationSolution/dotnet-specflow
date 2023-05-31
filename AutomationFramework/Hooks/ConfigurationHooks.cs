using AutomationFramework.Configuration;
using BoDi;
using TechTalk.SpecFlow;

namespace AutomationFramework.Hooks;

[Binding]
public class ConfigurationHooks
{
    private static IAutomationConfiguration _defaultConfiguration;
    private static IAutomationConfiguration _customConfiguration;
    
    [BeforeTestRun(Order = 20)]
    public static void BeforeTestRunResolveDependencies(IObjectContainer objectContainer)
    {
        _defaultConfiguration = new AutomationFrameworkConfiguration();
        _customConfiguration = objectContainer.Resolve<IAutomationConfiguration>();
    }
    
    [BeforeTestRun(Order = 30)]
    public static void BeforeTestRunInitializeRequiredConfiguration()
    {
        _defaultConfiguration.AddStaticSources(AutomationConfigurationManager.Instance);
        _defaultConfiguration.InitStaticConfiguration(AutomationConfigurationManager.Instance);
        
        _customConfiguration.AddStaticSources(AutomationConfigurationManager.Instance);
        _customConfiguration.InitStaticConfiguration(AutomationConfigurationManager.Instance);
    }

    [BeforeScenario(Order = 30)]
    public static void BeforeScenarioInitializeCustomConfiguration(ScenarioContext scenarioContext)
    {
        _defaultConfiguration.AddThreadStaticSources(AutomationConfigurationManager.Instance);
        _defaultConfiguration.InitThreadStaticConfiguration(AutomationConfigurationManager.Instance, scenarioContext);
        
        _customConfiguration.AddThreadStaticSources(AutomationConfigurationManager.Instance);
        _customConfiguration.InitThreadStaticConfiguration(AutomationConfigurationManager.Instance, scenarioContext);
    }
}