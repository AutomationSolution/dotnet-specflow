using Microsoft.Extensions.Configuration;
using TechTalk.SpecFlow;

namespace AutomationFramework.Configuration;

public interface IAutomationConfiguration
{
    public void AddStaticSources(ConfigurationManager configurationManagerInstance) {}
    public void AddThreadStaticSources(ConfigurationManager configurationManagerInstance) {}
    public void InitStaticConfiguration(ConfigurationManager configurationManagerInstance) {}
    public void InitThreadStaticConfiguration(ConfigurationManager configurationManagerInstance, ScenarioContext scenarioContext) {}
}