using Aquality.Appium.Mobile.Applications;
using AutomationMobile.Enums.Aquality;
using AutomationMobile.Enums.FrameworkAdditions;

namespace AutomationMobile.Models;

public class DeviceConfigModel
{
    public DeviceConfigModel(ApplicationName applicationName, DeviceType deviceType)
    {
        ApplicationName = applicationName;
        PlatformName = SetPlatformName(applicationName);
        ApplicationType = SetApplicationType(applicationName);
        DeviceType = deviceType;
        DeviceName = SetDeviceName(PlatformName, deviceType);
    }
    
    public PlatformName PlatformName { get; }
    public ApplicationName ApplicationName { get; }
    public ApplicationType ApplicationType { get; }
    public DeviceType DeviceType { get; }
    public DeviceName DeviceName { get; }

    private PlatformName SetPlatformName(ApplicationName applicationName)
    {
        if (applicationName.ToString().Contains(PlatformName.Android.ToString(), StringComparison.CurrentCultureIgnoreCase))
            return PlatformName.Android;
        if (applicationName.ToString().Contains(PlatformName.IOS.ToString(), StringComparison.CurrentCultureIgnoreCase))
            return PlatformName.IOS;
        throw new ArgumentOutOfRangeException(nameof(applicationName), applicationName, $"{nameof(PlatformName)} of given application name is unknown");
    }

    private ApplicationType SetApplicationType(ApplicationName applicationName)
    {
        if (applicationName.ToString().Contains(ApplicationType.Business.ToString(), StringComparison.CurrentCultureIgnoreCase))
            return ApplicationType.Business;
        if (applicationName.ToString().Contains(ApplicationType.Customer.ToString(), StringComparison.CurrentCultureIgnoreCase))
            return ApplicationType.Customer;
        throw new ArgumentOutOfRangeException(nameof(applicationName), applicationName, $"{nameof(ApplicationType)} of given application name is unknown");
    }

    private DeviceName SetDeviceName(PlatformName platformName, DeviceType deviceType)
    {
        if (platformName == PlatformName.Android)
        {
            if (deviceType == DeviceType.Phone) return DeviceName.AndroidPhone;
            if (deviceType == DeviceType.Tablet) return DeviceName.AndroidTablet;
        }

        if (platformName == PlatformName.IOS)
        {
            if (deviceType == DeviceType.Phone) return DeviceName.Iphone;
            if (deviceType == DeviceType.Tablet) return DeviceName.Ipad;
        }

        throw new ArgumentOutOfRangeException($"Not supported platform: {platformName}");
    }
}