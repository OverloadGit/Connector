namespace ERPConnector.Messages
{
    public class Message 
    {
        public Guid CorrelationId { get; set; }
        public string? ProtocolVersion { get; set; }
        public PayloadMessage? PayloadMessage { get; set; }
    }
}
