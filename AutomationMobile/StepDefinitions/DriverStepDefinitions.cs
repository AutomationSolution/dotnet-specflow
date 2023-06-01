using AutomationMobile.Screens;
using BoDi;
using TechTalk.SpecFlow;

namespace AutomationMobile.StepDefinitions;

[Binding]
public class DriverStepDefinitions
{
    private readonly MainScreen mainScreen;

    public DriverStepDefinitions(IObjectContainer objectContainer)
    {
        mainScreen = objectContainer.Resolve<MainScreen>();
    }
    
    [Given(@"I run a test with webdriver set up")]
    public void GivenIRunATestWithWebdriverSetUp()
    {
        mainScreen.ClickHelloWorldLabel();
    }
}