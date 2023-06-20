using AutomationAPI.Service;
using AutomationAPI.Utilities.WCF;
using FluentAssertions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace AutomationAPI.StepDefinitions;

[Binding]
public class WcfStepDefinitions
{
    private readonly ScenarioContext scenarioContext;
    private readonly string wcfResponseAlias = "WCFResponse";

    public WcfStepDefinitions(ScenarioContext scenarioContext)
    {
        this.scenarioContext = scenarioContext;
    }
    
    [Given(@"WCF connection is established")]
    public void GivenWcfConnectionIsEstablished()
    {
        scenarioContext.Set(new WcfClient());
    }

    [When(@"I send '(\d+)' number to WCF service")]
    public void WhenISendNumberToWcfService(int input)
    {
        var wcfClient = scenarioContext.Get<WcfClient>();
        var response = wcfClient.ClientInstance.GetDataAsync(input).Result;
        scenarioContext.Set(response, wcfResponseAlias);
    }

    [Then(@"I assert that WCF service response contains '(\d+)'")]
    public void ThenIAssertThatWcfServiceResponseIs(int input)
    {
        var response = scenarioContext.Get<string>(wcfResponseAlias);
        response.Should().Contain(input.ToString(), "WCF service response should contain expected input");
    }

    [When(@"I send the following object to WCF service contract")]
    public void WhenISendTheFollowingObjectToWcfServiceContract(Table table)
    {
        var wcfClient = scenarioContext.Get<WcfClient>();
        var compositeObject = table.CreateInstance<CompositeType>();
        var response = wcfClient.ClientInstance.GetDataUsingDataContractAsync(compositeObject).Result;
        scenarioContext.Set(response, wcfResponseAlias);
    }

    [Then(@"I assert that WCF service contract response contains the following object")]
    public void ThenIAssertThatWcfServiceContractResponseContainsTheFollowingObject(Table table)
    {
        var expectedObject = table.CreateInstance<CompositeType>();

        var response = scenarioContext.Get<CompositeType>(wcfResponseAlias);
        response.Should().BeEquivalentTo(expectedObject, "Object from WCF response should be equal to expected object");
    }
}
