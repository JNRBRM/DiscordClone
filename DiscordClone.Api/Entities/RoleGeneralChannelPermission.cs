namespace DiscordClone.Api.Entities
{
    public interface RoleGeneralChannelPermission //: BaseRoleChannelPermission<TextChannel>
    {}
    public class RoleGeneralTextChannelPermission : BaseRolePermission<TextChannel>, RoleGeneralChannelPermission { }
    public class RoleGeneralVoiceChannelPermission : BaseRolePermission<VoiceChannel>, RoleGeneralChannelPermission { }
}