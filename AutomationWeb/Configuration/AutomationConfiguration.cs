using AutomationWeb.Models.Configuration;
using Microsoft.Extensions.Configuration;

namespace AutomationWeb.Configuration;

public static class AutomationConfiguration
{
    public static AppSettingsModel AppSettingsModel { get; private set; }
    public static EnvironmentModel EnvironmentModel { get; private set; }
    public static ConfigurationManager ConfigurationManagerInstance { get; private set; }

    private const string AppSettingsFileName = "appsettings.json";
    private const string EnvironmentFileName = "environment.json";
    private const string EnvironmentFormattedFileName = "environment.{0}.json";
    private static readonly string ConfigurationResourcesPath = Path.Combine("Resources", "Configuration");

    public static void InitializeAutomationConfiguration()
    {
        // Init Configuration manager
        ConfigurationManagerInstance = new ConfigurationManager();

        // Add sources
        ConfigurationManagerInstance.AddJsonFile(AppSettingsFileName);
        AppSettingsModel = ConfigurationManagerInstance.Get<AppSettingsModel>(); // Partial load to use correct environment.json file

        ConfigurationManagerInstance.AddJsonFile(Path.Combine(ConfigurationResourcesPath, EnvironmentFileName));
        ConfigurationManagerInstance.AddJsonFile(
            Path.Combine(ConfigurationResourcesPath, string.Format(EnvironmentFormattedFileName, AppSettingsModel.DOTNETCORE_ENVIRONMENT)),
            optional: true);
        EnvironmentModel = ConfigurationManagerInstance.GetSection("environment").Get<EnvironmentModel>();

        // x.AddUserSecrets();  // TODO implement

        ConfigurationManagerInstance.AddEnvironmentVariables();

        // Full load
        AppSettingsModel = ConfigurationManagerInstance.Get<AppSettingsModel>();
        EnvironmentModel = ConfigurationManagerInstance.GetSection("environment").Get<EnvironmentModel>();
    }
}