namespace DiscordClone.Api.DataModels
{
    public class RegisterModel
    {
        public string Displayname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? PhoneNumber { get; set; }
        public Dictionary<string,string> SecurityQuestion { get; set; }
    }
}
