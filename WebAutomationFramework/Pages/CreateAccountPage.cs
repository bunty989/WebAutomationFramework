using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace WebAutomationFramework.Pages
{
    internal class CreateAccountPage
    {
        private readonly IWebDriver _driver;

        public CreateAccountPage(IWebDriver driver) => _driver = driver;

        private IWebElement HeaderText => _driver.FindElement(By.CssSelector("#registerPage h3[class^='robo']"));
        private IWebElement UserNameTxtBox => _driver.FindElement(By.CssSelector("[name='usernameRegisterPage']"));
        private IWebElement UserNameErrorLabel => _driver.FindElement(By.CssSelector("[name='usernameRegisterPage'] +label"));
        private IWebElement EmailTxtBox => _driver.FindElement(By.CssSelector("[name='emailRegisterPage']"));
        private IWebElement EmailErrorLabel => _driver.FindElement(By.CssSelector("[name='emailRegisterPage'] +label"));
        private IWebElement PasswordTxtBox => _driver.FindElement(By.CssSelector("[name='passwordRegisterPage']"));
        private IWebElement PasswordErrorLabel => _driver.FindElement(By.CssSelector("[name='passwordRegisterPage'] +label"));
        private IWebElement ConfirmPasswordTxtBox => _driver.FindElement(By.CssSelector("[name='confirm_passwordRegisterPage']"));
        private IWebElement ConfirmPasswordErrorLabel => _driver.FindElement(By.CssSelector("[name='confirm_passwordRegisterPage'] +label"));

        public void WaitForHeaderTextToBeDisplayed()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(driver => HeaderText.Displayed);
        }

        public void EnterUserName(string userName)
        {
            UserNameTxtBox.Clear();
            UserNameTxtBox.SendKeys(userName);
        }

        public void EnterEmail(string email)
        {
            EmailTxtBox.Clear();
            EmailTxtBox.SendKeys(email);
        }

        public void EnterPassword(string password)
        {
            PasswordTxtBox.Clear();
            PasswordTxtBox.SendKeys(password);
        }

        public void EnterConfirmPassword(string confirmPassword)
        {
            ConfirmPasswordTxtBox.Clear();
            ConfirmPasswordTxtBox.SendKeys(confirmPassword);
        }

        public string GetUserNameErrorLabel()
        {
            UserNameTxtBox.Click();
            UserNameTxtBox.SendKeys(Keys.Tab);
            return UserNameErrorLabel.Text;
        }

        public string GetEmailErrorLabel()
        {
            EmailTxtBox.Click();
            EmailTxtBox.SendKeys(Keys.Tab);
            return EmailErrorLabel.Text;
        }

        public string GetPasswordErrorLabel()
        {
            if (_driver is OpenQA.Selenium.Firefox.FirefoxDriver)
            {
                Thread.Sleep(5000);
            }
            PasswordTxtBox.Click();
            PasswordTxtBox.SendKeys(Keys.Tab);
            return PasswordErrorLabel.Text;
        }

        public string GetConfirmPasswordErrorLabel()
        {
            if (_driver is OpenQA.Selenium.Firefox.FirefoxDriver)
            {
                Thread.Sleep(5000);
            }
            ConfirmPasswordTxtBox.Click();
            ConfirmPasswordTxtBox.SendKeys(Keys.Tab);
            return ConfirmPasswordErrorLabel.Text;
        }
    }
}
