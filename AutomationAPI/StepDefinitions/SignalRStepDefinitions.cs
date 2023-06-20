using AutomationAPI.Utilities.SignalR;
using AutomationFramework.Utilities.Polly;
using FluentAssertions;
using NLog;
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
        // TODO implement conditional wait for established connection, as it is connecting asynchronously, or make connection establishing synchronous
        ConditionalWait.WaitFor<string>(() =>
        {
            LogManager.GetCurrentClassLogger().Debug("Code block execution. Wait for NOT NULL");
            return null;
        }, message: "Custom message on exception 1", codePurpose: "Wait until SignalR connection is established");
        
        ConditionalWait.WaitForTrue(() =>
        {
            LogManager.GetCurrentClassLogger().Debug("Code block execution. Wait for TRUE");
            return signalRConnection.IsConnectionEstablished();
        }, message: "Custom message on exception 2");
        
        signalRConnection.SendMessage(messageName, messageValue).Wait();
    }

    [Then(@"I assert that SignalR message with '(.*)' name and '(.*)' value is received")]
    public void ThenIAssertThatSignalRMessageWithNameAndValueIsReceived(string messageName, string messageValue)
    {
        var signalRConnection = scenarioContext.Get<SignalRConnection>();
        // TODO implement conditional wait for established connection and for new messages received
        Thread.Sleep(TimeSpan.FromSeconds(3));

        signalRConnection.IsConnectionEstablished().Should().BeTrue("Connection should be established");
        signalRConnection.GetReceivedMessages().Count.Should().BeGreaterThan(0, "Received messages should not be empty");
        signalRConnection.GetReceivedMessages().Any(tuple => tuple.Item1.Equals(messageName) && tuple.Item2.Equals(messageValue)).Should()
            .BeTrue($"There should be a message with {messageName} name and {messageValue} value");
    }
}
