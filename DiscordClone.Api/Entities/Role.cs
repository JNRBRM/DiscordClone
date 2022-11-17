namespace DiscordClone.Api.Entities
{
    public class Role : Entity<int>
    {
        public string Name { get; set; }  
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }  
        public List<Account> Accounts { get; set; }
        public List<RolePermission> Permissions { get; set; }


    }
}