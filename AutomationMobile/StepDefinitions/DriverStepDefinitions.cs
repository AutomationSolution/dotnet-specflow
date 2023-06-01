using Aquality.Appium.Mobile.Applications;
using AutomationMobile.Screens;
using TechTalk.SpecFlow;

namespace AutomationMobile.StepDefinitions;

[Binding]
public class DriverStepDefinitions
{
    private readonly MainScreen mainScreen = AqualityServices.ScreenFactory.GetScreen<MainScreen>();

    [Given(@"I run a test with webdriver set up")]
    public void GivenIRunATestWithWebdriverSetUp()
    {
        mainScreen.ClickHelloWorldLabel();
    }
}
