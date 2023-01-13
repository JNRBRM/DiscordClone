namespace DiscordClone.Api.Entities.Test
{
    public class TServer : BaseEntity<int>
    {
        public List<TChannel> Chans { get; set; }
        public List<TRole> Roles { get; set; }
    }
    public class TRole : BaseEntity<int>
    {
        public int Sid { get; set; }
        public TServer S { get; set; }
        public List<TRolePermission> TPerms { get; set; }
    }
    public class TChannel : BaseEntity<int>
    {
        public int Sid { get; set; }
        public TServer S { get; set; }
        public List<TRolePermission> TPerms { get; set; }
    }
    public class TRolePermission : BaseEntity<int>
    {
        public int Cid { get; set; }
        public TChannel C { get; set; }
        public int Pid { get; set; }
        public TPermission P { get; set; }
        public int Rid { get; set; }
        public TRole R { get; set; }
    }
    public class TPermission : BaseEntity<int>
    {
        public int MyProperty { get; set; }
    }
}
