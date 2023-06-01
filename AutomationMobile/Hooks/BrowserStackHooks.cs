using AutomationMobile.Utilities.BrowserStack;
using TechTalk.SpecFlow;

namespace AutomationMobile.Hooks;

[Binding]
public class BrowserStackHooks
{
    private readonly BrowserStackExecutor browserStackExecutor = new();
    
    [BeforeStep(Order = 1)]
    public void LogBrowserStackBeforeStep(ScenarioContext scenarioContext)
    {
        browserStackExecutor.AddAnnotation($"Specflow step started: {scenarioContext.StepContext.StepInfo.Text}");
    }
    
    [AfterStep(Order = 1)]
    public void LogBrowserStackAfterStep(ScenarioContext scenarioContext)
    {
        browserStackExecutor.AddAnnotation($"Specflow step finished: {scenarioContext.StepContext.StepInfo.Text}");
    }
    
    [AfterScenario(Order = 1)]
    public void SetBrowserStackTestStatus(ScenarioContext scenarioContext)
    {
        browserStackExecutor.ChangeTestStatus(scenarioContext.ScenarioExecutionStatus, scenarioContext.TestError?.Message + scenarioContext.TestError?.StackTrace);
    }
}