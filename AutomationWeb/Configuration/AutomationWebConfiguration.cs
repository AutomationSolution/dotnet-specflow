using System.Reflection;
using AutomationFramework.Configuration;
using AutomationWeb.Models;
using AutomationWeb.Models.Configuration;
using AutomationWeb.Models.Configuration.BrowserStack;
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
    public static WebEnvironment WebEnvironment { get; private set; }

    [field: ThreadStatic] public static ScenarioMetaData ScenarioMetaData { get; private set; }
    [field: ThreadStatic] public static UsersDataModel UsersDataModel { get; private set; }
    [field: ThreadStatic] public static UsersCredentialsModel UsersCredentialsModel { get; private set; }
    [field: ThreadStatic] public static BrowserStackModel BrowserStackModel { get; private set; }
    [field: ThreadStatic] public static ScenarioWebDataModel ScenarioWebDataModel { get; private set; }

    private readonly Action<BinderOptions> binderOptionsThrowOnError = options => options.ErrorOnUnknownConfiguration = false;

    public void AddStaticSources(ConfigurationManager configurationManagerInstance)
    {
        // environment.json + environment.%Environment%.json
        configurationManagerInstance.AddJsonFile(Path.Combine(ResourcesDirectoryName, ConfigurationDirectoryName, EnvironmentFileName));
        var environmentBasedFileName = string.Format(EnvironmentFormattedFileName, AutomationFrameworkConfiguration.RuntimeConfigurationModel.AutomationEnvironment);
        configurationManagerInstance.AddJsonFile(Path.Combine(ResourcesDirectoryName, ConfigurationDirectoryName, environmentBasedFileName), optional: true);
        configurationManagerInstance.AddJsonFile(Path.Combine(ResourcesDirectoryName, ConfigurationDirectoryName, WebEnvironmentFileName));

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
        WebEnvironment = configurationManagerInstance.GetRequiredSection("WebEnvironment").Get<WebEnvironment>();
    }

    public void AddThreadStaticSources(ConfigurationManager configurationManagerInstance)
    {
        configurationManagerInstance.AddJsonFile(Path.Combine(ResourcesDirectoryName, TestDataDirectoryName, UserDataFileName));
        configurationManagerInstance.AddJsonFile(Path.Combine(ResourcesDirectoryName, TestDataDirectoryName, UserCredentialsFileName));
        configurationManagerInstance.AddJsonFile(Path.Combine(ResourcesDirectoryName, BrowserStackDirectoryName, BrowserStackSettingsFileName));
    }

    public void InitThreadStaticConfiguration(ConfigurationManager configurationManagerInstance, ScenarioContext scenarioContext)
    {
        ScenarioMetaData = configurationManagerInstance.GetSection("ScenarioMetaData").Get<ScenarioMetaData>(binderOptionsThrowOnError) ?? new ScenarioMetaData();
        UsersDataModel = configurationManagerInstance.GetSection("UserData").Get<UsersDataModel>();
        UsersCredentialsModel = configurationManagerInstance.GetSection("UsersCredentials").Get<UsersCredentialsModel>();
        ScenarioWebDataModel = new ScenarioWebDataModel(scenarioContext);

        BrowserStackModel = new BrowserStackModel()
        {
            Settings = configurationManagerInstance.GetRequiredSection("BrowserStackSettings").Get<BrowserStackWebSettingsModel>(),
            Data = new BrowserStackWebData()
        };

        // DriverOptions = AutomationDriverOptionsBuilder.AddDriverOptions();
    }
}
