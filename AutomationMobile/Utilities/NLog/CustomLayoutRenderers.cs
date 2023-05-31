using AutomationMobile.Configuration;
using NLog;

namespace AutomationMobile.Utilities.NLog;

public static class CustomLayoutRenderers
{
    public static void AddApplicationNameLayoutRenderer()
    {
        LogManager.Setup().SetupExtensions(builder => builder.RegisterLayoutRenderer("applicationName", _ =>
        {
            try
            {
                return AutomationMobileConfiguration.DeviceConfigModel.ApplicationName;
            }
            catch (NullReferenceException)
            {
                return string.Empty;
            }
        }));
    }
}