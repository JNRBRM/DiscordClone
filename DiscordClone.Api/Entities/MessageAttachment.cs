namespace DiscordClone.Api.Entities
{
    public class MessageAttachment : BaseEntity<int>
    {
        public int MessageId { get; set; }
        public AttachmentType Type { get; set; }  
        public string FileLocation { get; set; }


        public Message Message { get; set; }
    }
    public enum AttachmentType
    {
        File,
        Video,
        VoiceRecording,
        Image
    }
}