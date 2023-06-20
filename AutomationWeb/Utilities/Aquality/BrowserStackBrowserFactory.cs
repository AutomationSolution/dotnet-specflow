using Aquality.Selenium.Browsers;
using Aquality.Selenium.Configurations;
using Aquality.Selenium.Core.Utilities;
using AutomationWeb.Configuration;
using AutomationWeb.Utilities.FrameworkAdditions;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace AutomationWeb.Utilities.Aquality;

public class BrowserStackBrowserFactory : RemoteBrowserFactory
{
    public BrowserStackBrowserFactory() : base(AqualityServices.Get<IActionRetrier>(), AqualityServices.Get<IBrowserProfile>(),
        AqualityServices.Get<ITimeoutConfiguration>(), AqualityServices.LocalizedLogger)
    {
    }

    protected override WebDriver Driver
    {
        get
        {
            LocalizedLogger.Info("loc.browser.grid");
            var capabilities = AutomationDriverOptionsBuilder.AddDriverOptions(BrowserProfile.DriverSettings.DriverOptions).ToCapabilities();
            
            try
            {
                return new RemoteWebDriver(AutomationWebConfiguration.BrowserStackModel.Settings.BrowserStackHubUrl, capabilities, TimeoutConfiguration.Command);
            }
            catch (Exception e)
            {
                LocalizedLogger.Fatal("loc.browser.grid.fail", e);
                throw;
            }
        }
    }
}
