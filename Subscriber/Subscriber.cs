using Core.Configuration.Mqtt.Interfaces;
using ERPConnector.Configuration.Mqtt.Publisher;
using ERPConnector.Configuration.Mqtt.Subscriber;
using ERPConnector.Messages;
using ERPHttpConntector.Abstract;
using MQTTnet;
using System.Text;
using System.Text.Json;

namespace Subscriber
{
    public class Subscriber : ISubscriber
    {
        private readonly IMqttConnection _mqttConnection;
        private readonly IHttpConnector _httpConnector;
        private readonly IPublisher _publisher;

        public Subscriber(
            IMqttConnection mqttConnection,
            IHttpConnector httpConnector,
            IPublisher publisher)
        {
            _mqttConnection = mqttConnection;
            _httpConnector = httpConnector;
            _publisher = publisher;
        }
        public async void RunSubscriber()
        {
            _mqttConnection.ConnectMqtt();
            var _client = _mqttConnection.GetClient();

            _client.ConnectedAsync += async e =>
            {
                Console.WriteLine("### CONNECTED ###");

                var newoptions = new MqttTopicFilterBuilder().WithTopic("erp-request").Build();

                await _client.SubscribeAsync(_mqttConnection.GetSubscribeOptions());
                Console.WriteLine("### SUBSCRIBED ###");
            };
            _client.ApplicationMessageReceivedAsync += async e =>
            {
                Console.WriteLine("Received application message.");
                try
                {


                    //var cos = new PayloadMessage() { Endpoint = "123", RequestBody = "Body", RequestType = "Type", ResponseTopic = "topic" };
                    //var newMess = JsonSerializer.Serialize(cos);
                    var message = ObjectExtensions.ConvertPayload(e.ApplicationMessage.Payload);

                    var result = await _httpConnector.SendRequestToEndpoint(new Message() { PayloadMessage = message, CorrelationId = new Guid(), ProtocolVersion = "V1.0" });
                    await _publisher.PublishResponseMessage(result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
               
            };
            _client.DisconnectedAsync += e =>
            {
                Console.WriteLine("### DISCONNECTED ###");
                return Task.CompletedTask;
            };

            Console.WriteLine(_client.Options.ClientId);
        }
    }

    internal static class ObjectExtensions
    {

        public static PayloadMessage ConvertPayload(byte[] payloadMessage)
        {
            var bytesAsString = Encoding.UTF8.GetString(payloadMessage);
            var message = JsonSerializer.Deserialize<PayloadMessage>(bytesAsString);

            return message;
        }
    }
}
