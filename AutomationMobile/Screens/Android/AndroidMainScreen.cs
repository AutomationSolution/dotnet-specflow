﻿using Aquality.Appium.Mobile.Elements.Interfaces;
using AutomationMobile.Enums.FrameworkAdditions;
using AutomationMobile.Utilities.Aquality;
using OpenQA.Selenium.Appium;

namespace AutomationMobile.Screens.Android;

[CustomApplicationName(ApplicationName.AndroidBusiness, ApplicationName.AndroidCustomer)]
public class AndroidMainScreen : MainScreen
{
    public AndroidMainScreen() : base(MobileBy.Id("main"))
    {
    }
    
    private IButton CartButton => ElementFactory.GetButton(MobileBy.XPath("//*[@content-desc='cart badge']"), "Cart");

    public override void ClickCartButton() => CartButton.Click();
}
