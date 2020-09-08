using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using QA_exercise.Pages;
using TechTalk.SpecFlow;

namespace QA_exercise
{
    [Binding]
    public class QaExerciseSteps
    {
        private readonly IWebDriver _driver = new ChromeDriver();

        private const string HomePageUrl = "http://qaexercise.envalfresco.com/settings";

        private const string FilesPageUrl = "http://qaexercise.envalfresco.com/files";

        private const string UserName = "guest@example.com";

        private const string Password = "Password";

        private readonly HomePage _homePage;

        private readonly LoginPage _loginPage;

        private readonly ApplicationPage _applicationPage;

        public QaExerciseSteps()
        {
            _homePage = new HomePage(_driver);

            _loginPage = new LoginPage(_driver);

            _applicationPage = new ApplicationPage(_driver);

            _driver.Manage().Window.Maximize();
        }

        [Given(@"I am on home page")]
        public void GivenIAmOnHomePage()
        {
            _homePage.NavigateToUrl(HomePageUrl);
            _homePage.BodyLoaded();
            _homePage.CheckHomePageIsDisplayed();
        }
        
        [Given(@"I set Provider to \'(.*)\'")]
        public void GivenISetProviderToECM(string p0)
        {
            _homePage.SelectProviderByName(p0);
        }
        
        [When(@"I click Apply")]
        public void WhenIClickApply()
        {
            _homePage.ClickApplyButton();
        }

        [Then(@"I navigate to login page")]
        public void ThenINavigateToLoginPage()
        {
            _loginPage.CheckLoginPageIsDisplayed();
        }

        [When(@"I insert Username and Password")]
        public void WhenIInsertUsernameAndPassword()
        {
            _loginPage.FillUserNameAndPassword(UserName, Password);
        }
        
        [When(@"I click Login")]
        public void WhenIClickLogin()
        {
            _loginPage.ClickLoginButton();
        }

        [Then(@"I navigate to files page")]
        public void ThenINavigateToFilesPage()
        {
            _applicationPage.ClickContentServicesMenu();
            _applicationPage.CheckFilesPageIsDisplayed();
        }

        [When(@"I click on Create New Folder")]
        public void WhenIClickOnCreateNewFolder()
        {
            _applicationPage.ClickNewFolderIcon();
        }

        [Then(@"New folder dialog is displayed")]
        public void ThenNewFolderDialogIsDisplayed()
        {
            Assert.IsTrue(_applicationPage.CheckNewFolderDialogIsDisplayed());
        }

        [When(@"I introduce my username")]
        public void WhenIIntroduceMyUsername()
        {
            _applicationPage.FillNewFolderNameInput(UserName);
        }

        [Then(@"Name has been added")]
        public void ThenNameHasBeenAdded()
        {
            Assert.AreEqual(_applicationPage.GetNewFolderNameInput(), UserName);
        }

        [When(@"I click on create button")]
        public void WhenIClickOnCreateButton()
        {
            _applicationPage.ClickCreateButton();
        }

        [Then(@"The dialog is closed")]
        public void ThenTheDialogIsClosed()
        {
            Assert.IsTrue(_applicationPage.CheckNewFolderDialogIsNotDisplayed());
        }

        [Then(@"The dialog is not closed")]
        public void ThenTheDialogIsNotClosed()
        {
            Assert.IsTrue(_applicationPage.CheckNewFolderDialogIsDisplayed());
        }

        [Then(@"Folder with username is created in current folder")]
        public void ThenFolderWithUsernameIsCreatedInCurrentFolder()
        {
            Assert.IsTrue(_applicationPage.FindFolderByName(UserName));
        }
        
        [Then(@"Message '(.*)' is displayed")]
        public void ThenMessageSAlreadyAFolderWithThisName_TryADifferentNameIsDisplayed(string p0)
        {
            Assert.AreEqual(p0, _applicationPage.GetNotificationText());

            _applicationPage.ClickCancelButton();
        }

        [Then(@"Select the folder with username")]
        public void ThenSelectTheFolderWithUsername()
        {
            _applicationPage.SelectFolderByName(UserName);
        }
        
        [Then(@"Open Options window")]
        public void ThenOpenOptionsWindow()
        {
            _applicationPage.ClickActionMenuButtonForFolderName(UserName);
        }
        
        [Then(@"Click Delete")]
        public void ThenClickDelete()
        {
            _applicationPage.ClickDeleteButton();
        }

        [AfterScenario]
        public void ClosePage()
        {
            _driver.Quit();
        }
    }
}