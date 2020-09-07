using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace QA_exercise.Pages
{
    class LoginPage
    {
        private readonly IWebDriver _driver;

        private readonly WebDriverWait _wait;

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 30));
        }

        private IWebElement LoginPageIdentification =>
            _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("adf-login-form")));
        private IWebElement UserNameInputField => 
            _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("username")));
        private IWebElement PasswordInputField => 
            _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("password")));
        private IWebElement LoginButton => 
            _wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("login-button")));

        public bool CheckLoginPageIsDisplayed() => LoginPageIdentification.Displayed;
        public void ClickLoginButton() => LoginButton.Click();
        public void FillUserNameAndPassword(string userName, string password)
        {
            UserNameInputField.SendKeys(userName);
            PasswordInputField.SendKeys(password);
        }
    }
}
