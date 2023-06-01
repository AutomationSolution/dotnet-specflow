using Aquality.Appium.Mobile.Applications;
using AutomationMobile.Utilities.Aquality;
using NLog;
using TechTalk.SpecFlow;

namespace AutomationMobile.Hooks;

[Binding]
public class AqualityHooks
{
    [BeforeScenario("UI")]
    public static void SetAqualityStartup(ScenarioContext scenarioContext)
    {
        AqualityServices.SetStartup(new CustomStartup());
    }
    
    [AfterScenario("UI")]
    public static void CloseDriver()
    {
        LogManager.GetCurrentClassLogger().Debug($"{nameof(AqualityServices.IsApplicationStarted)} is {AqualityServices.IsApplicationStarted}");

        if (AqualityServices.IsApplicationStarted)
        {
            LogManager.GetCurrentClassLogger().Debug($"Calling {nameof(AqualityServices.Application.Quit)}");

            AqualityServices.Application.Quit();
        }
    }
}