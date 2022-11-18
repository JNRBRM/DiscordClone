namespace DiscordClone.Api.Entities
{
    public class Message : BaseEntity<int>
    {
        public Guid ChatId { get; set; }
        public int AccountId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? EditedDate { get; set; }
        public List<MessageAttachment> MessageAttachments { get; set; }
        public Chat Chat { get; set; }
        public Account Account { get; set; }

    }
}