using AutomationFramework.Configuration;
using AutomationMobile.Models;
using AutomationMobile.Models.Configuration;
using AutomationMobile.Models.Configuration.BrowserStack;
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
    [field: ThreadStatic] public static ScenarioMobileDataModel ScenarioMobileDataModel { get; private set; }
    [field: ThreadStatic] public static BrowserStackModel BrowserStackModel { get; private set; }

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
        SecretsMobileModel = configurationManagerInstance.GetRequiredSection(SecretsMobileModel.JsonSectionName).Get<SecretsMobileModel>();
        MobileEnvironment = configurationManagerInstance.GetRequiredSection(MobileEnvironment.JsonSectionName).Get<MobileEnvironment>();
    }

    public void InitThreadStaticConfiguration(ConfigurationManager configurationManagerInstance, ScenarioContext scenarioContext)
    {
        DeviceConfigModel = new DeviceConfigModel(MobileTagsUtilities.GetApplicationName(scenarioContext), MobileTagsUtilities.GetDeviceType(scenarioContext));
        ScenarioMobileDataModel = new ScenarioMobileDataModel(scenarioContext);
        BrowserStackModel = new BrowserStackModel
        {
            Settings = configurationManagerInstance.GetRequiredSection(BrowserStackMobileSettingsModel.JsonSectionName).Get<BrowserStackMobileSettingsModel>(),
            Data = new BrowserStackMobileData()
        };

        AppiumOptions = AutomationAppiumOptionsBuilder.BuildAppiumOptions();
    }
}
