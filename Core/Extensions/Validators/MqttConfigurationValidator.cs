using Core.Data.Configuration;

namespace Core.Validators
{
    internal static class MqttConfigurationValidator
    {
        public static void MtqqConfValidator(this MqttConfiguration mqttConfiguration)
        {
            if (mqttConfiguration == null)
                throw new ArgumentNullException("Configuration data are null");

            if (mqttConfiguration.Host == null)
                throw new ArgumentNullException("Host value is null, cant connect with broker");

            if (mqttConfiguration.Port.Equals(default))
                throw new ArgumentNullException("Port value is null, cant connect with broker");
        }
    }
}
