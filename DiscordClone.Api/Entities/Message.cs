namespace DiscordClone.Api.Entities
{
    public abstract class Message<ChatType,ChatIdType> : BaseEntity<int>
    {
        public ChatIdType ChatId { get; set; }
        public int AccountId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? EditedDate { get; set; }
        //public List<MessageAttachment> MessageAttachments { get; set; }
        public List<MessageAttachment<ChatType, ChatIdType>> MessageAttachments { get; set; }
        public ChatType Chat { get; set; }
        public Account Account { get; set; }

    }
}