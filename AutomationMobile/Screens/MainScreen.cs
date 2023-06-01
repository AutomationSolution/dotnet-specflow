using Aquality.Appium.Mobile.Elements.Interfaces;
using Aquality.Appium.Mobile.Screens;
using OpenQA.Selenium.Appium;

namespace AutomationMobile.Screens;

public class MainScreen : Screen
{
    public MainScreen() : base(MobileBy.Id("main"), "Main app screen")
    {
    }

    private IButton HelloWorldButton => ElementFactory.GetButton(MobileBy.Id("helloWorld"), "Hello world");

    public void ClickHelloWorldLabel() => HelloWorldButton.Click();
}