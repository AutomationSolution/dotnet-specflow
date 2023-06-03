using AutomationWeb.Models.TestData;
using AutomationWeb.Pages;
using TechTalk.SpecFlow;

namespace AutomationWeb.StepDefinitions;

[Binding]
public class DriverStepDefinitions
{
    private readonly LoginPage loginPage = new();

    [Given(@"I am on a (.*)")]
    [Then(@"I am on a (.*)")]
    public void GivenIAmOnA(string pageName)
    {
        // ScenarioContext.StepIsPending();
    }

    [When(@"I fill in (.*) credentials on Login Page")]
    public void WhenIFillInCredentialsOnLoginPage(SimpleUserModel simpleUserModel)
    {
        loginPage.FillUsernameTextBox(simpleUserModel.Username);
        loginPage.FillPasswordTextBox(simpleUserModel.Password);
    }

    [When(@"I click Login button on Login Page")]
    public void WhenIClickLoginButtonOnLoginPage()
    {
        loginPage.ClickLoginButton();
    }
}
