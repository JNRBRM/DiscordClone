namespace DiscordClone.Api.Entities
{
    public class GroupChatAccount : BaseEntity<int>
    {
        public int AccountId { get; set; }
        public int GroupChatId { get; set; }
        public Account Account { get; set; }
        public GroupChat GroupChat { get; set; }
    }
}