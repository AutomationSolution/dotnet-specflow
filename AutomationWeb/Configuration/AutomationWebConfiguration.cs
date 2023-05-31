using System.Reflection;
using AutomationFramework.Configuration;
using AutomationWeb.Models.Configuration;
using AutomationWeb.Models.TestData;
using Microsoft.Extensions.Configuration;
using TechTalk.SpecFlow;
using static AutomationFramework.Configuration.ConfigurationPaths;

namespace AutomationWeb.Configuration;

public class AutomationWebConfiguration : IAutomationConfiguration
{
    public static EnvironmentModel EnvironmentModel { get; private set; }
    public static SecretsModel SecretsModel { get; private set; }
    public static ProjectPropertiesAttribute ProjectProperties { get; private set; }

    [field: ThreadStatic] public static ScenarioMetaData ScenarioMetaData { get; private set; }
    [field: ThreadStatic] public static UsersDataModel UsersDataModel { get; private set; }

    private readonly Action<BinderOptions> binderOptionsThrowOnError = options => options.ErrorOnUnknownConfiguration = true;

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
        EnvironmentModel = configurationManager.GetRequiredSection("Environment").Get<EnvironmentModel>(binderOptionsThrowOnError);
        SecretsModel = configurationManager.GetRequiredSection("Secrets").Get<SecretsModel>(binderOptionsThrowOnError);
        ProjectProperties = Assembly.GetExecutingAssembly().GetCustomAttribute<ProjectPropertiesAttribute>();
    }

    public void AddThreadStaticSources(ConfigurationManager configurationManager)
    {
        configurationManager.AddJsonFile(Path.Combine(ResourcesDirectoryName, TestDataDirectoryName, UserDataFileName));
    }

    public void InitThreadStaticConfiguration(ConfigurationManager configurationManager, ScenarioContext scenarioContext)
    {
        ScenarioMetaData = configurationManager.GetSection("ScenarioMetaData").Get<ScenarioMetaData>(binderOptionsThrowOnError) ?? new ScenarioMetaData();
        UsersDataModel = configurationManager.GetSection("UserData").Get<UsersDataModel>();
    }
}