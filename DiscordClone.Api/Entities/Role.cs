namespace DiscordClone.Api.Entities
{
    public class Role : BaseEntity<int>
    {
        public string Name { get; set; }  
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsServerAdmin { get; set; }
        public int ServerId { get; set; }
        public List<ServerProfile> Profiles { get; set; }
        public List<RoleGeneralServerPermission> RoleGeneralServerPermission { get; set; }
       // public List<RoleGeneralTextChannelPermission> RoleGeneralTextChannelPermission { get; set; }
        public List<RoleGeneralVoiceChannelPermission> RoleGeneralVoiceChannelPermission { get; set; }
        public List<RoleMembershipPermission> RoleMembershipPermission { get; set; }
        public List<RoleTextChannelPermission> RoleTextChannelPermission { get; set; }
        public List<RoleVoiceChannelPermission> RoleVoiceChannelPermission { get; set; }
        public Server Server { get; set; }

    }
}