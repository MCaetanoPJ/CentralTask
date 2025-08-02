namespace CentralTask.Core.DTO.Worker
{
    public class MessageBase
    {
        public MessageType MessageType { get; set; } = MessageType.RESOURCE_UNIQUE_ID;
        public string Message { get; set; }
        public bool Reprocess { get; set; } = false;
    }

    public class MessageEventModel : MessageBase
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class MessageRequestModel : MessageBase
    {
        public QueueEventType QueueEvent { get; set; }
    }

    public enum QueueEventType
    {
        CLIENT_NF_REQUIRED,
        INVOICE_NOTIFICATION
    }

    public enum MessageType
    {
        RESOURCE_UNIQUE_ID,
        RESOURCE_BODY,
        FEDERAL_DOCUMENT
    }
}