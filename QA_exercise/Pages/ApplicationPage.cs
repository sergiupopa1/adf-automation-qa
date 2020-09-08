using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace QA_exercise.Pages
{
    class ApplicationPage
    {
        private readonly IWebDriver _driver;

        private readonly WebDriverWait _wait;

        public ApplicationPage(IWebDriver driver)
        {
            _driver = driver;

            _wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 30));
        }

        private IWebElement FilesPageIdentification => 
            _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//mat-panel-title[contains(text(),'Recent Files')]")));
        private IWebElement ContentServicesMenu => 
            _wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("[data-automation-id='Content Services']")));
        private IWebElement NewFolderIcon => 
            _wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("[data-automation-id='create-new-folder']")));
        private IWebElement NewFolderNameInput => 
            _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("adf-folder-name-input")));
        private IWebElement CreateButton =>
            _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("adf-folder-create-button")));
        private IWebElement NewFolderDialog =>
            _wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("mat-dialog-container[aria-modal='true']")));        
        private IWebElement Notification =>
            _wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("snack-bar-container")));
        private IWebElement DeleteButton =>
            _wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("[data-automation-id='DOCUMENT_LIST.ACTIONS.FOLDER.DELETE']")));        
        private IWebElement CancelButton =>
            _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("adf-folder-cancel-button")));        
        private IReadOnlyCollection<IWebElement> FilesTableElements => 
            _wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("//adf-datatable-row[@aria-label]")));

        public string GetFileNameElementAt(int number) => FilesTableElements.ElementAt(number).GetAttribute("aria-label");
        public void ClickFileNameElementAt(int number) => FilesTableElements.ElementAt(number).Click();
        public bool CheckFilesPageIsDisplayed() => FilesPageIdentification.Displayed;        
        public void ClickContentServicesMenu() => ContentServicesMenu.Click();
        public void ClickNewFolderIcon() => NewFolderIcon.Click();
        public void ClickCreateButton() => CreateButton.Click();        
        public void FillNewFolderNameInput(string name) => NewFolderNameInput.SendKeys(name);
        public string GetNewFolderNameInput() => NewFolderNameInput.GetAttribute("value");
        public string GetNotificationText() => Notification.Text;        
        public void ClickDeleteButton() => DeleteButton.Click();
        public void ClickCancelButton() => CancelButton.Click();        
        public bool CheckNewFolderDialogIsDisplayed() => NewFolderDialog.Displayed;
        public bool CheckNewFolderDialogIsNotDisplayed() => _wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector("mat-dialog-container[aria-modal='true']")));

        public void ClickActionMenuButtonForFolderName(string folderName)
        {
            for (var i = 0; i < FilesTableElements.Count; i++)
            {
                if (GetFileNameElementAt(i).Contains(folderName))
                {
                    FilesTableElements.ElementAt(i).FindElement(By.XPath(".//button[@aria-label='Actions']")).Click();
                    break;
                }
            }
        }        
        public void SelectFolderByName(string folderName)
        {
            try
            {
                for (var i = 0; i < FilesTableElements.Count; i++)
                {
                    if (GetFileNameElementAt(i).Contains(folderName))
                    {
                        ClickFileNameElementAt(i);
                        break;
                    }
                }
            }
            catch (WebDriverTimeoutException)
            {
                Assert.Fail("Folder with name " + folderName + " not found!");
            }
        }
        public bool FindFolderByName(string folderName)
        {
            var result = false;
            try
            {
                for (var i = 0; i < FilesTableElements.Count; i++)
                {
                    if (GetFileNameElementAt(i).Contains(folderName))
                    {
                        result = true;
                        break;
                    }
                }
            }
            catch(WebDriverTimeoutException)
            {
                result = false;
            }            

            return result;
        }        
    }
}
