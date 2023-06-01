using AutomationMobile.Utilities.BrowserStack;
using TechTalk.SpecFlow;

namespace AutomationMobile.Hooks;

[Binding]
public class BrowserStackHooks
{
    private readonly BrowserStackExecutor browserStackExecutor = new();

    [BeforeStep("UI", Order = 10)]
    public void LogBrowserStackBeforeStep(ScenarioContext scenarioContext)
    {
        browserStackExecutor.AddAnnotation($"Specflow step started: {scenarioContext.StepContext.StepInfo.Text}");
    }

    [AfterStep("UI", Order = 10)]
    public void LogBrowserStackAfterStep(ScenarioContext scenarioContext)
    {
        browserStackExecutor.AddAnnotation($"Specflow step finished: {scenarioContext.StepContext.StepInfo.Text}");
    }

    [AfterScenario("UI", Order = 10)]
    public void SetBrowserStackTestStatus(ScenarioContext scenarioContext)
    {
        browserStackExecutor.ChangeTestStatus(scenarioContext.ScenarioExecutionStatus, scenarioContext.TestError?.Message + scenarioContext.TestError?.StackTrace);
    }
}