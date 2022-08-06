using Core.Extensions.Configuration;
using Core.Validators;
using Microsoft.Extensions.Configuration;
using MQTTnet;
using MQTTnet.Client;
using Resources;

namespace ERPConnector.Configuration.Mqtt.Abstract
{
    public abstract class CreateConnection
    {
        public IMqttClient _client;
        private MqttClientOptions _options;
        private MqttClientSubscribeOptions _subscribeOptions;

        public async void SetConnection(IConfiguration configuration)
        {
            if (_client == null)
            {
                var mqttConfugirationSettings = configuration.GetMqttConfiguration();

                mqttConfugirationSettings.MtqqConfValidator();

                var factory = new MqttFactory();
                _client = factory.CreateMqttClient();

                //configure options
                _options = new MqttClientOptionsBuilder()
                    .WithClientId(Guid.NewGuid().ToString())
                    .WithTcpServer(mqttConfugirationSettings.Host, mqttConfugirationSettings.Port)
                    .WithCleanSession()
                    .Build();

                _subscribeOptions = factory.CreateSubscribeOptionsBuilder()
                    .WithTopicFilter(f => { f.WithTopic(TopicResources.erp_topic); }).Build();

                Connect();
            }
        }

        public async void Connect()
        {
            await _client.ConnectAsync(_options);
        }

        public async void Disconnect()
        {
            await _client.DisconnectAsync();
        }

        public MqttClientSubscribeOptions GetSubscribeOptions()
        {
            return _subscribeOptions;
        }

        public IMqttClient GetClient()
        {
            if (_client != null)
                return _client;

            return null;
        }
    }
}
