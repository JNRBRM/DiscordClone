namespace DiscordClone.Api.Entities
{
    public class TextChannelMessage:BaseMessage<TextChannel,int>
    {
        public List<TextChannelMessageAttachment> MessageAttachments { get; set; }
    }
}
