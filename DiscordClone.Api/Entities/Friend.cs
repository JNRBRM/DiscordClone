namespace DiscordClone.Api.Entities
{
    public class Friend :BaseEntity<int>
    {
        public int AccountId1 { get; set; }
        public int AccountId2 { get; set; }
        public FriendStatus Status { get; set; }
        public DateTime SentDate { get; set; }
        public Account Account1 { get; set; }
        public Account Account2 { get; set; }
    }
    public enum FriendStatus
    {
        Pending,Accepted
    }
}
