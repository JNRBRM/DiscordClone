namespace DiscordClone.Api.Entities
{
    public abstract class MessageAttachment<ChatType, ChatIdType> : BaseEntity<int>
    { 
        public int MessageId { get; set; }
        public AttachmentType Type { get; set; }  
        public string FileLocation { get; set; }

        public Message<ChatType, ChatIdType> Message { get; set; }
    }
    public enum AttachmentType
    {
        File,
        Video,
        VoiceRecording,
        Image
    }
}