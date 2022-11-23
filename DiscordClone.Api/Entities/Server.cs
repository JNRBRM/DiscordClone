namespace DiscordClone.Api.Entities
{
    public class Server : BaseEntity<int>
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }

        public List<ServerProfile> Accounts { get; set; }
        public List<Role> Roles { get; set; }

        public List<TextChannel> TextChannels { get; set; }

        public List<VoiceChannel> VoiceChannels { get; set; }

        public User User { get; set; }
    }
}