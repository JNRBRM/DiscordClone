namespace DiscordClone.Api.Entities
{
    public class VoiceChannel : BaseChannel
    {
        public List<VoiceChannelSetting> VoiceChannelSettings { get; set; }
    }
}