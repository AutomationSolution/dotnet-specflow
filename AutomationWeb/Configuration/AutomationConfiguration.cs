using AutomationWeb.Models.Configuration;
using Microsoft.Extensions.Configuration;

namespace AutomationWeb.Configuration;

public static class AutomationConfiguration
{
    public static AppSettingsModel AppSettingsModel { get; private set; }
    public static EnvironmentModel EnvironmentModel { get; private set; }

    [field: ThreadStatic] public static TestThreadScopedModel TestThreadScopedModel { get; private set; }

    public static ConfigurationManager ConfigurationManagerInstance { get; private set; } // Exposed to public due to testing reasons. Should be private by defauly

    private const string AppSettingsFileName = "appsettings.json";
    private const string EnvironmentFileName = "environment.json";
    private const string EnvironmentFormattedFileName = "environment.{0}.json";
    private static readonly string ConfigurationResourcesPath = Path.Combine("Resources", "Configuration");

    private static void InitConfigurationManager()
    {
        ConfigurationManagerInstance = new ConfigurationManager();
    }

    private static void AddStaticSources()
    {
        // appsettings.json
        ConfigurationManagerInstance.AddJsonFile(AppSettingsFileName);
        AppSettingsModel = ConfigurationManagerInstance.Get<AppSettingsModel>(); // Partial load to use correct environment.json file

        // environment.json + environment.DOTNETCORE_ENVIRONMENT.json
        ConfigurationManagerInstance.AddJsonFile(Path.Combine(ConfigurationResourcesPath, EnvironmentFileName));
        ConfigurationManagerInstance.AddJsonFile(
            Path.Combine(ConfigurationResourcesPath, string.Format(EnvironmentFormattedFileName, AppSettingsModel.DOTNETCORE_ENVIRONMENT)),
            optional: true);

        // User secrets
        // x.AddUserSecrets();  // TODO implement

        // Environment variables
        ConfigurationManagerInstance.AddEnvironmentVariables();
    }

    /// <summary>
    /// Use with static configuration Properties. Call in [BeforeTestRun] hook.
    /// </summary>
    public static void InitTestRunConfiguration()
    {
        InitConfigurationManager();
        AddStaticSources();

        // Bind necessary static objects
        AppSettingsModel = ConfigurationManagerInstance.Get<AppSettingsModel>();
        EnvironmentModel = ConfigurationManagerInstance.GetRequiredSection("environment").Get<EnvironmentModel>();
    }

    private static void AddThreadStaticSources()
    {
        // If you have some env variable(or config value), which is set after InitTestRunConfiguration method, you need to manually add dependent resource here
        
        Environment.SetEnvironmentVariable("template:time", "now");    // TODO delete or refactor
        Environment.SetEnvironmentVariable("template:guid", "00000000-0000-0000-0000-000000000000");    // TODO delete or refactor
        ConfigurationManagerInstance.AddEnvironmentVariables();    // TODO delete or refactor
    }

    /// <summary>
    /// Use with ThreadStatic configuration Properties (Test Thread scoped). Call in [BeforeScenario] hook
    /// </summary>
    public static void InitTestThreadConfiguration()
    {
        AddThreadStaticSources();

        TestThreadScopedModel = ConfigurationManagerInstance.GetSection("template").Get<TestThreadScopedModel>();
    }
}