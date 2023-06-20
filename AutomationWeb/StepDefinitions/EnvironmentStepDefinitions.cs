using AutomationFramework.Configuration;
using AutomationWeb.Configuration;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using NLog;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using TechTalk.SpecFlow.Assist.Attributes;
using static AutomationFramework.Configuration.ConfigurationPaths;

namespace AutomationWeb.StepDefinitions;

[Binding]
public class EnvironmentStepDefinitions
{
    private readonly ScenarioContext scenarioContext;

    public EnvironmentStepDefinitions(ScenarioContext scenarioContext)
    {
        this.scenarioContext = scenarioContext;
    }

    [Given(@"The following environment variables are set")]
    public void GivenTheFollowingEnvironmentVariablesAreSet(Table table)
    {
        var environmentVariables = table.CreateSet<SimpleEnvironmentVariable>();
        foreach (var variable in environmentVariables)
        {
            Environment.SetEnvironmentVariable($"{variable.Section}:{variable.Name}", variable.Value);
        }
    }

    private class SimpleEnvironmentVariable
    {
        [TableAliases("VariableSection")]
        public string Section { get; set; }

        [TableAliases("VariableName")]
        public string Name { get; set; }

        [TableAliases("VariableValue")]
        public string Value { get; set; }
    }

    private class MappedSectionObject
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }

    [Given(@"Environment variables are added as a source to ConfigurationManager")]
    public void GivenEnvironmentVariablesAreAddedAsASourceToConfigurationManager()
    {
        AutomationConfigurationManager.Instance.AddEnvironmentVariables();
    }

    [Given(@"'(.*)' section is mapped to a MappedSectionObject object")]
    public void GivenSectionIsMappedToAMappedSectionObjectObject(string sectionName)
    {
        var envObjectFromConfiguration = AutomationConfigurationManager.Instance.GetRequiredSection(sectionName).Get<MappedSectionObject>();
        scenarioContext.Set(envObjectFromConfiguration);
    }

    [Given("Mapped object contain the following environment variables")]
    [Then(@"I assert that mapped object contain the following environment variables")]
    public void ThenIAssertThatMappedObjectContainTheFollowingEnvironmentVariables(Table table)
    {
        var actualResult = scenarioContext.Get<MappedSectionObject>();

        var expectedResult = table.CreateSet<SimpleEnvironmentVariable>();

        expectedResult.Any(x => x.Value == actualResult.Name).Should().BeTrue();
        expectedResult.Any(x => x.Value == actualResult.Password).Should().BeTrue();
    }

    [Given(@"'(.*)' file is added as a source to ConfigurationManager")]
    public void GivenFileIsAddedAsASourceToConfigurationManager(string fileName)
    {
        AutomationConfigurationManager.Instance.AddJsonFile(Path.Combine(ResourcesDirectoryName, ConfigurationDirectoryName, fileName));
    }

    [When(@"I output EnvironmentModel values")]
    public void WhenIOutputEnvironmentModelValues()
    {
        LogManager.GetCurrentClassLogger().Info(AutomationWebConfiguration.EnvironmentModel.Endpoint);
        LogManager.GetCurrentClassLogger().Info(AutomationWebConfiguration.EnvironmentModel.EnvironmentType);
    }
}