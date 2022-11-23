namespace DiscordClone.Api.Entities
{
    public class Chat : BaseEntity<Guid>
    {
        public DateTime CreatedDate { get; set; }
        public  List<ChatMessage> Messages { get; set; }
        public List<AccountChat> Accounts { get; set; }
        
    }

    public class Chat<MessageType,ChatRelation> : BaseEntity<Guid>
    {
        public DateTime CreatedDate { get; set; }
        public List<MessageType> Messages { get; set; }
        public List<ChatRelation> Accounts { get; set; }

    }
}