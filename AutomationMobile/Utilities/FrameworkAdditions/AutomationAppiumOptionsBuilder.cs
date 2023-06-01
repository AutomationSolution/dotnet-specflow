using Aquality.Appium.Mobile.Applications;
using AutomationFramework.Configuration;
using AutomationMobile.Configuration;
using AutomationMobile.Enums.FrameworkAdditions;
using NLog;
using OpenQA.Selenium.Appium;
using SpecFlow.Internal.Json;

namespace AutomationMobile.Utilities.FrameworkAdditions;

public class AutomationAppiumOptionsBuilder
{
    public static AppiumOptions BuildAppiumOptions()
    {
        var environmentType = AutomationMobileConfiguration.MobileEnvironment.MobileEnvironmentType;
        LogManager.GetCurrentClassLogger().Info("STARTED building Appium Options for Environment Type: " + environmentType);

        var appiumOptions = new AppiumOptions();

        switch (environmentType)
        {
            case MobileEnvironmentType.BrowserStack:
                var browserstackOptions = new Dictionary<string, object>
                {
                    {"userName", AutomationMobileConfiguration.SecretsMobileModel.BrowserStackUser},
                    {"accessKey", AutomationMobileConfiguration.SecretsMobileModel.BrowserStackKey},
                    {"local", AutomationMobileConfiguration.BrowserStackMobileSettingsModel.BrowserStackLocalTesting},
                    {"gpsLocation", AutomationMobileConfiguration.BrowserStackMobileSettingsModel.BrowserStackDefaultLocation},
                    {"networkLogs", AutomationMobileConfiguration.BrowserStackMobileSettingsModel.BrowserStackEnableNetworkLogs},
                    {"idleTimeout", AutomationMobileConfiguration.BrowserStackMobileSettingsModel.BrowserStackIdleTimeoutSeconds},
                    {"acceptInsecureCerts", AutomationMobileConfiguration.BrowserStackMobileSettingsModel.BrowserStackLocalTestingAcceptSslCerts},
                    {"realMobile", AutomationMobileConfiguration.BrowserStackMobileSettingsModel.BrowserStackRealMobile},
                    {"sessionName", AutomationMobileConfiguration.ScenarioDataModel.InformativeScenarioName},
                    {"buildName", AutomationMobileConfiguration.BrowserStackMobileData.BrowserStackBuild},
                    {"projectName", AutomationMobileConfiguration.BrowserStackMobileSettingsModel.BrowserStackProject},
                    {"enableBiometric", AutomationMobileConfiguration.ScenarioDataModel.BiometricAuthEnabled},
                    {"video", AutomationMobileConfiguration.BrowserStackMobileSettingsModel.BrowserStackVideoRecording},
                    {"interactiveDebugging", AutomationMobileConfiguration.BrowserStackMobileSettingsModel.BrowserStackInteractiveDebugging}
                };
                appiumOptions.AddAdditionalAppiumOption("bstack:options", browserstackOptions);
                appiumOptions.App = AutomationMobileConfiguration.BrowserStackMobileData.BrowserStackAppAddress;

                // Arguments and Environment for iOS Application
                SetPlatformSpecificOptions(appiumOptions);

                // Capability for info only
                appiumOptions.AddAdditionalAppiumOption("automationEnvironment",
                    AutomationFrameworkConfiguration.RuntimeConfigurationModel.AutomationEnvironment);
                appiumOptions.AddAdditionalAppiumOption("secretsClient",
                    AutomationFrameworkConfiguration.RuntimeConfigurationModel.SecretsClient);
                break;
            case MobileEnvironmentType.Local:
            case MobileEnvironmentType.LocalFromExternalNetwork:
                appiumOptions.AddAdditionalAppiumOption("appPackage", "com.android.chrome");
                appiumOptions.AddAdditionalAppiumOption("appActivity", "com.google.android.apps.chrome.Main");
                break;
            default:
                throw new InvalidOperationException($"Unsupported Environment: {environmentType}");
        }

        LogManager.GetCurrentClassLogger().Info("FINISHED building Appium Options for Environment Type: " + environmentType);

        return appiumOptions;
    }

    private static void SetPlatformSpecificOptions(AppiumOptions appiumOptions)
    {
        if (AutomationMobileConfiguration.DeviceConfigModel.PlatformName == PlatformName.IOS)
            appiumOptions.AddAdditionalAppiumOption("processArguments", GetIosProcessArguments());
        
        if (AutomationMobileConfiguration.DeviceConfigModel.PlatformName == PlatformName.Android)
            appiumOptions.AddAdditionalAppiumOption("processArguments", GetAndroidProcessArguments());
    }
    
    private static string GetIosProcessArguments()
    {
        return new Dictionary<string, object>
        {
            {"args", GetIosArguments()},
            {"env", GetIosEnvironmentVariables()}
        }.ToJson();
    }
    
    private static List<string> GetIosArguments()
    {
        return new List<string>
        {
            $"--{AutomationFrameworkConfiguration.RuntimeConfigurationModel.AutomationEnvironment}",
            "--allfeatures"
        };
    }
    
    private static Dictionary<string, string> GetIosEnvironmentVariables()
    {
        return new Dictionary<string, string>
        {
            ["custom_environment_variable1"] = "custom_value",
            ["custom_environment_variable2"] = "custom_value",
            ["custom_environment_variable3"] = "custom_value"
        };
    }

    private static string GetAndroidProcessArguments()
    {
        return string.Empty;    // Implement if needed
    }
}