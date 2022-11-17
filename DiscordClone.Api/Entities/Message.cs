namespace DiscordClone.Api.Entities
{
    public class Message : Entity<int>
    {
        public string Content { get; set; }

        public List<MessageAttachment> MessageAttachments { get; set; }

        public Guid ChatRoomId { get; set; }

        public ChatRoom ChatRoom { get; set; }

        public int AccountId { get; set; }

        public Account Account { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? EditedDate { get; set; }
    }
}