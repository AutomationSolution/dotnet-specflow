using AutomationWeb.Configuration;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Microsoft.Extensions.Configuration.Json;
using TechTalk.SpecFlow;

namespace AutomationWeb.StepDefinitions;

[Binding]
public class ConfigurationFeatureStepDefinitions
{
    [Then(@"I assert that '(.*)' file contains in ConfigurationManager sources")]
    public void ThenIAssertThatFileContainsInConfigurationManagerSources(string fileName)
    {
        var sourcesList = AutomationConfiguration.ConfigurationManagerInstance.Sources.ToList();
        sourcesList.Single(x => x.GetType() == typeof(JsonConfigurationSource) && (x as JsonConfigurationSource).Path.Contains(fileName));
    }

    [Then(@"I assert that environment variables contains in ConfigurationManager sources")]
    public void ThenIAssertThatEnvironmentVariablesContainsInConfigurationManagerSources()
    {
        var sourcesList = AutomationConfiguration.ConfigurationManagerInstance.Sources.ToList();
        sourcesList.Single(x => x.GetType() == typeof(EnvironmentVariablesConfigurationSource));
    }
}