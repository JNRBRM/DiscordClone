using DiscordClone.Api.Entities.Base;

namespace DiscordClone.Api.Entities.ServerRelated
{
    public class TextChannel : BaseChannel<RoleGeneralTextChannelPermission>
    {
        //add mute sometime ye
        public List<TextChannelSetting> TextChannelSettings { get; set; }
        public List<TextChannelMessage> Messages { get; set; }
        public List<RoleTextChannelPermission> Permissions { get; set; }

    }
}