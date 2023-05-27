using AutomationWeb.Models.Configuration;
using Microsoft.Extensions.Configuration;

namespace AutomationWeb.Configuration;

public static class AutomationConfiguration
{
    public static AutomationEnvironmentModel AutomationEnvironmentModel { get; private set; }
    public static AutomationLoggingModel AutomationLoggingModel { get; private set; }
    public static ConfigurationManager ConfigurationManagerInstance { get; private set; }
    
    public static void InitializeAutomationConfiguration()
    {
        // Init Configuration manager
        ConfigurationManagerInstance = new ConfigurationManager();

        // Add sources
        ConfigurationManagerInstance.AddJsonFile(Path.Combine("appsettings.json"));
        ConfigurationManagerInstance.AddJsonFile(Path.Combine("Resources", "Configuration", "environment.json"));
        // x.AddUserSecrets();  // TODO implement
        ConfigurationManagerInstance.AddEnvironmentVariables();

        // Bind settings from configuration manager to a model
        AutomationEnvironmentModel = ConfigurationManagerInstance.GetSection("environment").Get<AutomationEnvironmentModel>();
        AutomationLoggingModel = ConfigurationManagerInstance.GetSection("Logging").Get<AutomationLoggingModel>();
    }
}