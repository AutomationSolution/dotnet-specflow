using Aquality.Appium.Mobile.Applications;
using static Aquality.Appium.Mobile.Applications.AqualityServices;

namespace AutomationMobile.Extensions.FrameworkAdditions;

public static class AppExtensions
{
    private const string NativeAppContext = "NATIVE_APP";
    private const string WebViewContextNamePart = "WEBVIEW";

    public static void SwitchToAppSession(this IMobileApplication mobileApplication)
    {
        if (!mobileApplication.Driver.Context.Equals(NativeAppContext)) mobileApplication.Driver.Context = NativeAppContext;
    }

    public static void SwitchToWebSession(this IMobileApplication mobileApplication, int contextCount = 0)
    {
        ConditionalWait.WaitForTrue(mobileApplication.IsWebViewAvailable, message:
            $"Web view was not found among available contexts: {string.Join(", ", mobileApplication.Driver.Contexts)}");

        var contextToSwitch =
            mobileApplication.Driver.Contexts.Where(context => context.Contains(WebViewContextNamePart)).Skip(contextCount).First();

        if (!mobileApplication.Driver.Context.Equals(contextToSwitch)) mobileApplication.Driver.Context = contextToSwitch;
    }

    public static void SwitchToWebSession(this IMobileApplication mobileApplication, TimeSpan timeoutInSeconds)
    {
        ConditionalWait.WaitForTrue(mobileApplication.IsWebViewAvailable, message:
            $"Web view was not found among available contexts: {string.Join(", ", mobileApplication.Driver.Contexts)}", timeout: timeoutInSeconds);

        var contextToSwitch =
            mobileApplication.Driver.Contexts.First(context => context.Contains(WebViewContextNamePart));

        if (!mobileApplication.Driver.Context.Equals(contextToSwitch))
            mobileApplication.Driver.Context = mobileApplication.Driver.Contexts.First(context => context.Contains(WebViewContextNamePart));
    }

    private static bool IsWebViewAvailable(this IMobileApplication mobileApplication) => 
        mobileApplication.Driver.Contexts.Any(context => context.Contains(WebViewContextNamePart));
}