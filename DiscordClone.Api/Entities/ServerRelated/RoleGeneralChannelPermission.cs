using DiscordClone.Api.Entities.Base;

namespace DiscordClone.Api.Entities.ServerRelated
{
    public interface RoleGeneralChannelPermission //: BaseRoleChannelPermission<TextChannel>
    { }
    public class RoleGeneralTextChannelPermission : BaseRolePermission<TextChannel>, RoleGeneralChannelPermission { }
    public class RoleGeneralVoiceChannelPermission : BaseRolePermission<VoiceChannel>, RoleGeneralChannelPermission { }
}