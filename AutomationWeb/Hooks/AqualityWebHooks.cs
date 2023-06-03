using Aquality.Selenium.Browsers;
using AutomationWeb.Configuration;
using NLog;
using TechTalk.SpecFlow;

namespace AutomationWeb.Hooks;

[Binding]
public class AqualityWebHooks
{
    [BeforeScenario("UI", Order = 40)]
    public static void SetAqualityStartup(ScenarioContext scenarioContext)
    {
        // AqualityServices.SetStartup(new CustomStartup());
    }    

    [BeforeScenario("UI", Order = 50)]
    public static void OpenBaseUrl(ScenarioContext scenarioContext)
    {
        AqualityServices.Browser.GoTo(AutomationWebConfiguration.EnvironmentModel.GetEndpoint());
    }

    [AfterScenario("UI")]
    public static void CloseDriver()
    {
        LogManager.GetCurrentClassLogger()
            .Debug($"{nameof(AqualityServices.IsBrowserStarted)} is {AqualityServices.IsBrowserStarted}");

        if (AqualityServices.IsBrowserStarted)
        {
            LogManager.GetCurrentClassLogger().Debug($"Calling {nameof(AqualityServices.Browser.Quit)}");

            AqualityServices.Browser.Quit();
        }
    }
}
