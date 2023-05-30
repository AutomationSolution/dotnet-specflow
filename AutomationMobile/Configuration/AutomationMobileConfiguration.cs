using AutomationMobile.Models;
using static AutomationFramework.Configuration.ConfigurationPaths;

namespace AutomationMobile.Configuration;

public static class AutomationMobileConfiguration
{
    [field: ThreadStatic] public static DeviceConfigModel DeviceConfigModel { get; set; }

    private static void AddStaticSources()
    {
        // // environment.json + environment.DOTNETCORE_ENVIRONMENT.json
        // AutomationConfiguration.Instance.AddJsonFile(Path.Combine(ResourcesDirectoryName, ConfigurationDirectoryName, EnvironmentFileName));
        // var environmentBasedFileName = string.Format(EnvironmentFormattedFileName, AutomationConfiguration.RuntimeConfigurationModel.AutomationEnvironment);
        // AutomationConfiguration.Instance.AddJsonFile(Path.Combine(ResourcesDirectoryName, ConfigurationDirectoryName, environmentBasedFileName), optional: true);
        //
        // // CMD args
        // AutomationConfiguration.Instance.AddCommandLine(Environment.GetCommandLineArgs());
        //
        // // User secrets
        // SecretsSetUp.Setup(AutomationConfiguration.RuntimeConfigurationModel.SecretsClient);
        //
        // // Environment variables
        // AutomationConfiguration.Instance.AddEnvironmentVariables();
    }

    /// <summary>
    /// Use with static configuration Properties. Call in [BeforeTestRun] hook.
    /// </summary>
    public static void InitTestRunConfiguration()
    {
        // AddStaticSources();
        //
        // // Bind necessary static objects
        // EnvironmentModel = AutomationConfiguration.Instance.GetRequiredSection("Environment").Get<EnvironmentModel>();
        // SecretsModel = AutomationConfiguration.Instance.GetRequiredSection("Secrets").Get<SecretsModel>();
        // ProjectProperties = Assembly.GetCallingAssembly().GetCustomAttribute<ProjectPropertiesAttribute>();
    }

    private static void AddThreadStaticSources()
    {
        // AutomationConfiguration.Instance.AddJsonFile(Path.Combine(ResourcesDirectoryName, TestDataDirectoryName, UserDataFileName));
        //
        // Environment.SetEnvironmentVariable("template:time", "now");    // TODO delete or refactor
        // Environment.SetEnvironmentVariable("template:guid", "00000000-0000-0000-0000-000000000000");    // TODO delete or refactor
        // AutomationConfiguration.Instance.AddEnvironmentVariables();    // TODO delete or refactor
    }

    /// <summary>
    /// Use with ThreadStatic configuration Properties (Test Thread scoped). Call in [BeforeScenario] hook
    /// </summary>
    public static void InitTestThreadConfiguration()
    {
        // AddThreadStaticSources();
        //
        // TestThreadScopedModel = AutomationConfiguration.Instance.GetSection("template").Get<TestThreadScopedModel>();
        // UsersDataModel = AutomationConfiguration.Instance.GetSection("UserData").Get<UsersDataModel>();
    }
}