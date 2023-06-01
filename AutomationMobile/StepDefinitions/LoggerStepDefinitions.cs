using AutomationMobile.Configuration;
using FluentAssertions;
using NLog;
using TechTalk.SpecFlow;

namespace AutomationMobile.StepDefinitions;

[Binding]
public class LoggerStepDefinitions
{
    [Then(@"I assert that layout renderer is set up correctly")]
    public void ThenIAssertThatLayoutRendererIsSetUpCorrectly()
    {
        // Do everything in one step to not capture SpecFlow logs
        // Arrange
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        // Act
        LogManager.GetCurrentClassLogger().Warn($"Custom message");

        // Assert
        var consoleOutput = stringWriter.ToString();
        consoleOutput.Should().Contain(AutomationMobileConfiguration.DeviceConfigModel.ApplicationName.ToString());
        consoleOutput.Should().Contain("WARN");
        consoleOutput.Should().Contain("Custom message");
    }
}
