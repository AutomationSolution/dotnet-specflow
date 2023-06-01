using Aquality.Appium.Mobile.Elements.Interfaces;
using AutomationMobile.Enums.FrameworkAdditions;
using AutomationMobile.Utilities.Aquality;
using OpenQA.Selenium.Appium;

namespace AutomationMobile.Screens.Ios;

[CustomApplicationName(ApplicationName.iOSBusiness, ApplicationName.iOSCustomer)]
public class IosMainScreen : MainScreen
{
    public IosMainScreen() : base(MobileBy.Id("main"))
    {
    }
    
    private IButton HelloWorldButton => ElementFactory.GetButton(MobileBy.Name("tab bar option cart"), "Hello world");

    public override void ClickHelloWorldLabel() => HelloWorldButton.Click();
}
