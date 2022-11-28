namespace DiscordClone.Api.Entities
{
    public class VoiceChannel : BaseChannel
    {
        public List<VoiceChannelSetting> VoiceChannelSettings { get; set; }
        public override Server Server { get; set; }
    }
}