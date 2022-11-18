namespace DiscordClone.Api.Entities
{
    public class Account : BaseEntity<int>
    {
        public Guid? UserId { get; set; }
        public string DisplayName { get; set; }
        public int AccountImageId { get; set; }
        public List<Account> Friends { get; set; }
        public List<Message> Messages { get; set; }
        public List<Chat> Chats { get; set; }
        public List<Server> Servers { get; set; }
        public List<ServerProfile> ServerProfiles { get; set; }
        public AccountImage Image { get; set; }
        public User User { get; set; }

    }
}
