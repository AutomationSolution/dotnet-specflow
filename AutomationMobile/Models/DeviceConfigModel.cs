using Aquality.Appium.Mobile.Applications;
using AutomationMobile.Enums.FrameworkAdditions;

namespace AutomationMobile.Models;

public class DeviceConfigModel
{
    public DeviceConfigModel(ApplicationName applicationName)
    {
        ApplicationName = applicationName;
        PlatformName = SetPlatformName(applicationName);
        ApplicationType = SetApplicationType(applicationName);
    }
    
    public PlatformName PlatformName { get; }
    public ApplicationName ApplicationName { get; }
    public ApplicationType ApplicationType { get; }

    private static PlatformName SetPlatformName(ApplicationName applicationName)
    {
        if (applicationName.ToString().Contains(PlatformName.Android.ToString(), StringComparison.CurrentCultureIgnoreCase))
            return PlatformName.Android;
        if (applicationName.ToString().Contains(PlatformName.IOS.ToString(), StringComparison.CurrentCultureIgnoreCase))
            return PlatformName.IOS;
        throw new ArgumentOutOfRangeException(nameof(applicationName), applicationName, $"{nameof(PlatformName)} of given application name is unknown");
    }

    private static ApplicationType SetApplicationType(ApplicationName applicationName)
    {
        if (applicationName.ToString().Contains(ApplicationType.Business.ToString(), StringComparison.CurrentCultureIgnoreCase))
            return ApplicationType.Business;
        if (applicationName.ToString().Contains(ApplicationType.Customer.ToString(), StringComparison.CurrentCultureIgnoreCase))
            return ApplicationType.Customer;
        throw new ArgumentOutOfRangeException(nameof(applicationName), applicationName, $"{nameof(ApplicationType)} of given application name is unknown");
    }
}