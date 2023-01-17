using DiscordClone.Api.Entities.Base;

namespace DiscordClone.Api.Entities.ServerRelated
{
    public class ServerProfile : BaseEntity<int>
    {
        public int AccountId { get; set; }
        public int ServerId { get; set; }
        public string NickName { get; set; }
        public ServerProfileImage Image { get; set; }
        public Account Account { get; set; }
        public Server Server { get; set; }

    }
}