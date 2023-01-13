namespace DiscordClone.Api.Entities
{
    public abstract class BaseChannel : BaseEntity<int>
    {
        public int ServerId { get; set; }
        public string Name { get; set; }

        public bool IsAgeRestricted { get; set; }
        public List<RoleGeneralChannelPermission> GeneralPermissions { get; set; }
        public Server Server { get; set; }
    }
}