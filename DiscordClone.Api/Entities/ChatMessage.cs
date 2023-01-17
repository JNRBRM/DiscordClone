using DiscordClone.Api.Entities.Base;

namespace DiscordClone.Api.Entities
{
    public class ChatMessage:BaseMessage<Chat,Guid>
    {
        public List<ChatMessageAttachment> MessageAttachments { get; set; }
    }
}