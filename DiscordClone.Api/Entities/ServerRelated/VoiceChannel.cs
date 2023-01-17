using DiscordClone.Api.Entities.Base;

namespace DiscordClone.Api.Entities.ServerRelated
{
    public class VoiceChannel : BaseChannel<RoleGeneralVoiceChannelPermission>
    {
        public List<VoiceChannelSetting> VoiceChannelSettings { get; set; }
        public List<RoleVoiceChannelPermission> Permissions { get; }
    }
}