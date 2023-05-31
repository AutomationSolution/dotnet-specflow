using AutomationFramework.Configuration;
using AutomationMobile.Models;
using AutomationMobile.Utilities.SpecFlow;
using Microsoft.Extensions.Configuration;
using TechTalk.SpecFlow;

namespace AutomationMobile.Configuration;

public class AutomationMobileConfiguration : IAutomationConfiguration
{
    [field: ThreadStatic] public static DeviceConfigModel DeviceConfigModel { get; set; }

    public void InitThreadStaticConfiguration(ConfigurationManager configurationManagerInstance, ScenarioContext scenarioContext)
    {
        DeviceConfigModel = new DeviceConfigModel(TagsUtilities.GetApplicationName(scenarioContext));
    }
}