namespace DiscordClone.Api.Entities
{
    public class User : BaseEntity<Guid>
    {
        public int AccountId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public DateTime PasswordSetDate { get; set; }
        public Account? Account { get; set; }
    }
}
