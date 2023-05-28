using AutomationWeb.Configuration;
using FluentAssertions;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Microsoft.Extensions.Configuration.Json;
using TechTalk.SpecFlow;

namespace AutomationWeb.StepDefinitions;

[Binding]
public class ConfigurationFeatureStepDefinitions
{
    private readonly ScenarioContext scenarioContext;
    
    public ConfigurationFeatureStepDefinitions(ScenarioContext scenarioContext)
    {
        this.scenarioContext = scenarioContext;
    }
    
    [Then(@"I assert that '(.*)' file contains in ConfigurationManager sources")]
    public void ThenIAssertThatFileContainsInConfigurationManagerSources(string fileName)
    {
        var sourcesList = AutomationConfiguration.ConfigurationManagerInstance.Sources.ToList();
        sourcesList.Any(x => x.GetType() == typeof(JsonConfigurationSource) && (x as JsonConfigurationSource).Path.Contains(fileName));
    }

    [Then(@"I assert that environment variables contains in ConfigurationManager sources")]
    public void ThenIAssertThatEnvironmentVariablesContainsInConfigurationManagerSources()
    {
        var sourcesList = AutomationConfiguration.ConfigurationManagerInstance.Sources.ToList();
        sourcesList.Any(x => x.GetType() == typeof(EnvironmentVariablesConfigurationSource));
    }

    [Given(@"The GUID is set in Configuration for specific scenario")]
    public void GivenTheGuidIsSetInConfigurationForSpecificScenario()
    {
        var guid = Guid.NewGuid();
        AutomationConfiguration.TestThreadScopedModel.guid = guid;
        scenarioContext.Set(guid);
    }

    [When(@"I wait for '(\d+)' seconds")]
    public void WhenIWaitForSeconds(int secondsToWait)
    {
        Thread.Sleep(TimeSpan.FromSeconds(secondsToWait));
    }

    [Then(@"I assert that GUID set in Configuration is the same")]
    public void ThenIAssertThatGuidSetInConfigurationIsTheSame()
    {
        var expectedGuid = scenarioContext.Get<Guid>();
        AutomationConfiguration.TestThreadScopedModel.guid.Should().Be(expectedGuid);
    }

    [Then(@"I assert that Environment property from \.csproj file is accessible in runtime")]
    public void ThenIAssertThatEnvironmentPropertyFromCsprojFileIsAccessibleInRuntime()
    {
        AutomationConfiguration.ProjectProperties.Environment.Should().NotBe(null);
        AutomationConfiguration.ProjectProperties.Environment.Should().NotBe(string.Empty);
        AutomationConfiguration.ProjectProperties.Environment.Should().NotBe("\"\"");
    }
}