using Microsoft.Extensions.Configuration;

namespace Common
{
    public static class AppSettings
    {
        public static IConfigurationRoot GetIConfigurationRoot(string appsettingsDotJsonFileName)
        {
            return new ConfigurationBuilder()
                .AddJsonFile(appsettingsDotJsonFileName, optional: true, reloadOnChange: true)                
                .AddEnvironmentVariables()
                .Build();
        }

        public static T GetMyAppConfiguration<T>(string appsettingsDotJsonFileName, string rootPropertyName)
            where T : class, new()
        {
            var configuration = new T();
            GetIConfigurationRoot(appsettingsDotJsonFileName).Bind(rootPropertyName, configuration);
            return configuration;
        }
    }
}
