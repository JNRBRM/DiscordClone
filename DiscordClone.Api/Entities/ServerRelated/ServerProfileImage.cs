using DiscordClone.Api.Entities.Base;

namespace DiscordClone.Api.Entities.ServerRelated
{
    public class ServerProfileImage : BaseImage
    {
        public int ServerProfileId { get; set; }
        public ServerProfile ServerProfile { get; set; }
    }
}