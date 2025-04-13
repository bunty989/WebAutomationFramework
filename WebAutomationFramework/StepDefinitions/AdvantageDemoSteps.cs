using NUnit.Framework;
using OpenQA.Selenium;
using WebAutomationFramework.Pages;
using WebAutomationFramework.Utilities;
using Reqnroll;

namespace WebAutomationFramework.StepDefinitions
{
    [Binding]
    public class AdvantageDemoSteps
    {
        private LandingPage? _landingPage;
        private CreateAccountPage? _createAccountPage;
        private IWebDriver? _driver;
        private List<string> errorLabels = new List<string>();
        private string[] _errorLabel;

        public AdvantageDemoSteps(ScenarioContext scenarioContext)
        {
            _driver = DriverHelper.Driver;
            _landingPage = new LandingPage(_driver);
            _createAccountPage = new CreateAccountPage(_driver);
        }

        [Given("I navigate to the landing page of the app")]
        public void GivenINavigateToTheLandingPageOfTheApp()
        {
            _driver?.Navigate().GoToUrl(ConfigHelper.ReadConfigValue
                    (TestConstant.ConfigTypes.AppConfig, TestConstant.ConfigTypesKey.AppUrl));
        }

        [When("I see the page is loaded")]
        public void WhenISeeThePageIsLoaded()
        {
            _landingPage?.WaitForSpinnerToDisappear();
            _landingPage?.LandingPageIsDisplayed();
        }


        [When("I click the user button to create new user")]
        public void WhenIClickTheUserButtonToCreateNewUser()
        {
            _landingPage?.ClickUserBtn();
            _landingPage?.ClickCreateNewAccount();
            _createAccountPage?.WaitForHeaderTextToBeDisplayed();
        }

        [When("I dont enter anything to username and email fields")]
        public void WhenIDontEnterAnythingToUsernameAndEmailFields()
        {
            errorLabels?.Add(_createAccountPage?.GetEmailErrorLabel());
            errorLabels?.Add(_createAccountPage?.GetUserNameErrorLabel());

        }

        [When("I dont enter anything to password and confirm password fields")]
        public void WhenIDontEnterAnythingToPasswordAndConfirmPasswordFields()
        {
            errorLabels.Add(_createAccountPage?.GetPasswordErrorLabel());
            errorLabels.Add(_createAccountPage?.GetConfirmPasswordErrorLabel());
        }

        [Then("I see the {string} error message is displayed")]
        public void ThenISeeTheErrorMessageIsDisplayed(string errorMessage)
        {
            _errorLabel = errorLabels.ToArray();
            var errorKey = errorMessage.Split(" ")[0];
            var selectedError = _errorLabel.FirstOrDefault(label => label?.Contains(errorKey) == true);
            Assert.That(selectedError, Is.EqualTo(errorMessage));
        }

        [When("I enter {string} to username field")]
        public void WhenIEnterToUsernameField(string userName)
        {
            _createAccountPage?.EnterUserName(userName);
        }

        [When("I enter {string} to email field")]
        public void WhenIEnterToEmailField(string email)
        {
            _createAccountPage?.EnterEmail(email);
        }

        [When("I enter {string} to password field")]
        public void WhenIEnterToPasswordField(string password)
        {
            _createAccountPage?.EnterPassword(password);
        }

        [When("I enter {string} to confirm password field")]
        public void WhenIEnterToConfirmPasswordField(string confirmPassword)
        {
            _createAccountPage?.EnterConfirmPassword(confirmPassword);
        }

        [Then("I dont see any error message for {string} field")]
        public void ThenIDontSeeAnyErrorMessageForField(string fieldName)
        {
            var errorKey = GetErrorLabels();
            var selectedError = errorKey.FirstOrDefault(label => label?.ToLowerInvariant().Contains(fieldName) == true);
            Assert.That(selectedError.ToLowerInvariant(), Is.EqualTo(fieldName.ToLowerInvariant()));
        }

        private string[] GetErrorLabels()
        {
            errorLabels.Add(_createAccountPage?.GetUserNameErrorLabel());
            errorLabels.Add(_createAccountPage?.GetEmailErrorLabel());
            errorLabels.Add(_createAccountPage?.GetPasswordErrorLabel());
            errorLabels.Add(_createAccountPage?.GetConfirmPasswordErrorLabel());
            return errorLabels.ToArray();

        }
    }
}
