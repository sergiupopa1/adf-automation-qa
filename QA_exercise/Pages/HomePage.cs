using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;
using System.Threading;

namespace QA_exercise.Pages
{
    class HomePage
    {
        private readonly IWebDriver _driver;

        private readonly WebDriverWait _wait;

        public HomePage(IWebDriver driver)
        {
            _driver = driver;

            _wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 60));
        }

        private IWebElement HomePageIdentification => 
            _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[@class='ng-tns-c136-1 ng-star-inserted'][contains(text(),'ALL')]")));
        private IWebElement ApplyButton => 
            _wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("host-button")));
        private IWebElement ProviderDropDown => 
            _wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("adf-provider-selector")));
        private IReadOnlyCollection<IWebElement> ProviderDropDownElements => 
            _wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector("span.mat-option-text")));

        public bool CheckHomePageIsDisplayed() => HomePageIdentification.Displayed;
        public string GetProviderDropDownElementAt(int number) => ProviderDropDownElements.ElementAt(number).Text;
        public void ClickProviderDropDownElementAt(int number) => ProviderDropDownElements.ElementAt(number).Click();
        public void ClickApplyButton() => ApplyButton.Click();
        public void ClickProviderDropDown() => ProviderDropDown.Click();

        public void SelectProviderByName(string providerName)
        {
            var name = "";
            var i = 0;

            ClickProviderDropDown();
            
            while (name != providerName && i < ProviderDropDownElements.Count)
            {
                name = GetProviderDropDownElementAt(i);
                i++;
            }

            if (name == providerName)
                ClickProviderDropDownElementAt(--i);
            else
                Assert.Fail("Provider name " + providerName + " cannot be found!");
        }
        public void NavigateToUrl(string URL)
        {
            _driver.Navigate().GoToUrl(URL);
        }
        public void BodyLoaded()
        {
            var i = 0;

            while (_wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body app-root div div div"))).GetAttribute("class").Contains("app-loader-text") && i < 600)
            {
                Thread.Sleep(100);
                i++;
            }
        }
    }
}
