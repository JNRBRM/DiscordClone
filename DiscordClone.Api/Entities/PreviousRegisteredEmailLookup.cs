namespace DiscordClone.Api.Entities
{
    public class PreviousRegisteredEmailLookup : BaseEntity<int>
    {
        public int AccountId { get; set; }
        public byte[] EmailHash { get; set; }
        public byte[] Salt { get; set; }

        public List<SecurityQuestion> Questions { get; set; }

        public Account Account { get; set; }
    }
}
