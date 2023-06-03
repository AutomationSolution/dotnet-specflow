using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace AutomationWeb.Pages;

public class LoginPage : Form
{
    public LoginPage() : base(By.Id("login-button"), "Login page")
    {
    }

    private ITextBox UsernameTextBox => ElementFactory.GetTextBox(By.Id("user-name"), "Username");
    private ITextBox PasswordTextBox => ElementFactory.GetTextBox(By.Id("password"), "Password");
    private IButton LoginButton => ElementFactory.GetButton(By.Id("login-button"), "Login");

    public void FillUsernameTextBox(string username) => UsernameTextBox.SendKeys(username);

    public void FillPasswordTextBox(string password) => PasswordTextBox.SendKeys(password);

    public void ClickLoginButton() => LoginButton.Click();
}
