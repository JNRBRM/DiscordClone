namespace DiscordClone.Api.Entities
{
    public class GroupChatAccount : BaseEntity<Guid>
    {
        public int AccountId { get; set; }
        public Guid GroupChatId { get; set; }
        public Account Account { get; set; }
        public GroupChat GroupChat { get; set; }
    }
}