using Microsoft.Extensions.Configuration;

namespace WebAutomationFramework.Utilities
{
    internal class ConfigHelper
    {
        public static string ReadConfigValue(string configType, string keyValue)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(TestConstant.PathVariables.ConfigFileName).Build();
            return config.GetValue<string>(configType + keyValue);
        }
    }
}
