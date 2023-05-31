using AutomationWeb.Configuration;
using NLog;

namespace AutomationWeb.Utilities.NLog;

public static class CustomLayoutRenderers
{
    public static void AddScenarioGuidRenderer()
    {
        LogManager.Setup().SetupExtensions(builder => builder.RegisterLayoutRenderer("scenarioGuid", _ =>
        {
            try
            {
                return AutomationWebConfiguration.ScenarioMetaData.ScenarioGuid;
            }
            catch (NullReferenceException)
            {
                return string.Empty;
            }
        }));
    }
}