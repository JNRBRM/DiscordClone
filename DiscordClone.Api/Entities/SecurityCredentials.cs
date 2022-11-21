namespace DiscordClone.Api.Entities
{
    public class SecurityCredentials : BaseEntity<Guid>
    {
        public Guid UserId { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] Salt { get; set; }
        public User User { get; set; }
    }
}
