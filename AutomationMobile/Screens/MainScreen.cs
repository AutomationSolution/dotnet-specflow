using Aquality.Appium.Mobile.Screens;
using OpenQA.Selenium;

namespace AutomationMobile.Screens;

public abstract class MainScreen : Screen
{
    protected MainScreen(By locator) : base(locator, "Main app screen")
    {
    }

    public abstract void ClickHelloWorldLabel();
}