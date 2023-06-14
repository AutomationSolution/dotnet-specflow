using AutomationAPI.Utilities.SignalR;
using AutomationAPI.Utilities.WCF;
using Grpc.Net.Client;
using NLog;
using TechTalk.SpecFlow;

namespace AutomationAPI.Hooks;

[Binding]
public class DisposeHooks
{
    [AfterScenario("gRPCFeature")]
    public static void DisposeGrpcConnection(ScenarioContext scenarioContext)
    {
        try
        {
            var grpcConnection = scenarioContext.Get<GrpcChannel>();
            grpcConnection.Dispose();
        }
        catch (KeyNotFoundException)
        {
            LogManager.GetCurrentClassLogger().Warn("Tried to dispose gRPC connection, but it wasn't found in scenario conext. Make sure you're using gRPCFeature tag correctly");
        }
    }
    
    [AfterScenario("SignalRFeature")]
    public static void DisposeSignalRConnection(ScenarioContext scenarioContext)
    {
        try
        {
            var signalRConnection = scenarioContext.Get<SignalRConnection>();
            signalRConnection.Dispose();
        }
        catch (KeyNotFoundException)
        {
            LogManager.GetCurrentClassLogger().Warn("Tried to dispose SignalR connection, but it wasn't found in scenario conext. Make sure you're using SignalRFeature tag correctly");
        }
    }
    
    [AfterScenario("WCFFeature")]
    public static void DisposeWcfConnection(ScenarioContext scenarioContext)
    {
        try
        {
            var wcfClient = scenarioContext.Get<WcfClient>();
            wcfClient.Dispose();
        }
        catch (KeyNotFoundException)
        {
            LogManager.GetCurrentClassLogger().Warn("Tried to dispose WCF connection, but it wasn't found in scenario conext. Make sure you're using WCFFeature tag correctly");
        }
    }    

    [AfterScenario("OpenAPIFeature")]
    public static void DisposeHTTPConnection(ScenarioContext scenarioContext)
    {
        try
        {
            var httpClient = scenarioContext.Get<HttpClient>();
            httpClient.Dispose();
        }
        catch (KeyNotFoundException)
        {
            LogManager.GetCurrentClassLogger().Warn("Tried to dispose HTTP connection, but it wasn't found in scenario conext. Make sure you're using OpenAPIFeature tag correctly");
        }
    }
}
