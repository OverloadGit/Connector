using Microsoft.Extensions.Configuration;

namespace ERPConnector.Configuration.Settings
{
    internal static class FileSettings
    {
        public static IConfigurationRoot GetConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json");

            return configuration.Build();
        }
    }
}
