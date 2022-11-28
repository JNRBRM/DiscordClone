namespace DiscordClone.Api.Entities
{
    public abstract class BaseMessageAttachment<MessageType, ChatIdType> : BaseEntity<int>
    { 
        public int MessageId { get; set; }
        public AttachmentType Type { get; set; }  
        public string FileLocation { get; set; }

        public BaseMessage<MessageType, ChatIdType> Message { get; set; }
    }
    public enum AttachmentType
    {
        File,
        Video,
        VoiceRecording,
        Image
    }
}