using DiscordClone.Api.Entities.Base;

namespace DiscordClone.Api.Entities
{
    public class GroupChat : BaseChat<GroupChatMessage,AccountGroupChat>
    {
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public Account Owner { get; set; }
    }
}
