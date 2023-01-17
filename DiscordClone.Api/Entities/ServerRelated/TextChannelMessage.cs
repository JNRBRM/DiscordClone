using DiscordClone.Api.Entities.Base;

namespace DiscordClone.Api.Entities.ServerRelated
{
    public class TextChannelMessage : BaseMessage<TextChannel, int>
    {
        public List<TextChannelMessageAttachment> MessageAttachments { get; set; }
    }
}
