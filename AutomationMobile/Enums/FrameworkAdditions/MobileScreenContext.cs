using System.ComponentModel;

namespace AutomationMobile.Enums.FrameworkAdditions;

public enum MobileScreenContext
{
    [Description("NATIVE")] NativeContext,
    [Description("WEBVIEW")] WebViewContext
}