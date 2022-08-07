using Core.Configuration.Mqtt.Interfaces;
using Core.Data.Messages;
using MQTTnet;
using System.Runtime.Serialization.Formatters.Binary;

namespace ERPConnector.Configuration.Mqtt.Publisher
{
    public class Publisher : IPublisher
    {
        private readonly IMqttConnection _mqttConnection;

        public Publisher(IMqttConnection mqttConnection)
        {
            _mqttConnection = mqttConnection;
        }

        public async Task PublishResponseMessage(ExtendHttpResponseMessage message)
        {
            var responseMessage = new MqttApplicationMessageBuilder()
                .WithTopic(message.TopicName)
                .WithPayload(ObjectToByteArray(message.ResponseMessage))
                .WithCorrelationData(ObjectToByteArray(message.CorrelationId))
                .Build();

            var _client = _mqttConnection.GetClient();

            if (_client.IsConnected)
                await _client.PublishAsync(responseMessage);
        }

        byte[] ObjectToByteArray(object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }
    }
}
