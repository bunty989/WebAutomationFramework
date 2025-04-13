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
        }

        [When("I dont enter anything to username and email fields")]
        public void WhenIDontEnterAnythingToUsernameAndEmailFields()
        {
            _createAccountPage?.WaitForHeaderTextToBeDisplayed();
        }


        [Then("I see the {string} error message is displayed")]
        public void ThenISeeTheErrorMessageIsDisplayed(string errorMessage)
        {
            var errorLabel = errorMessage.Contains("Email") ? _createAccountPage?.GetEmailErrorLabel() : _createAccountPage?.GetUserNameErrorLabel();
            Assert.That(errorLabel, Is.EqualTo(errorMessage));
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

        [Then("I dont see any error message for {string} field")]
        public void ThenIDontSeeAnyErrorMessageForField(string fieldName)
        {
            var errorLabel = fieldName.ToLowerInvariant().Equals("email") ? _createAccountPage?.GetEmailErrorLabel() : _createAccountPage?.GetUserNameErrorLabel();
            Assert.That(errorLabel.ToLowerInvariant(), Is.EqualTo(fieldName.ToLowerInvariant()));
        }

    }
}
