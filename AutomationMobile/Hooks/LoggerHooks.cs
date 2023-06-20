using AutomationMobile.Utilities.NLog;
using TechTalk.SpecFlow;

namespace AutomationMobile.Hooks;

[Binding]
public class LoggerHooks
{
    [BeforeTestRun(Order = 50)]
    public static void BeforeTestRunInitLoggerLayoutRenderers()
    {
        CustomLayoutRenderers.AddApplicationNameLayoutRenderer();
    }
}
