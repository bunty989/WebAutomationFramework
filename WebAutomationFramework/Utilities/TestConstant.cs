﻿using System.IO;

namespace WebAutomationFramework.Utilities
{
    public static class TestConstant
    {
        public static class ConfigTypes
        {
            public const string WebDriverConfig = "webDriverConfig:";
            public const string AppConfig = "AppConfig:";
        }

        public static class ConfigTypesKey
        {
            public const string Browser = "Browser";
            public const string PageLoadTimeOut = "PageLoadTimeOut";
            public const string ImplicitWaitTimeout = "ImplicitWaitTimeout";
            public const string ObjectIdentificationTimeOut = "ObjectIdentificationTimeOut";
            public const string Protocol = "Protocol";
            public const string AppUrl = "Url";
        }

        public static class PathVariables
        {
            public static string? GetBaseDirectory = Directory.GetParent(@"../../../")?.FullName;
            public static string ReportPath = Path.Combine(GetBaseDirectory ?? throw new DirectoryNotFoundException());
            public static string HtmlReportFolder = "\\Logs";
            public const string ConfigFileName = "appsettings.json";
            public const string LogName = @"\Log";
            public const string ExtentConfigName = "ExtentConfig.xml";
        }

        public static class LoggerLevel
        {
            public const string LogLevel = "LogLevel";
        }

        public enum BrowserType
        {
            Chrome,
            Firefox,
            InternetExplorer,
            Edge,
            ChromeHeadless,
            ChromeIncognito
        }
    }
}
