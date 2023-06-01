using AutomationFramework.Configuration;
using AutomationMobile.Models;
using AutomationMobile.Models.Configuration;
using AutomationMobile.Utilities.FrameworkAdditions;
using AutomationMobile.Utilities.SpecFlow;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium.Appium;
using TechTalk.SpecFlow;
using static AutomationFramework.Configuration.ConfigurationPaths;

namespace AutomationMobile.Configuration;

public class AutomationMobileConfiguration : IAutomationConfiguration
{
    public static SecretsMobileModel SecretsMobileModel { get; set; }
    public static MobileEnvironment MobileEnvironment { get; private set; }

    [field: ThreadStatic] public static DeviceConfigModel DeviceConfigModel { get; set; }
    [field: ThreadStatic] public static AppiumOptions AppiumOptions { get; set; }
    [field: ThreadStatic] public static BrowserStackMobileSettingsModel BrowserStackMobileSettingsModel { get; set; }
    [field: ThreadStatic] public static ScenarioDataModel ScenarioDataModel { get; private set; }
    [field: ThreadStatic] public static BrowserStackMobileData BrowserStackMobileData { get; private set; }


    public void AddStaticSources(ConfigurationManager configurationManagerInstance)
    {
        // User secrets
        SecretsSetUp.Setup(AutomationFrameworkConfiguration.RuntimeConfigurationModel.SecretsClient, configurationManagerInstance);
        
        configurationManagerInstance.AddJsonFile(Path.Combine(ResourcesDirectoryName, ConfigurationDirectoryName,
            MobileEnvironmentFileName));
    }

    public void AddThreadStaticSources(ConfigurationManager configurationManagerInstance)
    {
        configurationManagerInstance.AddJsonFile(Path.Combine(ResourcesDirectoryName, BrowserStackDirectoryName,
            BrowserStackSettingsFileName));
    }

    public void InitStaticConfiguration(ConfigurationManager configurationManagerInstance)
    {
        SecretsMobileModel = configurationManagerInstance.GetRequiredSection("Secrets").Get<SecretsMobileModel>();
        MobileEnvironment = configurationManagerInstance.GetRequiredSection("MobileEnvironment").Get<MobileEnvironment>();
    }

    public void InitThreadStaticConfiguration(ConfigurationManager configurationManagerInstance, ScenarioContext scenarioContext)
    {
        DeviceConfigModel = new DeviceConfigModel(TagsUtilities.GetApplicationName(scenarioContext), TagsUtilities.GetDeviceType(scenarioContext));
        BrowserStackMobileSettingsModel = configurationManagerInstance.GetRequiredSection("BrowserStackSettings").Get<BrowserStackMobileSettingsModel>();
        ScenarioDataModel = new ScenarioDataModel(scenarioContext);
        BrowserStackMobileData = new BrowserStackMobileData();

        AppiumOptions = AutomationAppiumOptionsBuilder.BuildAppiumOptions();
    }
}
