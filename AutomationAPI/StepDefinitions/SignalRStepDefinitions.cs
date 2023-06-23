using AutomationAPI.Utilities.SignalR;
using AutomationFramework.Utilities.Polly;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace AutomationAPI.StepDefinitions;

[Binding]
public class SignalRStepDefinitions
{
    private readonly ScenarioContext scenarioContext;

    public SignalRStepDefinitions(ScenarioContext scenarioContext)
    {
        this.scenarioContext = scenarioContext;
    }

    [Given(@"SignalR connection is opened")]
    public async Task GivenSignalRConnectionIsOpened()
    {
        var signalRConnection = new SignalRConnection();
        await signalRConnection.Connect();
        scenarioContext.Set(signalRConnection);
    }

    [When(@"I send a SignalR message with '(.*)' name and '(.*)' value")]
    public void WhenISendASignalRMessageWithNameAndValue(string messageName, string messageValue)
    {
        var signalRConnection = scenarioContext.Get<SignalRConnection>();

        ConditionalWait.WaitForTrue(() => signalRConnection.IsConnectionEstablished(), 
            failReason: "SignalR connection is not established",
            codePurpose: "Wait until SignalR connection is established");
        
        signalRConnection.SendMessage(messageName, messageValue).Wait();
    }

    [Then(@"I assert that SignalR message with '(.*)' name and '(.*)' value is received")]
    public void ThenIAssertThatSignalRMessageWithNameAndValueIsReceived(string messageName, string messageValue)
    {
        var signalRConnection = scenarioContext.Get<SignalRConnection>();

        ConditionalWait.WaitForTrue(() => signalRConnection.IsConnectionEstablished(), 
            failReason: "SignalR connection is not established",
            codePurpose: "Wait until SignalR connection is established");

        var result = ConditionalWait.WaitForPredicateAndGetResult(
            () => signalRConnection.GetReceivedMessages().Count, 
            i => i > 0, 
            failReason: "Received messages from SignalR should not be empty",
            codePurpose: "Wait until SignalR message is received");

        signalRConnection.GetReceivedMessages().Any(tuple => tuple.Item1.Equals(messageName) && tuple.Item2.Equals(messageValue)).Should()
            .BeTrue($"There should be a message with {messageName} name and {messageValue} value");
    }
}
