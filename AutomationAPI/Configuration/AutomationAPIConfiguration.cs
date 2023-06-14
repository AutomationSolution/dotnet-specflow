using AutomationAPI.Models.Configuration;
using AutomationFramework.Configuration;
using Microsoft.Extensions.Configuration;
using static AutomationFramework.Configuration.ConfigurationPaths;

namespace AutomationAPI.Configuration;

public class AutomationAPIConfiguration : IAutomationConfiguration
{
    public static SignalRDataModel SignalRData { get; private set; }
    public static GrpcDataModel GrpcData { get; private set; }
    public static OpenApiDataModel OpenApiData { get; private set; }

    public void AddStaticSources(ConfigurationManager configurationManagerInstance)
    {
        configurationManagerInstance.AddJsonFile(Path.Combine(ResourcesDirectoryName, ConfigurationDirectoryName, SignalRSettingsFileName));
        configurationManagerInstance.AddJsonFile(Path.Combine(ResourcesDirectoryName, ConfigurationDirectoryName, GrpcSettingsFileName));
        configurationManagerInstance.AddJsonFile(Path.Combine(ResourcesDirectoryName, ConfigurationDirectoryName, OpenApiSettingsFileName));
    }

    public void InitStaticConfiguration(ConfigurationManager configurationManagerInstance)
    {
        SignalRData = configurationManagerInstance.GetRequiredSection("SignalR").Get<SignalRDataModel>();
        GrpcData = configurationManagerInstance.GetRequiredSection("gRPC").Get<GrpcDataModel>();
        OpenApiData = configurationManagerInstance.GetRequiredSection("OpenAPI").Get<OpenApiDataModel>();
    }
}
