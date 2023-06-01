using Aquality.Appium.Mobile.Applications;
using AutomationMobile.Enums.FrameworkAdditions;
using AutomationMobile.Extensions.FrameworkAdditions;
using OpenQA.Selenium;

namespace AutomationMobile.Utilities.FrameworkAdditions;

public static class ContextUtils
{
    #region Web frames contexts

    public static void DoInCurrentFrame(Action action, IWebElement frameElement = null)
    {
        if (frameElement == null)
            SwitchToCurrentFrame();
        else
            SwitchToCurrentFrame(frameElement);

        action.Invoke();
        SwitchToDefaultContent();
    }

    public static T DoInCurrentFrame<T>(Func<T> action, IWebElement frameElement = null)
    {
        if (frameElement == null)
            SwitchToCurrentFrame();
        else
            SwitchToCurrentFrame(frameElement);

        var result = action.Invoke();
        SwitchToDefaultContent();
        return result;
    }


    public static T DoInDefaultFrame<T>(Func<T> action, IWebElement frameElement = null)
    {
        SwitchToDefaultContent();
        var result = action.Invoke();
        if (frameElement == null)
            SwitchToCurrentFrame();
        else
            SwitchToCurrentFrame(frameElement);

        return result;
    }

    public static void SwitchToCurrentFrame()
    {
        AqualityServices.Application.Driver.SwitchTo().Frame(0);
    }

    public static void SwitchToCurrentFrame(IWebElement frameElement)
    {
        AqualityServices.Application.Driver.SwitchTo().Frame(frameElement);
    }

    public static void SwitchToDefaultContent()
    {
        AqualityServices.Application.Driver.SwitchTo().DefaultContent();
    }

    #endregion

    #region Native contexts

    public static void ChangeContext(MobileScreenContext screenContext, int contextCount = 0)
    {
        switch (screenContext)
        {
            case MobileScreenContext.NativeContext:
                AqualityServices.Application.SwitchToAppSession();
                break;
            case MobileScreenContext.WebViewContext:
                AqualityServices.Application.SwitchToWebSession(contextCount);
                break;
            default:
                throw new InvalidOperationException(
                    $"Can't detect proper screen context to switch for {screenContext} context");
        }
    }

    public static T DoInWebViewContext<T>(Func<T> func)
    {
        AqualityServices.Application.SwitchToWebSession();
        var result = func.Invoke();
        AqualityServices.Application.SwitchToAppSession();
        return result;
    }

    public static void DoInWebViewContext(Action action)
    {
        AqualityServices.Application.SwitchToWebSession();
        action.Invoke();
        AqualityServices.Application.SwitchToAppSession();
    }

    public static T DoInNativeContext<T>(Func<T> func)
    {
        AqualityServices.Application.SwitchToAppSession();
        var result = func.Invoke();
        AqualityServices.Application.SwitchToWebSession();
        return result;
    }

    public static void DoInNativeContext(Action action)
    {
        AqualityServices.Application.SwitchToAppSession();
        action.Invoke();
        AqualityServices.Application.SwitchToWebSession();
    }

    #endregion
}
