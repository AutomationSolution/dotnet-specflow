using AutomationFramework.Configuration;
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
        var sourcesList = AutomationConfigurationManager.Instance.Sources.ToList();
        sourcesList.Any(x => x.GetType() == typeof(JsonConfigurationSource) && (x as JsonConfigurationSource).Path.Contains(fileName));
    }

    [Then(@"I assert that environment variables contains in ConfigurationManager sources")]
    public void ThenIAssertThatEnvironmentVariablesContainsInConfigurationManagerSources()
    {
        var sourcesList = AutomationConfigurationManager.Instance.Sources.ToList();
        sourcesList.Any(x => x.GetType() == typeof(EnvironmentVariablesConfigurationSource));
    }

    [Given(@"The GUID is set in Configuration for specific scenario")]
    public void GivenTheGuidIsSetInConfigurationForSpecificScenario()
    {
        var guid = Guid.NewGuid();
        AutomationWebConfiguration.ScenarioMetaData.ScenarioGuid = guid;
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
        AutomationWebConfiguration.ScenarioMetaData.ScenarioGuid.Should().Be(expectedGuid);
    }

    [Then(@"I assert that RuntimeProperty from \.csproj file is accessible in runtime")]
    public void ThenIAssertThatRuntimePropertyFromCsprojFileIsAccessibleInRuntime()
    {
        AutomationWebConfiguration.ProjectProperties.RuntimePropertyExample.Should().NotBe(null);
        AutomationWebConfiguration.ProjectProperties.RuntimePropertyExample.Should().NotBe(string.Empty);
        AutomationWebConfiguration.ProjectProperties.RuntimePropertyExample.Should().NotBe("\"\"");
    }

    [Then(@"I assert that '(.*)' secret is accessible in runtime and its value is '(.*)'")]
    public void ThenIAssertThatSecretIsAccessibleInRuntimeAndItsValueIs(string secretName, string secretValue)
    {
        string result;

        try
        {
            result = AutomationWebConfiguration.SecretsModel.GetType().GetProperty(secretName).GetValue(AutomationWebConfiguration.SecretsModel, null) as string;
            
        }
        catch (NullReferenceException e)
        {
            throw new ArgumentOutOfRangeException(secretName, $"Can't find a specified property in {nameof(AutomationWebConfiguration.SecretsModel)} object");
        }

        result.Should().Be(secretValue);
    }
}