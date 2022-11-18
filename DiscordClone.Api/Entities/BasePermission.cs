namespace DiscordClone.Api.Entities
{
    public abstract class BasePermission : BaseEntity<int>
    {
        public string Action { get; set; }
        public string Description { get; set; }
    }
}