using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace WebAutomationFramework.Pages
{
    internal class LandingPage
    {
        private readonly IWebDriver _driver;
        public LandingPage(IWebDriver driver)
        {
            _driver = driver;
        }

        private IWebElement Spinner => _driver.FindElement(By.CssSelector(".loader div svg"));
        private IWebElement Products => _driver.FindElement(By.CssSelector("#our_products"));
        private IWebElement UserBtn => _driver.FindElement(By.CssSelector("#menuUserLink"));
        private IWebElement CreateNewAccount => _driver.FindElement(By.CssSelector("[class='login ng-scope'] [translate='CREATE_NEW_ACCOUNT']"));

        public void WaitForSpinnerToDisappear()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(driver => !Spinner.Displayed);
        }

        public void LandingPageIsDisplayed()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(driver => Products.Displayed);
        }

        public void ClickUserBtn()
        {
            if (_driver is OpenQA.Selenium.Firefox.FirefoxDriver)
            {
                Thread.Sleep(5000);
            }
            UserBtn.Click();
        }

        public void ClickCreateNewAccount()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(driver => CreateNewAccount.Enabled);
            var executor = _driver as IJavaScriptExecutor;
            executor?.ExecuteScript("arguments[0].click();", CreateNewAccount);
            //CreateNewAccount.Click();
            if (_driver is OpenQA.Selenium.Firefox.FirefoxDriver)
            {
                Thread.Sleep(5000);
            }
        }
    }
}
