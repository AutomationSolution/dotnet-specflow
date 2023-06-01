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
    public static SecretsWebModel SecretsWebModel { get; private set; }
    public static ProjectPropertiesAttribute ProjectProperties { get; private set; }

    [field: ThreadStatic] public static ScenarioMetaData ScenarioMetaData { get; private set; }
    [field: ThreadStatic] public static UsersDataModel UsersDataModel { get; private set; }

    private readonly Action<BinderOptions> binderOptionsThrowOnError = options => options.ErrorOnUnknownConfiguration = true;

    public void AddStaticSources(ConfigurationManager configurationManagerInstance)
    {
        // environment.json + environment.%Environment%.json
        configurationManagerInstance.AddJsonFile(Path.Combine(ResourcesDirectoryName, ConfigurationDirectoryName, EnvironmentFileName));
        var environmentBasedFileName = string.Format(EnvironmentFormattedFileName, AutomationFrameworkConfiguration.RuntimeConfigurationModel.AutomationEnvironment);
        configurationManagerInstance.AddJsonFile(Path.Combine(ResourcesDirectoryName, ConfigurationDirectoryName, environmentBasedFileName), optional: true);

        // CMD args
        configurationManagerInstance.AddCommandLine(Environment.GetCommandLineArgs());

        // User secrets
        SecretsSetUp.Setup(AutomationFrameworkConfiguration.RuntimeConfigurationModel.SecretsClient, configurationManagerInstance);

        // Environment variables
        configurationManagerInstance.AddEnvironmentVariables();
    }


    public void InitStaticConfiguration(ConfigurationManager configurationManagerInstance)
    {
        // Bind necessary static objects
        EnvironmentModel = configurationManagerInstance.GetRequiredSection("Environment").Get<EnvironmentModel>(binderOptionsThrowOnError);
        SecretsWebModel = configurationManagerInstance.GetRequiredSection("Secrets").Get<SecretsWebModel>(binderOptionsThrowOnError);
        ProjectProperties = Assembly.GetExecutingAssembly().GetCustomAttribute<ProjectPropertiesAttribute>();
    }

    public void AddThreadStaticSources(ConfigurationManager configurationManagerInstance)
    {
        configurationManagerInstance.AddJsonFile(Path.Combine(ResourcesDirectoryName, TestDataDirectoryName, UserDataFileName));
    }

    public void InitThreadStaticConfiguration(ConfigurationManager configurationManagerInstance, ScenarioContext scenarioContext)
    {
        ScenarioMetaData = configurationManagerInstance.GetSection("ScenarioMetaData").Get<ScenarioMetaData>(binderOptionsThrowOnError) ?? new ScenarioMetaData();
        UsersDataModel = configurationManagerInstance.GetSection("UserData").Get<UsersDataModel>();
    }
}