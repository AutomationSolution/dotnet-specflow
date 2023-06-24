using Aquality.Appium.Mobile.Applications;
using TechTalk.SpecFlow;
using static AutomationFramework.Configuration.ConfigurationPaths;

namespace AutomationMobile.Utilities.BrowserStack;

public static class BrowserStackExecutor
{
    private const string AnnotationFileName = "AddAnnotationScript.json";
    private const string TestStatusChangeScript = "TestStatusChangeScript.json";
    
    public static object AddAnnotation(string annotation, string annotationLevel = "info")
    {
        var scriptBase = GetScriptBase(AnnotationFileName);
        var script = BrowserStackScriptsFormatter.ReplaceBeforeFormat(scriptBase);

        var formattedScript = string.Format(script, annotation, annotationLevel);

        var scriptToExecute = BrowserStackScriptsFormatter.ReplaceAfterFormat(formattedScript);
        return Execute(scriptToExecute);
    }

    public static object ChangeTestStatus(ScenarioExecutionStatus scenarioExecutionStatus, string reason)
    {
        var status = scenarioExecutionStatus == ScenarioExecutionStatus.OK ? "passed" : "failed";
        
        var scriptBase = GetScriptBase(TestStatusChangeScript);
        var script = BrowserStackScriptsFormatter.ReplaceBeforeFormat(scriptBase);

        var formattedScript = string.Format(script, status, reason);

        var scriptToExecute = BrowserStackScriptsFormatter.ReplaceAfterFormat(formattedScript);
        return Execute(scriptToExecute);
    }

    private static object Execute(string script)
    {
        var browserstackExecutorScript = $"browserstack_executor: {script}";

        return AqualityServices.Application.Driver.ExecuteScript(browserstackExecutorScript);
    }

    private static string GetScriptBase(string scriptFileName)
    {
        var pathToTestDataFile = Path.Combine(ResourcesDirectoryName, BrowserStackDirectoryName, BrowserStackScriptsDirectoryName, scriptFileName);
        return File.ReadAllText(pathToTestDataFile).Replace("\r\n", "");
    }
}
