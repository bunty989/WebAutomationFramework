using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Safari;
using Browser = WebAutomationFramework.Utilities.TestConstant.BrowserType;

namespace WebAutomationFramework.Utilities
{
    public static class DriverHelper
    {
        public static IWebDriver? Driver { get; private set; }

        public static void InitializeDriver()
        {
            var browser = ConfigHelper.ReadConfigValue(TestConstant.ConfigTypes.WebDriverConfig, TestConstant.ConfigTypesKey.Browser);
            var browserType = Enum.Parse<Browser>(browser, true);
            switch (browserType)
            {
                case Browser.Chrome or Browser.ChromeHeadless or Browser.ChromeIncognito:
                {
                    var chromeOption = new ChromeOptions();
                    chromeOption.AddArguments("start-maximized", "--disable-gpu", "--no-sandbox");
                    if (browserType == Browser.ChromeHeadless)
                    {
                        chromeOption.AddArguments("window-size=1280,800", "--headless=new");
                    }
                    else if (browserType == Browser.ChromeIncognito)
                    {
                        chromeOption.AddArguments("--incognito");
                    }
                    chromeOption.AddExcludedArgument("enable-automation");
                    chromeOption.AddUserProfilePreference("credentials_enable_service", false);
                    chromeOption.AddUserProfilePreference("profile.password_manager_enabled", false);
                    chromeOption.PageLoadStrategy = PageLoadStrategy.Eager;
                    Driver = new ChromeDriver(chromeOption);
                    break;
                }
                case Browser.InternetExplorer:
                {
                    var ieOptions = new InternetExplorerOptions
                    {
                        IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                        RequireWindowFocus = true,
                        EnsureCleanSession = true,
                        IgnoreZoomLevel = true
                    };
                    ieOptions.AddAdditionalInternetExplorerOption(CapabilityType.AcceptSslCertificates, true);
                    ieOptions.PageLoadStrategy = PageLoadStrategy.Eager;
                    Driver = new InternetExplorerDriver(ieOptions);
                    break;
                }
                case Browser.Firefox or Browser.FirefoxHeadless:
                {
                    var ffOptions = new FirefoxOptions
                    {
                        AcceptInsecureCertificates = true
                    };
                    if (browserType == Browser.FirefoxHeadless)
                    {
                        ffOptions.AddArguments("-headless", "--width=1280", "--height=800");
                    }
                    ffOptions.SetPreference("permissions.default.image", 1);
                    ffOptions.PageLoadStrategy = PageLoadStrategy.Eager;
                    Driver = new FirefoxDriver(ffOptions);
                    break;
                }
                case Browser.Edge or Browser.EdgeHeadless:
                {
                    var edgeOptions = new EdgeOptions
                    {
                        AcceptInsecureCertificates = true,
                        PageLoadStrategy = PageLoadStrategy.Eager
                    };
                    if (browserType == Browser.EdgeHeadless)
                    {
                        edgeOptions.AddArguments("window-size=1280,800", "--headless=new");
                    }
                    Driver = new EdgeDriver(edgeOptions);
                    break;
                }
                case Browser.Safari:
                {
                    var safariOptions = new SafariOptions
                    {
                        AcceptInsecureCertificates = true,
                        PageLoadStrategy = PageLoadStrategy.Eager
                    };
                    Driver = new SafariDriver(safariOptions);
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
