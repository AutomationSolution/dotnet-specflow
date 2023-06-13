using FluentAssertions;
using Grpc.Net.Client;
using TechTalk.SpecFlow;

namespace AutomationAPI.StepDefinitions;

[Binding]
public class GrpcStepDefinitions
{
    private readonly ScenarioContext scenarioContext;

    public GrpcStepDefinitions(ScenarioContext scenarioContext)
    {
        this.scenarioContext = scenarioContext;
    }

    [Given(@"gRPC connection is established")]
    public void GivenGrpcConnectionIsEstablished()
    {
        var grpcChannel = GrpcChannel.ForAddress("https://localhost:7278");
        scenarioContext.Set(grpcChannel);
    }

    [Given(@"Greeter service is initialized")]
    public void GivenGreeterServiceIsInitialized()
    {
        var grpcChannel = scenarioContext.Get<GrpcChannel>();
        var greeterClient = new Greeter.GreeterClient(grpcChannel);
        scenarioContext.Set(greeterClient);
    }

    [When(@"I send SayHello gRPC request with '(.*)' message")]
    public void WhenISendSayHelloGrpcRequestWithMessage(string message)
    {
        var greeterClient = scenarioContext.Get<Greeter.GreeterClient>();
        var response = greeterClient.SayHello(new HelloRequest {Name = message});
        scenarioContext.Set(response);
    }

    [Then(@"I assert that SayHello gRPC response contains '(.*)'")]
    public void ThenIAssertThatSayHelloGrpcResponseContains(string expectedString)
    {
        var grpcResponse = scenarioContext.Get<HelloReply>();
        grpcResponse.Message.Should().Contain(expectedString, $"Response message should contain {expectedString}");
    }
}
