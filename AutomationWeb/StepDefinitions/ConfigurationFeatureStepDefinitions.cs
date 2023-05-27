using AutomationWeb.Configuration;
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
}