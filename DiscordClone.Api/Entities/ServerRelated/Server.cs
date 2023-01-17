using DiscordClone.Api.Entities.Base;

namespace DiscordClone.Api.Entities.ServerRelated
{
    public class Server : BaseEntity<int>
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }

        public List<ServerProfile> ServerProfiles { get; set; }
        public List<Role> Roles { get; set; }

        public List<TextChannel> TextChannels { get; set; }

        public List<VoiceChannel> VoiceChannels { get; set; }

        public User User { get; set; }
    }
}