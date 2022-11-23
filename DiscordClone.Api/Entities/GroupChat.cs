namespace DiscordClone.Api.Entities
{
    public class GroupChat : Chat<GroupChatMessage,GroupChatAccount>
    {
        public string Name { get; set; }
        public GroupChatAccount Owner { get; set; }
    }
}
