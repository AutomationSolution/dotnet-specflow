using Aquality.Appium.Mobile.Applications;
using AutomationFramework.Configuration;
using AutomationMobile.Configuration;
using AutomationMobile.Enums.FrameworkAdditions;
using NLog;
using OpenQA.Selenium.Appium;
using SpecFlow.Internal.Json;

namespace AutomationMobile.Utilities.FrameworkAdditions;

public static class AutomationAppiumOptionsBuilder
{
    public static AppiumOptions BuildAppiumOptions()
    {
        var executionPlatform = AutomationMobileConfiguration.MobileEnvironment.MobileExecutionPlatform;
        LogManager.GetCurrentClassLogger().Info("STARTED building Appium Options for Environment Type: " + executionPlatform);

        var appiumOptions = new AppiumOptions();

        switch (executionPlatform)
        {
            case MobileExecutionPlatform.BrowserStack:
                var browserstackOptions = new Dictionary<string, object>
                {
                    {"userName", AutomationMobileConfiguration.SecretsMobileModel.BrowserStackUser},
                    {"accessKey", AutomationMobileConfiguration.SecretsMobileModel.BrowserStackKey},
                    {"local", AutomationMobileConfiguration.BrowserStackModel.Settings.BrowserStackLocalTesting},
                    {"gpsLocation", AutomationMobileConfiguration.BrowserStackModel.Settings.BrowserStackDefaultLocation},
                    {"networkLogs", AutomationMobileConfiguration.BrowserStackModel.Settings.BrowserStackEnableNetworkLogs},
                    {"idleTimeout", AutomationMobileConfiguration.BrowserStackModel.Settings.BrowserStackIdleTimeoutSeconds},
                    {"acceptInsecureCerts", AutomationMobileConfiguration.BrowserStackModel.Settings.BrowserStackLocalTestingAcceptSslCerts},
                    {"realMobile", AutomationMobileConfiguration.BrowserStackModel.Settings.BrowserStackRealMobile},
                    {"sessionName", AutomationMobileConfiguration.ScenarioMobileDataModel.InformativeScenarioName},
                    {"buildName", AutomationMobileConfiguration.BrowserStackModel.Data.BrowserStackBuild},
                    {"projectName", AutomationMobileConfiguration.BrowserStackModel.Settings.BrowserStackProject},
                    {"enableBiometric", AutomationMobileConfiguration.ScenarioMobileDataModel.BiometricAuthEnabled},
                    {"video", AutomationMobileConfiguration.BrowserStackModel.Settings.BrowserStackVideoRecording},
                    {"interactiveDebugging", AutomationMobileConfiguration.BrowserStackModel.Settings.BrowserStackInteractiveDebugging}
                };
                appiumOptions.AddAdditionalAppiumOption("bstack:options", browserstackOptions);
                appiumOptions.App = AutomationMobileConfiguration.BrowserStackModel.Data.BrowserStackAppAddress;

                // Arguments and Environment for iOS Application
                SetPlatformSpecificOptions(appiumOptions);

                // Capability for info only
                appiumOptions.AddAdditionalAppiumOption("automationEnvironment",
                    AutomationFrameworkConfiguration.RuntimeConfigurationModel.AutomationEnvironment);
                appiumOptions.AddAdditionalAppiumOption("secretsClient",
                    AutomationFrameworkConfiguration.RuntimeConfigurationModel.SecretsClient);
                break;
            case MobileExecutionPlatform.Local:
            case MobileExecutionPlatform.LocalFromExternalNetwork:
                appiumOptions.AddAdditionalAppiumOption("appPackage", "com.android.chrome");
                appiumOptions.AddAdditionalAppiumOption("appActivity", "com.google.android.apps.chrome.Main");
                break;
            default:
                throw new InvalidOperationException($"Unsupported Environment: {executionPlatform}");
        }

        LogManager.GetCurrentClassLogger().Info("FINISHED building Appium Options for Environment Type: " + executionPlatform);

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
