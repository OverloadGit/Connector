namespace Core.Data.Messages
{
    public class ExtendHttpResponseMessage
    {
        public Guid CorrelationId { get; set; }
        public string? ProtocolVersion { get; set; }
        public string? TopicName { get; set; }
        public HttpResponseMessage? ResponseMessage { get; set; }
    }
}
