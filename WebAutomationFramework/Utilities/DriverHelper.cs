using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace WebAutomationFramework.Utilities
{
    public static class DriverHelper
    {
        public static IWebDriver? Driver { get; private set; }

        public static void InitializeDriver()
        {
            var browser = ConfigHelper.ReadConfigValue(TestConstant.ConfigTypes.WebDriverConfig, TestConstant.ConfigTypesKey.Browser);
            switch (browser.ToLowerInvariant())
            {
                case "chrome":
                {
                    var chromeOption = new ChromeOptions();
                    chromeOption.AddArguments("start-maximized", "--disable-gpu", "--no-sandbox");
                    chromeOption.AddExcludedArgument("enable-automation");
                    chromeOption.AddUserProfilePreference("credentials_enable_service", false);
                    chromeOption.AddUserProfilePreference("profile.password_manager_enabled", false);
                    chromeOption.PageLoadStrategy = PageLoadStrategy.Eager;
                    Driver = new ChromeDriver(chromeOption);
                    break;
                }
                case "firefox":
                {
                    var ffOptions = new FirefoxOptions
                    {
                        AcceptInsecureCertificates = true
                    };
                    ffOptions.SetPreference("permissions.default.image", 2);
                    ffOptions.PageLoadStrategy = PageLoadStrategy.Eager;
                    Driver = new FirefoxDriver(ffOptions);
                    break;
                }
                case "edge":
                {
                    var edgeOptions = new EdgeOptions
                    {
                        AcceptInsecureCertificates = true,
                        PageLoadStrategy = PageLoadStrategy.Eager
                    };
                    Driver = new EdgeDriver(edgeOptions);
                    break;
                }
                case "chromeheadless":
                {
                        var chromeOption = new ChromeOptions();
                        chromeOption.AddArguments("start-maximized", "--disable-gpu", "--no-sandbox", "--headless=new");
                        chromeOption.AddExcludedArgument("enable-automation");
                        chromeOption.AddUserProfilePreference("credentials_enable_service", false);
                        chromeOption.AddUserProfilePreference("profile.password_manager_enabled", false);
                        chromeOption.PageLoadStrategy = PageLoadStrategy.Eager;
                        Driver = new ChromeDriver(chromeOption);
                        break;
                }
                default:
                    Driver = new ChromeDriver();
                    break;
            }
            Driver.Manage().Window.Maximize();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(int.Parse(ConfigHelper.ReadConfigValue
                    (TestConstant.ConfigTypes.WebDriverConfig, TestConstant.ConfigTypesKey.ImplicitWaitTimeout)));
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(int.Parse(ConfigHelper.ReadConfigValue
                    (TestConstant.ConfigTypes.WebDriverConfig, TestConstant.ConfigTypesKey.PageLoadTimeOut)));
        }

        public static void QuitDriver()
        {
            Driver?.Quit();
        }
    }
}
