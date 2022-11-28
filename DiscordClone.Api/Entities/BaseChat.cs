namespace DiscordClone.Api.Entities
{
    public abstract class BaseChat<MessageType, ChatRelation> : BaseEntity<Guid>
    {
        public DateTime CreatedDate { get; set; }
        public List<MessageType> Messages { get; set; }
        public List<ChatRelation> Accounts { get; set; }

    }
}
