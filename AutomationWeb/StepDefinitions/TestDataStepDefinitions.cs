using AutomationWeb.Enums.TestData;
using AutomationWeb.Models.TestData;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace AutomationWeb.StepDefinitions;

[Binding]
public class TestDataStepDefinitions
{
    [Then(@"I assert that '(.*)' does have '(.*)' username, '(.*)' status and '(.*)' verification")]
    public void ThenIAssertThatDoesHaveUsernameStatusAndVerification(UserDataModel userDataModel, string username, UserStatus status, UserVerification verification)
    {
        userDataModel.Username.Should().Be(username);
        userDataModel.Status.Should().Be(status);
        userDataModel.Verification.Should().Be(verification);
    }
}