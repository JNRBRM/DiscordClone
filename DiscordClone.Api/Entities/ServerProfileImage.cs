namespace DiscordClone.Api.Entities
{
    public class ServerProfileImage : BaseImage
    {
        public int ServerProfileId { get; set; }
        public ServerProfile ServerProfile { get; set; }
    }
}