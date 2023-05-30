using System.Reflection;
using AutomationFramework.Configuration;
using AutomationWeb.Models.Configuration;
using Microsoft.Extensions.Configuration;

namespace AutomationWeb.Configuration;

public static class AutomationWebConfiguration
{
    public static EnvironmentModel EnvironmentModel { get; private set; }
    public static SecretsModel SecretsModel { get; private set; }
    public static ProjectPropertiesAttribute ProjectProperties { get; private set; }

    [field: ThreadStatic] public static TestThreadScopedModel TestThreadScopedModel { get; private set; }

    private const string EnvironmentFileName = "environment.json";
    private const string EnvironmentFormattedFileName = "environment.{0}.json";

    private static void AddStaticSources()
    {
        // environment.json + environment.DOTNETCORE_ENVIRONMENT.json
        AutomationConfiguration.Instance.AddJsonFile(Path.Combine(ResourcesDirectoryName, ConfigurationDirectoryName, EnvironmentFileName));
        var environmentBasedFileName = string.Format(EnvironmentFormattedFileName, AutomationConfiguration.RuntimeConfigurationModel.AutomationEnvironment);
        AutomationConfiguration.Instance.AddJsonFile(Path.Combine(ResourcesDirectoryName, ConfigurationDirectoryName, environmentBasedFileName), optional: true);

        // CMD args
        AutomationConfiguration.Instance.AddCommandLine(Environment.GetCommandLineArgs());

        // User secrets
        SecretsSetUp.Setup(AutomationConfiguration.RuntimeConfigurationModel.SecretsClient);

        // Environment variables
        AutomationConfiguration.Instance.AddEnvironmentVariables();
    }

    /// <summary>
    /// Use with static configuration Properties. Call in [BeforeTestRun] hook.
    /// </summary>
    public static void InitTestRunConfiguration()
    {
        AddStaticSources();

        // Bind necessary static objects
        EnvironmentModel = AutomationConfiguration.Instance.GetRequiredSection("Environment").Get<EnvironmentModel>();
        SecretsModel = AutomationConfiguration.Instance.GetRequiredSection("Secrets").Get<SecretsModel>();
        ProjectProperties = Assembly.GetCallingAssembly().GetCustomAttribute<ProjectPropertiesAttribute>();
    }

    private static void AddThreadStaticSources()
    {
        // If you have some env variable(or config value), which is set after InitTestRunConfiguration method, you need to manually add dependent resource here
        
        Environment.SetEnvironmentVariable("template:time", "now");    // TODO delete or refactor
        Environment.SetEnvironmentVariable("template:guid", "00000000-0000-0000-0000-000000000000");    // TODO delete or refactor
        AutomationConfiguration.Instance.AddEnvironmentVariables();    // TODO delete or refactor
    }

    /// <summary>
    /// Use with ThreadStatic configuration Properties (Test Thread scoped). Call in [BeforeScenario] hook
    /// </summary>
    public static void InitTestThreadConfiguration()
    {
        AddThreadStaticSources();

        TestThreadScopedModel = AutomationConfiguration.Instance.GetSection("template").Get<TestThreadScopedModel>();
    }
}