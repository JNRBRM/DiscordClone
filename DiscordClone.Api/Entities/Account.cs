namespace DiscordClone.Api.Entities
{
    public class Account : Entity<int>
    {
        public Guid? UserId { get; set; }
        public string DisplayName { get; set; }

        public List<Account> Friends { get; set; }
        public List<Message> Messages { get; set; }
        public List<ChatRoom> ChatRooms { get; set; }
        public List<Server> Servers { get; set; }
        
        
        public User User { get; set; }

    }
}
