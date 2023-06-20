using AutomationFramework.Configuration;
using AutomationWeb.Configuration;
using AutomationWeb.Enums.FrameworkAdditions;
using NLog;
using OpenQA.Selenium;

namespace AutomationWeb.Utilities.FrameworkAdditions;

public static class AutomationDriverOptionsBuilder
{
    public static DriverOptions AddDriverOptions(DriverOptions driverOptions)
    {
        var environmentType = AutomationWebConfiguration.WebEnvironment.WebExecutionPlatform;
        LogManager.GetCurrentClassLogger().Info("STARTED building Appium Options for Environment Type: " + environmentType);
        
        switch (environmentType)
        {
            case WebExecutionPlatform.BrowserStack:
                var browserstackOptions = new Dictionary<string, object>
                {
                    {"userName", AutomationWebConfiguration.SecretsWebModel.BrowserStackUser},
                    {"accessKey", AutomationWebConfiguration.SecretsWebModel.BrowserStackKey},
                    {"os", AutomationWebConfiguration.BrowserStackModel.Settings.BrowserStackOS},
                    {"osVersion", AutomationWebConfiguration.BrowserStackModel.Settings.BrowserStackOSVersion},
                    {"local", AutomationWebConfiguration.BrowserStackModel.Settings.BrowserStackLocalTesting},
                    {"gpsLocation", AutomationWebConfiguration.BrowserStackModel.Settings.BrowserStackDefaultLocation},
                    {"networkLogs", AutomationWebConfiguration.BrowserStackModel.Settings.BrowserStackEnableNetworkLogs},
                    {"idleTimeout", AutomationWebConfiguration.BrowserStackModel.Settings.BrowserStackIdleTimeoutSeconds},
                    {"acceptInsecureCerts", AutomationWebConfiguration.BrowserStackModel.Settings.BrowserStackLocalTestingAcceptSslCerts},
                    {"sessionName", AutomationWebConfiguration.ScenarioWebDataModel.InformativeScenarioName},
                    {"buildName", AutomationWebConfiguration.BrowserStackModel.Data.BrowserStackBuild},
                    {"projectName", AutomationWebConfiguration.BrowserStackModel.Settings.BrowserStackProject},
                    {"video", AutomationWebConfiguration.BrowserStackModel.Settings.BrowserStackVideoRecording},
                    {"interactiveDebugging", AutomationWebConfiguration.BrowserStackModel.Settings.BrowserStackInteractiveDebugging}
                };
                driverOptions.AddAdditionalOption("bstack:options", browserstackOptions);

                // Capability for info only
                driverOptions.AddAdditionalOption("automationEnvironment",
                    AutomationFrameworkConfiguration.RuntimeConfigurationModel.AutomationEnvironment);
                driverOptions.AddAdditionalOption("secretsClient",
                    AutomationFrameworkConfiguration.RuntimeConfigurationModel.SecretsClient);
                break;
            case WebExecutionPlatform.Local:
            case WebExecutionPlatform.LocalFromExternalNetwork:
                break;
            default:
                throw new InvalidOperationException($"Unsupported Environment: {environmentType}");
        }

        LogManager.GetCurrentClassLogger().Info("FINISHED building Appium Options for Environment Type: " + environmentType);

        return driverOptions;
    }
}
