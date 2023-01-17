using DiscordClone.Api.Entities.Base;

namespace DiscordClone.Api.Entities
{
    public class GroupChatMessage: BaseMessage<GroupChat,Guid>
    {
        public List<GroupChatMessageAttachment> MessageAttachments { get; set; }
    }
}
