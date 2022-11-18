namespace DiscordClone.Api.Entities
{
    public class Chat : BaseEntity<Guid>
    {
        public DateTime CreatedDate { get; set; }
        public  List<Message> Messages { get; set; }
        public List<Account> Accounts { get; set; }
        
    }
}