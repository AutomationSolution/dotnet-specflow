using System.Reflection;
using Aquality.Appium.Mobile.Applications;
using Aquality.Appium.Mobile.Screens;
using Aquality.Appium.Mobile.Screens.ScreenFactory;
using AutomationMobile.Configuration;

namespace AutomationMobile.Utilities.Aquality;

public class CustomScreenFactory : IScreenFactory
{
    /// <summary>
    ///     Returns an implementation of a particular app screen.
    /// </summary>
    /// <typeparam name="TAppScreen">Desired application screen.</typeparam>
    public TAppScreen GetScreen<TAppScreen>() where TAppScreen : IScreen
    {
        Type screenType;
        try
        {
            screenType = Assembly.Load(AqualityServices.ApplicationProfile.ScreensLocation)
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(TAppScreen)))
                .Where(t => t.IsDefined(typeof(CustomApplicationNameAttribute), false))
                .SingleOrDefault(t =>
                {
                    var attribute =
                        (CustomApplicationNameAttribute) Attribute.GetCustomAttribute(t,
                            typeof(CustomApplicationNameAttribute));
                    return attribute != null && attribute.ApplicationNameList.Contains(AutomationMobileConfiguration.DeviceConfigModel.ApplicationName);
                });
        }
        catch (FileNotFoundException ex)
        {
            throw new InvalidOperationException("Could not find Assembly with Screens. " +
                                                "Please specify value \"screensLocation\" in settings file", ex);
        }

        if (screenType == null)
            throw new InvalidOperationException($"Implementation for Screen {typeof(TAppScreen).Name} " +
                                                $"for application type {AutomationMobileConfiguration.DeviceConfigModel.ApplicationName} " +
                                                $"was not found in Assembly {AqualityServices.ApplicationProfile.ScreensLocation}");

        return (TAppScreen) Activator.CreateInstance(screenType);
    }
}