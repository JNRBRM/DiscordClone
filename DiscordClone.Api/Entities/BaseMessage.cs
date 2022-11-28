namespace DiscordClone.Api.Entities
{
    public abstract class BaseMessage<ChatType,ChatIdType> : BaseEntity<int>
    {
        public ChatIdType ChatId { get; set; }
        public int AccountId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? EditedDate { get; set; }
        //public List<MessageAttachment> MessageAttachments { get; set; }
        public List<BaseMessageAttachment<ChatType, ChatIdType>> MessageAttachments { get; set; }
        public ChatType Chat { get; set; }
        public Account Account { get; set; }

    }
}