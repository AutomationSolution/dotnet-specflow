using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace AutomationWeb.Pages.TempFolder;

public class MainPage : Form
{
    public MainPage() : base(By.Id("shopping_cart_container"), "Main Page")
    {
    }

    private IButton CartButton => ElementFactory.GetButton(By.Id("shopping_cart_container"), "Cart");

    public void ClickCartButton() => CartButton.Click();
}
