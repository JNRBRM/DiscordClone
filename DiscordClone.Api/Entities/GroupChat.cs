namespace DiscordClone.Api.Entities
{
    public class GroupChat : Chat
    {
        public string Name { get; set; }
        public GroupChatAccount Owner { get; set; }
    }
}
