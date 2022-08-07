using Core.Data.Messages;
using ERPConnector.Messages;

namespace ERPHttpConntector.Abstract
{
    public interface IHttpConnector
    {
        Task<ExtendHttpResponseMessage> SendRequestToEndpoint(Message message);
    }
}
