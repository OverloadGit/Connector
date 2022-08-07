using Core.Data.Messages;

namespace ERPConnector.Configuration.Mqtt.Publisher
{
    public interface IPublisher
    {
        Task PublishResponseMessage(ExtendHttpResponseMessage message);
    }
}
