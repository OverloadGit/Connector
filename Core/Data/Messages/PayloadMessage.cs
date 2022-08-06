namespace ERPConnector.Messages
{
    public class PayloadMessage 
    {
        public string? Endpoint { get; set; }
        public string? RequestType { get; set; }
        public string? RequestBody { get; set; }
        public string? ResponseTopic { get; set; }
    }
}
