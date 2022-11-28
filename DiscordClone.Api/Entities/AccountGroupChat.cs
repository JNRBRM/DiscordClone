namespace DiscordClone.Api.Entities
{
    public class AccountGroupChat : BaseEntity<int>
    {
        public int AccountId { get; set; }
        public Guid ChatId { get; set; }
        public Account Account { get; set; }
        public GroupChat GroupChat { get; set; }
    }
}
