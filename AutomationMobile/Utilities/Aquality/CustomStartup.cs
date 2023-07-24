using Aquality.Appium.Mobile.Applications;
using Aquality.Appium.Mobile.Configurations;
using Aquality.Appium.Mobile.Screens.ScreenFactory;
using Aquality.Selenium.Core.Applications;
using Aquality.Selenium.Core.Configurations;
using AutomationMobile.Configuration;
using AutomationMobile.Enums.FrameworkAdditions;
using Microsoft.Extensions.DependencyInjection;

namespace AutomationMobile.Utilities.Aquality;

internal class CustomStartup : MobileStartup
{
    public override IServiceCollection ConfigureServices(IServiceCollection services, Func<IServiceProvider, IApplication> applicationProvider,
        ISettingsFile? settings = null)
    {
        base.ConfigureServices(services, applicationProvider, settings);
        services.AddSingleton<IScreenFactory, CustomScreenFactory>();
        SetApplicationProfile(services);
        return services;
    }

    private static void SetApplicationProfile(IServiceCollection services)
    {
        switch (AutomationMobileConfiguration.MobileEnvironment.MobileExecutionPlatform)
        {
            case MobileExecutionPlatform.BrowserStack:
                services.AddSingleton<IApplicationProfile, BrowserStackApplicationProfile>();
                break;
            case MobileExecutionPlatform.LambdaTest:
                throw new ArgumentOutOfRangeException($"{AutomationMobileConfiguration.MobileEnvironment.MobileExecutionPlatform} is not supported");
            case MobileExecutionPlatform.Local:
            case MobileExecutionPlatform.LocalFromExternalNetwork:
                services.AddSingleton<IApplicationProfile, LocalApplicationProfile>();
                break;  // do nothing and use settings.json by default
            default:
                throw new ArgumentOutOfRangeException($"{AutomationMobileConfiguration.MobileEnvironment.MobileExecutionPlatform} is not supported");
        }
    }
}
