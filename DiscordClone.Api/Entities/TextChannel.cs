namespace DiscordClone.Api.Entities
{
    public class TextChannel : BaseChannel
    {
        //add mute sometime ye
        public List<TextChannelSetting> TextChannelSettings { get; set; }
        public List<TextChannelMessage> Messages { get; set; }
        public List<RoleTextChannelPermission> Permissions { get; set; }
       
    }
}