namespace DiscordClone.Api.Entities
{
    public abstract class BaseMessageAttachment<ChatType, ChatIdType, MessageType> : BaseEntity<int>
    { 
        public int MessageId { get; set; }
        public AttachmentType Type { get; set; }  
        public string FileLocation { get; set; }

        public MessageType Message { get; set; }
    }
    public enum AttachmentType
    {
        File,
        Video,
        VoiceRecording,
        Image
    }
}