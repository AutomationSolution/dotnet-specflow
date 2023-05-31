using System.Reflection;
using AutomationFramework.Configuration;
using AutomationWeb.Models.Configuration;
using AutomationWeb.Models.TestData;
using Microsoft.Extensions.Configuration;
using static AutomationFramework.Configuration.ConfigurationPaths;

namespace AutomationWeb.Configuration;

public class AutomationWebConfiguration : IAutomationConfiguration
{
    public static EnvironmentModel EnvironmentModel { get; private set; }
    public static SecretsModel SecretsModel { get; private set; }
    public static ProjectPropertiesAttribute ProjectProperties { get; private set; }

    [field: ThreadStatic] public static TestThreadScopedModel TestThreadScopedModel { get; private set; }
    [field: ThreadStatic] public static UsersDataModel UsersDataModel { get; private set; }

    public void AddStaticSources(ConfigurationManager configurationManager)
    {
        // environment.json + environment.%Environment%.json
        configurationManager.AddJsonFile(Path.Combine(ResourcesDirectoryName, ConfigurationDirectoryName, EnvironmentFileName));
        var environmentBasedFileName = string.Format(EnvironmentFormattedFileName, AutomationFrameworkConfiguration.RuntimeConfigurationModel.AutomationEnvironment);
        configurationManager.AddJsonFile(Path.Combine(ResourcesDirectoryName, ConfigurationDirectoryName, environmentBasedFileName), optional: true);

        // CMD args
        configurationManager.AddCommandLine(Environment.GetCommandLineArgs());

        // User secrets
        SecretsSetUp.Setup(AutomationFrameworkConfiguration.RuntimeConfigurationModel.SecretsClient, configurationManager);

        // Environment variables
        configurationManager.AddEnvironmentVariables();
    }


    public void InitStaticConfiguration(ConfigurationManager configurationManager)
    {
        // Bind necessary static objects
        EnvironmentModel = configurationManager.GetRequiredSection("Environment").Get<EnvironmentModel>();
        SecretsModel = configurationManager.GetRequiredSection("Secrets").Get<SecretsModel>();
        ProjectProperties = Assembly.GetExecutingAssembly().GetCustomAttribute<ProjectPropertiesAttribute>();
    }

    public void AddThreadStaticSources(ConfigurationManager configurationManager)
    {
        configurationManager.AddJsonFile(Path.Combine(ResourcesDirectoryName, TestDataDirectoryName, UserDataFileName));
        
        Environment.SetEnvironmentVariable("template:time", "now");    // TODO delete or refactor
        Environment.SetEnvironmentVariable("template:guid", "00000000-0000-0000-0000-000000000000");    // TODO delete or refactor
        configurationManager.AddEnvironmentVariables();    // TODO delete or refactor
    }

    public void InitThreadStaticConfiguration(ConfigurationManager configurationManager)
    {
        // Bind necessary thread static objects
        TestThreadScopedModel = configurationManager.GetSection("template").Get<TestThreadScopedModel>();
        UsersDataModel = configurationManager.GetSection("UserData").Get<UsersDataModel>();
    }
}