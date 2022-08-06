using Core.Data.Configuration;
using Microsoft.Extensions.Configuration;

namespace Core.Extensions.Configuration
{
    public static class ConfigurationExtensions
    {
        public static MqttConfiguration GetMqttConfiguration(this IConfiguration configuration)
        {
            var mqttconfiguration = configuration.GetSection("MqttConfiguration").Get<MqttConfiguration>();

            return mqttconfiguration;
        }

        public static HttpClientConfiguration GetHttpConfiguration(this IConfiguration configuration)
        {
            var httpClientConfiguration = configuration.GetSection("ERPHttpConfiguration").Get<HttpClientConfiguration>();

            return httpClientConfiguration;
        }
    }
}
