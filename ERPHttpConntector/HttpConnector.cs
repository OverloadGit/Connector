using Core.Data.Messages;
using Core.Extensions.Configuration;
using Core.Extensions.Validators;
using ERPConnector.Messages;
using ERPHttpConntector.Abstract;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace ERPHttpConntector
{
    public class HttpConnector : IHttpConnector
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public HttpConnector(
            IHttpClientFactory httpClientFactory, 
            IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }
        public async Task<ExtendHttpResponseMessage> SendRequestToEndpoint(Message message)
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                try
                {
                    var httpConfiguration = _configuration.GetHttpConfiguration();
                    httpConfiguration.HttpClientConfValidator();

                    var json = JsonSerializer.Serialize(message.PayloadMessage.RequestBody);
                    var data = new StringContent(json, Encoding.UTF8, "application/json");

                    var httpRequestMessage = new HttpRequestMessage()
                    {
                        Content = data,
                        Method = new HttpMethod(message.PayloadMessage.RequestType),
                        RequestUri = new Uri(new Uri(httpConfiguration.Adres), new Uri(message.PayloadMessage.Endpoint, UriKind.Relative))
                    };

                    //var response = await httpClient.SendAsync(httpRequestMessage);

                    return new ExtendHttpResponseMessage() { CorrelationId = message.CorrelationId, ProtocolVersion = "V1.0", TopicName = message.PayloadMessage.ResponseTopic};
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
                

            }
        }
    }
}
