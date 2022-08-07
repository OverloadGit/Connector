using Core.Configuration.Mqtt.Interfaces;
using ERPConnector.Configuration.Mqtt;
using ERPConnector.Configuration.Mqtt.Publisher;
using ERPConnector.Configuration.Mqtt.Subscriber;
using ERPConnector.Configuration.Settings;
using ERPHttpConntector;
using ERPHttpConntector.Abstract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Subscriber;

var services = new ServiceCollection();
RegisterServices(services);
services.BuildServiceProvider().GetService<ISubscriber>().RunSubscriber();


Console.ReadLine();

static void RegisterServices(IServiceCollection services)
{
    IConfigurationRoot configuration = FileSettings.GetConfiguration();

    services
            .AddSingleton<IMqttConnection, MqttConnection>()
            .AddScoped<ISubscriber, Subscriber>()
            .AddScoped<IPublisher, Publisher>()
            .AddScoped<IConfiguration>(_ => configuration)
            .AddScoped<IHttpConnector, HttpConnector>()
            .AddHttpClient();
}
