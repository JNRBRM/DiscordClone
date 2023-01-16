namespace DiscordClone.Api.Entities
{
    public class VoiceChannel : BaseChannel<RoleGeneralVoiceChannelPermission>
    {
        public List<VoiceChannelSetting> VoiceChannelSettings { get; set; }
        public List<RoleVoiceChannelPermission> Permissions { get;}
    }
}