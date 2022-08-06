using Core.Configuration.Mqtt.Interfaces;
using ERPConnector.Configuration.Mqtt.Abstract;
using Microsoft.Extensions.Configuration;
using MQTTnet.Client;

namespace ERPConnector.Configuration.Mqtt
{
    

    public class MqttConnection : CreateConnection, IMqttConnection
    {
        private readonly IConfiguration configuration;
        public MqttConnection(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void ConnectMqtt()
        {
            if(!(_client != null && _client.IsConnected))
            {
                base.SetConnection(configuration);
            }
        }
    }
}
