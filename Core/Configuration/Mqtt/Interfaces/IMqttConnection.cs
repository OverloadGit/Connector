using MQTTnet.Client;

namespace Core.Configuration.Mqtt.Interfaces
{
    public interface IMqttConnection
    {
        void ConnectMqtt();
        IMqttClient GetClient();
        MqttClientSubscribeOptions GetSubscribeOptions();
    }
}
