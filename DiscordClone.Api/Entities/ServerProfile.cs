namespace DiscordClone.Api.Entities
{
    public class ServerProfile : BaseEntity<int>
    {
        public int AccountId { get; set; }
        public int ServerId { get; set; }
        public int ProfileImageId { get; set; }
        public string NickName { get; set; }
        public ProfileImage Image { get; set; }
        public Account Account { get; set; }
        public Server Server { get; set; }

    }
}