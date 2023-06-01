using Aquality.Appium.Mobile.Applications;
using Aquality.Appium.Mobile.Configurations;
using Aquality.Appium.Mobile.Screens.ScreenFactory;
using Aquality.Selenium.Core.Applications;
using Aquality.Selenium.Core.Configurations;
using Microsoft.Extensions.DependencyInjection;

namespace AutomationMobile.Utilities.Aquality;

internal class CustomStartup : MobileStartup
{
    public override IServiceCollection ConfigureServices(IServiceCollection services, Func<IServiceProvider, IApplication> applicationProvider,
        ISettingsFile settings = null)
    {
        base.ConfigureServices(services, applicationProvider, settings);
        services.AddSingleton<IScreenFactory, CustomScreenFactory>();
        services.AddSingleton<IApplicationProfile, CustomApplicationProfile>();
        return services;
    }
}