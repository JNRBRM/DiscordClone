namespace DiscordClone.Api.Entities
{
    public class AccountChat : BaseEntity<int>
    {
        public int AccountId { get; set; }
        public Guid ChatId { get; set; }
        public Account Account { get; set; }
        public Chat Chat { get; set; }
    }
}