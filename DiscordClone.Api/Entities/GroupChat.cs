namespace DiscordClone.Api.Entities
{
    public class GroupChat : BaseChat<GroupChatMessage,AccountGroupChat>
    {
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public AccountGroupChat Owner { get; set; }
    }
}
